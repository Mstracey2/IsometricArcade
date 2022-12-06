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
        if(itemList.Count < 5)                  //the spawner is limited to spawning 5 items at a time
        {
            timer -= Time.deltaTime;            //counts down timer till spawning a new button
        }        
        if(timer <= 0 && itemList.Count < 5)
        {
            GameObject newItem = Instantiate(buttonTemplate, this.gameObject.transform);        //spawns a randomly generated new button with item
            StartCoroutine(AddToList(newItem));                                                 //function that will add the new item to the item list in UIpanel, its in a cooroutine as I had some issues with the game not adding the manager to the list, probably something to do with get component
            if (uIImage.enabled == false)                                                       //if the UI isn't open, it will hide the new spawned item
            {
                HideNewItem(newItem);
            }
            

            RandomizeTimer();
        }
        
        foreach (ButtonManager thisItem in itemList)                                            //removes button if the time has expired
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
        ButtonManager newManager = item.GetComponent<ButtonManager>();              //new button manager
        yield return new WaitForSeconds(0.1f);

        itemList.Add(newManager);
        newManager.setShop(this);                                                   //the button manager requires to know which shop its in, this will pass this instance of shop
        newManager.AddMoveToInventoryListener();                                    //will set the button to move to inventory when clicked

        yield break;
    }
   


    public void MoveToInventory(GameObject chosenItem)
    {
        itemButton = chosenItem.GetComponent<ButtonManager>();                      //gets the button manager for the button that was pressed

        if (playerInventory.GetCurrency() >= itemButton.CheckPrice())               //if the player has enough
        {
            playerInventory.ChangeCurrency(playerInventory.GetCurrency() - itemButton.CheckPrice());        //removes the price from the players currency
            itemButton.NoLongerInShop();                                                                    //function to remove text such as the price, timer etc
            itemButton.AddSpawnInRoomListener(playerInventory);                                             //On click will now spawn the object in the room
            playerInventory.AddItemToList(chosenItem);                                                      //adds the manager to the inventory item list
            itemList.Remove(chosenItem.GetComponent<ButtonManager>());                                      //removes the manager from the shop item list
        }
    }
  
}
