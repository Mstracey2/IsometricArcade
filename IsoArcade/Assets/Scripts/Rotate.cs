using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : Clickable
{
    MeshRenderer rend;                      // renderer and collider is enabled and disabled when editor mode is switched
    Collider col;
    GameObject parent;                      // gets parent object
    public GameObject center;
    float newAngle;                         // angle used to rotate object

    #region Get components
    // Start is called before the first frame update
    public void Start()
    {
        rend = GetComponent<MeshRenderer>();
        col = GetComponent<Collider>();
        parent = transform.parent.gameObject; 
        inheritedFunction = RotateParent;                                           // inherited function from clickable is set to rotate
    }
    #endregion

    // Update is called once per frame
    void Update()
    {
        if (Mouse.Instance.mouseMode == Mouse.Instance.MouseEditorMode)                                                 // if in editor mode, make the object visible and collidable
        {
            rend.enabled = true;
            col.enabled = true;
        }
        else
        {
            rend.enabled = false;
            col.enabled = false;
        }
    }

   public void RotateParent()
    {
        newAngle = transform.localRotation.y + 90;                        //snaps the parent on a 90 degree rotation

        parent.transform.Rotate(0, newAngle, 0, Space.Self);                     //rotates parent on the new angle
    }
}
