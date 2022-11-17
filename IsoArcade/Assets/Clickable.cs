using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Clickable : MonoBehaviour
{

    protected Camera cam;
    protected delegate void MyDelegate();
    protected MyDelegate inheritedFunction;

    private void OnMouseDown()
    {
        cam = Camera.main;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100))
        {
            if (hit.collider.gameObject == this.gameObject && inheritedFunction != null)
            {
                inheritedFunction();
            }
        }
    }
}
