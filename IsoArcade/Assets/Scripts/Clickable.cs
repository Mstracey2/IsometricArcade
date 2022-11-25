using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Clickable : MonoBehaviour
{
    #region Variables
    protected Camera cam;                           //reference to the main camera, used to raycast out from camera position
    protected delegate void MyDelegate();           
    protected MyDelegate inheritedFunction;         //delegate to determine what happens when the player clicks on the object, function changes by an inherited script
    protected RaycastHit hit;                       //gets the result from the raycast.
    #endregion

    private void OnMouseDown()                  //when mouse button is pressed
    {
        Raycast();
    }


    public void Raycast()
    {
        cam = Camera.main;                                      //camera is equal to the main camera
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);    //ray origin is assigned to the mouse position on the camera

        Physics.Raycast(ray, out hit, 100);                     //sends raycast out from mouse

        if (hit.collider.gameObject == this.gameObject && inheritedFunction != null)        //if the player connects with the machine and a function is assigned
        {
            inheritedFunction();                // executes the function
        }
    }

   
}
