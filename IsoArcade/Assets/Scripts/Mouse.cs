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
    private GameObject grabPosition;            // location of the grabber, uses the transform as a location when moving objects
    [SerializeField]
    private LayerMask ignoreRaycast;            //Layer that ignores the raycast, this is required for moving the object around 
    [SerializeField]
    private LayerMask defaultLayer;             // default layer, object switches back to this when the object is dropped so i can be picked back up again
    [SerializeField]
    public GameObject grabbedObject = null;    // reference to the object thats grabbed, stops the game automatically picking up other objects when one is already picked up
    private Rigidbody grabbedObjectRigidBody;
    Clickable comp;

    public delegate void MouseFunction();
    public MouseFunction mouseMode;
    #endregion

    private void Start()
    {
        cam = Camera.main;
        mouseMode = MouseMainMode;
    }

    public void Raycast()                                   //raycast from mouse
    {                           
        ray = cam.ScreenPointToRay(Input.mousePosition);    //gets mouse position from the camera
    }

    // Update is called once per frame
    void Update()
    {
        Raycast();
        Physics.Raycast(ray, out hit, 100);

        mouseMode();
      
    }

    #region Object movement

    public void MouseMainMode()
    {
            Clickable();
                    if (comp != null && comp.gameObject.CompareTag("Object") && Mouse.Instance.mouseMode != Mouse.Instance.MouseEditorMode)
                    {
                        MinigameButtonScript.Instance.ShowButton();
                        MinigameButtonScript.Instance.ChangeScene(comp.GetComponent<ArcadeMachieneController>().ShowMinigame());

                    }
            comp = null;
    }

    public void MouseEditorMode()
    {
        Clickable();
        comp = null;

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
            }
        }
        else if (Input.GetMouseButtonUp(0) && grabbedObject != null)           //if the player lets go of the mouse button
        {
            grabbedObject.layer = defaultLayer;                               // returns layer to default 
            grabbedObject = null;                                             // grabbed object variable turns back to null
            grabbedObjectRigidBody.isKinematic = false;
            grabbedObjectRigidBody = null;
        }
    }

    public void Clickable()
    {
         if (Input.GetMouseButtonDown(0))
         {
            if (hit.collider != null)
            {
             hit.collider.gameObject.TryGetComponent<Clickable>(out comp);
                if (comp != null)
                {
                comp.RunFunction();
                }
            }
         }
    }

    public void MouseMiniGameOne()
    {
        if (hit.collider != null)
        {
            if (Input.GetMouseButtonDown(0) && hit.collider.CompareTag("PoolBall") && grabbedObject == null)
            {
                grabbedObject = hit.collider.gameObject;
            }
            if (Input.GetMouseButton(0) && grabbedObject != null)
            {
                grabbedObject.transform.position = Vector3.MoveTowards(new Vector3(grabbedObject.transform.position.x, grabbedObject.transform.position.y, grabbedObject.transform.position.z), new Vector3(hit.point.x, grabbedObject.transform.position.y, hit.point.z), 1 * Time.deltaTime);
            }

        }
        if (Input.GetMouseButtonUp(0))
        {
            grabbedObject = null;
        }
    }

    public void CheckEditCollider()
    {
        if (hit.collider.gameObject.tag == "Object" && grabbedObject == null)    //if the collider is an object and the player isn't already holding something       
        {
            grabbedObject = hit.collider.gameObject;                            // grabbed object becomes the object
            grabbedObject.layer = ignoreRaycast;                                // the grabbed objects layer becomes the ignore raycast layer
            grabbedObjectRigidBody = grabbedObject.GetComponent<Rigidbody>();
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
        if(mouseMode == MouseEditorMode)
        {
            mouseMode = MouseMainMode;
        }
        else
        {
            mouseMode = MouseEditorMode;
        }
       
    }

    #endregion
}
