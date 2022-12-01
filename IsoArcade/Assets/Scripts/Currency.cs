using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Currency : MonoBehaviour
{
    private float currency = 0;
    private TMP_Text UIText;

    private void Start()
    {
        UIText = GetComponentInChildren<TMP_Text>();                //gets the UI currency text
    }
    public void AddToCurrency()                                     //Adds value to currency
    {
        currency++;
        UpdateUI();
    }

    public void UpdateUI()                                         // updates the text in the UI
    {
        UIText.text = "Currency: " + currency;
    }

    public float ReturnCurrency()
    {
        return currency;
    }

}
