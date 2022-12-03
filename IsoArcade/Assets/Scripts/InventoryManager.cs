using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : PanelUI
{

    [SerializeField] private Currency playerCurrency;
    [SerializeField] private GameObject spawner;

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

    public void ChangeCurrency(float value)
    {
        playerCurrency.SetCurrency(value);
    }

    public void AddItemToList(GameObject item)
    {
        item.transform.SetParent(this.gameObject.transform, false);

        ButtonManager newManager = item.GetComponent<ButtonManager>();
        itemList.Add(newManager);
        HideNewItem(item.gameObject);
    }

    public void SpawnObject(ButtonManager obj)
    {
        GameObject clone = Instantiate(obj.Getmachine(), spawner.transform.position, spawner.transform.rotation);
        string name = obj.GetName();
        float income = obj.GetIncome();
        itemList.Remove(obj);
        Destroy(obj.gameObject);
        clone.GetComponent<ArcadeMachieneController>().SetNameAndIncome(name,income);
    }

    public void ChangeSpawner(GameObject newSpawn)
    {
        spawner = newSpawn;
    }

    
}
