using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelUI : MonoBehaviour
{
    [SerializeField]
    protected GameObject buttonTemplate;
    public List<ButtonManager> itemList = new List<ButtonManager>();
    protected Image uIImage;

    public void HideUI()
    {
        uIImage.enabled = false;
        foreach (ButtonManager thisItem in itemList)
        {
            GameObject button = thisItem.gameObject;
            button.GetComponent<Image>().enabled = false;
            foreach (Transform child in button.transform)
            {
                child.gameObject.SetActive(false);
            }
        }

    }

    public void ShowUI()
    {
        uIImage.enabled = true;
        foreach (ButtonManager thisItem in itemList)
        {
            GameObject button = thisItem.gameObject;
            button.GetComponent<Image>().enabled = true;
            foreach (Transform child in button.transform)
            {
                child.gameObject.SetActive(true);
            }
        }
    }

    public void HideNewItem(GameObject newItem)
    {

        newItem.GetComponent<Image>().enabled = false;
        foreach (Transform child in newItem.transform)
        {
            child.gameObject.SetActive(false);
        }
    }
}
