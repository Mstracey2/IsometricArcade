using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse : MonoBehaviour
{
    public static Mouse Instance;
    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    #region variables
    protected Camera cam;                       // reference to the main camera
    protected RaycastHit hit;                   // result of raycast
    [SerializeField]
    private Ray ray;                            // ray for raycast

    [SerializeField]
    private UnityEngine.GameObject grabPosition;            // location of the grabber, uses the transform as a location when moving objects
    [SerializeField]
    private LayerMask ignoreRaycast;            //Layer that ignores the raycast, this is required for moving the object around 
    [SerializeField]
    private LayerMask defaultLayer;             // default layer, object switches back to this when the object is dropped so i can be picked back up again
    [SerializeField]
    private GameObject grabbedObject = null;    // reference to the object thats grabbed, stops the game automatically picking up other objects when one is already picked up
    private Rigidbody grabbedObjectRigidBody;

    public float bottomCol;

    private bool editorMode;                    // bool which checks to see whether the game is in editor mode
    Clickable comp;
    #endregion

    private void Start()
    {
        cam = Camera.main;
    }

    public void Raycast()                                   //raycast from mouse
    {                           
        ray = cam.ScreenPointToRay(Input.mousePosition);    //gets mouse position from the camera
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Raycast();
            Physics.Raycast(ray, out hit, 100);
            
            if(hit.collider != null)
            {
                hit.collider.gameObject.TryGetComponent<Clickable>(out comp);
                if (comp != null)
                {
                    comp.RunFunction();
                    if (comp.gameObject.CompareTag("Object") && CheckEditor() == false)
                    {
                        MinigameButtonScript.Instance.ShowButton();
                        MinigameButtonScript.Instance.ChangeScene(comp.GetComponent<ArcadeMachieneController>().ShowMinigame());

                    }
                }
            }
            comp = null;
        }

        if(editorMode)
        {
            Raycast();
            Physics.Raycast(ray, out hit, 100);
            EditorMouse();                                //checks if what the player clicked on is movable and moves it
        }
    }

    #region Object movement

    float FindBoundary(Collider col,Transform obj)                        // function used to find the boundary of the collider of the object.
    {
        float yHalf = col.bounds.extents.y;
        float yCenter= col.bounds.center.y;
        float yLower = obj.transform.position.y + (yCenter - yHalf);
        return yLower;                                              // returns the lower boundary of the collider, used to move the object around at this position
    }

    void EditorMouse()
    {
        if (Input.GetMouseButton(0))
        {
            if (hit.collider != null)
            {
                CheckEditCollider();
            }
            if (grabbedObject != null)
            {
                grabbedObjectRigidBody.isKinematic = true;
                grabbedObjectRigidBody.MovePosition(new Vector3(grabPosition.transform.localPosition.x, grabbedObjectRigidBody.position.y, grabPosition.transform.localPosition.z));
                //grabbedObject.transform.position = new Vector3(grabPosition.transform.localPosition.x, grabPosition.transform.localPosition.y + (grabbedObject.transform.position.y - bottomCol), grabPosition.transform.localPosition.z); //grabbed object follows the position of the grabber object, but on the Y axis, its grabbed at the bottom of the object boundary
            }
        }
        else if (editorMode && Input.GetMouseButtonUp(0) && grabbedObject != null)           //if the player lets go of the mouse button
        {
            grabbedObject.layer = defaultLayer;                               // returns layer to default 
            grabbedObject = null;                                             // grabbed object variable turns back to null
            grabbedObjectRigidBody.isKinematic = false;
            grabbedObjectRigidBody = null;
        }
    }

    public void CheckEditCollider()
    {
        if (hit.collider.gameObject.tag == "Object" && grabbedObject == null)    //if the collider is an object and the player isn't already holding something       
        {
            grabbedObject = hit.collider.gameObject;                            // grabbed object becomes the object
            grabbedObject.layer = ignoreRaycast;                                // the grabbed objects layer becomes the ignore raycast layer
            grabbedObjectRigidBody = grabbedObject.GetComponent<Rigidbody>();

            bottomCol = FindBoundary(grabbedObject.GetComponent<Collider>(), grabbedObject.transform);
        }
        else if (hit.collider.gameObject.tag == "Floor")
        {
            grabPosition.transform.position = new Vector3(hit.point.x, hit.point.y, hit.point.z);       // grab position follows above the mouse if the mouse is pointing at the floor
        }
    }

    #endregion


    #region Editor statements
    public void ChangeToEditor()            //changes the editor variable
    {
        editorMode = !editorMode;   
    }

    public bool CheckEditor()               //returns editor value
    {
        return editorMode;
    }
    #endregion
}
