using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Coin : Clickable
{
    private Currency UICurrency;  //reference to the UI  

    // Start is called before the first frame update
    void Start()
    {
        UICurrency = GameObject.Find("UI").GetComponent<Currency>(); //upon creation, it will find the UI with the script that changes the currency
        inheritedFunction = AddCurrency;                             //clickable delegate changes to add currencey function
    }

    public void AddCurrency()
    {
        UICurrency.SetCurrency(UICurrency.ReturnCurrency() + 1);             //function that adds to currencey on the UI
        Destroy(gameObject);                    //destroys coin
    }



}
