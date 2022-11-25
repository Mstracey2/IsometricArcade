using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse : MonoBehaviour
{
    protected Camera cam;                       // reference to the main camera
    protected RaycastHit hit;                   // result of raycast
    [SerializeField]
    private Ray ray;                            // ray for raycast

    [SerializeField]
    private GameObject grabPosition;            // location of the grabber, uses the transform as a location when moving objects
    [SerializeField]
    private LayerMask ignoreRaycast;
    [SerializeField]
    private LayerMask defaultLayer;
    [SerializeField]
    private GameObject grabbedObject = null;

    private bool editorMode;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Raycast()
    {
        cam = Camera.main;
        ray = cam.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(ray, out hit, 100))
        {
            CheckMousePos();
        }
        
           

           
        
    }

    // Update is called once per frame
    void Update()
    {
        if (editorMode)
        {
            Raycast();
        }
       
    }

    float FindBoundary(Collider col)
    {
        float yHalf = col.bounds.extents.y;
        float yCenter= col.bounds.extents.y;
        float yLower = transform.position.y + (yCenter - yHalf);
        return yLower;
    }

    void CheckMousePos()
    {
        if (Input.GetMouseButton(0))
        {
            
            if (hit.collider.gameObject.tag == "Object" && grabbedObject == null)
            {
                grabbedObject = hit.collider.gameObject;
                grabbedObject.layer = ignoreRaycast;
            }
            else if (hit.collider.gameObject.tag == "Floor" )
            {
                grabPosition.transform.position = new Vector3(hit.point.x, hit.point.y, hit.point.z);
            }

            if (grabbedObject != null)
            {
                grabbedObject.transform.position = new Vector3(grabPosition.transform.position.x, grabPosition.transform.position.y + FindBoundary(grabbedObject.GetComponent<Collider>()) - 0.12f, grabPosition.transform.position.z);
            }

        }
        if (Input.GetMouseButtonUp(0) && grabbedObject != null)
        {
            grabbedObject.layer = defaultLayer;
            grabbedObject = null;
        }
    }

    public void ChangeToEditor()
    {
        editorMode = !editorMode;
        
    }

    public bool CheckEditor()
    {
        return editorMode;
    }
}
