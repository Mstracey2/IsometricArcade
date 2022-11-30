using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideUI : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> allText = new List<GameObject>();

    public void Hide()
    {
        foreach(UnityEngine.GameObject thisText in allText)
        {
            thisText.SetActive(false);
        }
    }

    public void Show()
    {
        foreach (UnityEngine.GameObject thisText in allText)
        {
            thisText.SetActive(true);
        }
    }
}
