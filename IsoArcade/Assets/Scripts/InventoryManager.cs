using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : PanelUI
{

    [SerializeField] private Currency playerCurrency;
    [SerializeField] private GameObject spawner;
    [SerializeField] private GameObject minigame;

    // Start is called before the first frame update
    void Start()
    {
        uIImage = GetComponent<Image>();
    }
    public float GetCurrency()
    {
        return playerCurrency.ReturnCurrency();
    }



    public void AddItemToList(GameObject item)                              // adds new button manager from spawned object to the Inventory List
    {
        item.transform.SetParent(this.gameObject.transform, false);         //makes the object its own parent so its an independant object from the shop

        ButtonManager newManager = item.GetComponent<ButtonManager>();
        itemList.Add(newManager);
        HideNewItem(item.gameObject);                                       // function that hides the new button if the inventory is not open
    }

    public void SpawnObject(ButtonManager obj)
    {
        GameObject clone = Instantiate(obj.Getmachine(), spawner.transform.position, spawner.transform.rotation);           //spawns new object in room
        
        string name = obj.GetName();
        float income = obj.GetIncome();
        itemList.Remove(obj);                                                                                               // removes the button manager from the inventory list
        Destroy(obj.gameObject);                                                                                            // destroys button from inventory
        ArcadeMachieneController cloneController = clone.GetComponent<ArcadeMachieneController>();
        cloneController.SetNameAndIncome(name,income);                                                                       // sets the variables of the income and name of the machine so the machine know how much it should produce in currency
        cloneController.SetMinigame(minigame);
    }

    #region Variable Changers

    public void ChangeSpawner(GameObject newSpawn)
    {
        spawner = newSpawn;
    }

    public void ChangeCurrency(float value)
    {
        playerCurrency.SetCurrency(value);
    }
    #endregion
}
