using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Clickable : MonoBehaviour
{

    protected Camera cam;
    protected delegate void MyDelegate();
    protected MyDelegate inheritedFunction;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            cam = Camera.main;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100))
            {
                if(hit.collider.gameObject == this.gameObject && inheritedFunction != null)
                {
                    inheritedFunction();
                }  
            }
        }
    }
}
