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
        UIText = GetComponentInChildren<TMP_Text>();
    }
    public void AddToCurrency()
    {
        currency++;
        UpdateUI();
    }

    public void UpdateUI()
    {
        UIText.text = "Currency: " + currency;
    }

}
