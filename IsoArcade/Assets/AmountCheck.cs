using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class AmountCheck : MonoBehaviour
{
    [SerializeField] private Currency playersCurrency;
    [SerializeField] private GameObject fundsText; 

    private TMP_Text amountText;

    private float currentCost;
    private GameObject currentRoomButton;
    private GameObject currentArrowButton;
    private GameObject newRoomText;

    // Start is called before the first frame update
    void Start()
    {
        amountText = GetComponent<TMP_Text>();
        transform.parent.gameObject.SetActive(false);
        
    }

    public void DisplayCostAndGetButton(float cost, GameObject button, GameObject arrowButton, GameObject UIRoom)
    {
        amountText.text = "£" + cost;
        currentCost = cost;
        currentRoomButton = button;
        currentArrowButton = arrowButton;
        newRoomText = UIRoom;
    }

   public void CheckPlayerCurrency()
    {
        if(playersCurrency.ReturnCurrency() >= currentCost)
        {
            playersCurrency.SetCurrency(playersCurrency.ReturnCurrency() - currentCost);
            currentCost = 0;
            currentRoomButton.SetActive(false);
            currentRoomButton = null;
            currentArrowButton.SetActive(true);
            currentArrowButton = null;
            newRoomText.SetActive(false);
        }
        else
        {
            fundsText.SetActive(true);
        }
    }
}
