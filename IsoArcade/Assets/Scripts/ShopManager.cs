using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : PanelUI
{
    public float timer;

    [SerializeField]
    private InventoryManager playerInventory;

    public ButtonManager itemButton;
    // Start is called before the first frame update
    void Start()
    {
        timer = 10;
        uIImage = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if(itemList.Count < 5)
        {
            timer -= Time.deltaTime;
        }        
        if(timer <= 0 && itemList.Count < 5)
        {
            GameObject newItem = Instantiate(buttonTemplate, this.gameObject.transform);
            StartCoroutine(AddToList(newItem));
            if (uIImage.enabled == false)
            {
                HideNewItem(newItem);
            }
            

            timer = 5;
        }
        
        foreach (ButtonManager thisItem in itemList)
        {
            if (thisItem.CheckTime() <= 0)
            {
                itemList.Remove(thisItem);
                break;
            }
        }

    }
    public void RandomizeTimer()
    {
        timer = Random.Range(20, 300);
    }

    IEnumerator AddToList(GameObject item)
    {
        ButtonManager newManager = item.GetComponent<ButtonManager>();
        yield return new WaitForSeconds(0.1f);

        itemList.Add(newManager);
        newManager.setShop(this);
        newManager.AddMoveToInventoryListener();

        yield break;
    }
   


    public void MoveToInventory(GameObject chosenItem)
    {
        itemButton = chosenItem.GetComponent<ButtonManager>();

        if (playerInventory.GetCurrency() >= itemButton.CheckPrice())
        {
            playerInventory.ChangeCurrency(playerInventory.GetCurrency() - itemButton.CheckPrice());
            itemButton.NoLongerInShop();
            itemButton.AddSpawnInRoomListener(playerInventory);
            playerInventory.AddItemToList(chosenItem);
            itemList.Remove(chosenItem.GetComponent<ButtonManager>());
        }
    }
  
}
