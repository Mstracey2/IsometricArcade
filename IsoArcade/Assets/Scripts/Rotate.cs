using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : Clickable
{
    Mouse mManager;
    MeshRenderer rend;
    Collider col;
    GameObject parent;
    float newAngle;
    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<MeshRenderer>();
        col = GetComponent<Collider>();
        mManager = GameObject.Find("MouseManager").GetComponent<Mouse>();
        parent = transform.parent.gameObject;
        inheritedFunction = RotateParent;
    }

    // Update is called once per frame
    void Update()
    {
        if (mManager.CheckEditor())
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

    void RotateParent()
    {
        newAngle = parent.transform.localRotation.y + 90;

        parent.transform.Rotate(0, newAngle, 0, Space.Self);
    }
}
