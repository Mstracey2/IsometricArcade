using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelUI : MonoBehaviour            // base class for any UI with panel, holds the list of buttons attached to the panel
{
    #region Variables
    [SerializeField]
    protected GameObject buttonTemplate;                                //template for all the buttons required to spawn new items
    public List<ButtonManager> itemList = new List<ButtonManager>();    // list of all the button managers in the panel, used for various functions like adding and removing new buttons
    protected Image uIImage;
    #endregion


    #region UI panel visibility
    public void HideUI()
    {
        uIImage.enabled = false;                                //sets the panel to false to hide the shop or inventory
        foreach (ButtonManager thisItem in itemList)            //process of disabling all imagery of the buttons, I couldn't stimply deactivate the object, as that would stop the buttons timer making them only tick when the shop was open. 
        {
            GameObject button = thisItem.gameObject;
            button.GetComponent<Image>().enabled = false;
            foreach (Transform child in button.transform)
            {
                child.gameObject.SetActive(false);
            }
        }

    }

    public void ShowUI()                                    //same process for disabling the UI but for enabling it
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
    #endregion
    public void HideNewItem(GameObject newItem)             //this is a special function used to hide any newly created buttons if the panel isn't open
    {
        newItem.GetComponent<Image>().enabled = false;
        foreach (Transform child in newItem.transform)
        {
            child.gameObject.SetActive(false);
        }
    }
}
