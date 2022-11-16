using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Coin : Clickable
{
    private Currency UICurrency;

    // Start is called before the first frame update
    void Start()
    {
        UICurrency = GameObject.Find("UI").GetComponent<Currency>();
        inheritedFunction = AddCurrency;
    }

    public void AddCurrency()
    {
        UICurrency.AddToCurrency();
        Destroy(gameObject);
    }



}
