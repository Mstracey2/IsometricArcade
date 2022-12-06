using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideUI : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> allText = new List<GameObject>();          //holds all UI buttons and text

    public void Hide()
    {
        foreach(GameObject thisText in allText)
        {
            thisText.SetActive(false);                  //sets them all to false
        }
    }

    public void Show()
    {
        foreach (GameObject thisText in allText)
        {
            if (!thisText.CompareTag("SecondaryUI"))        //will only set to true if is Primary UI (stops the buy room text and buttons etc)
            {
              thisText.SetActive(true);
            }
        }
    }
}
