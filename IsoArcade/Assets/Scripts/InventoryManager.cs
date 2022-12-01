using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : PanelUI
{
    [SerializeField]
    private Currency playerCurrency;

    // Start is called before the first frame update
    void Start()
    {
        uIImage = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float GetCurrency()
    {
        return playerCurrency.ReturnCurrency();
    }

    public void AddItemToList(GameObject item)
    {
        item.transform.SetParent(this.gameObject.transform, false);

        ButtonManager newManager = item.GetComponent<ButtonManager>();
        itemList.Add(newManager);
        HideNewItem(item.gameObject);
    }
}
