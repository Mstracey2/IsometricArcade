using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class AmountCheck : MonoBehaviour
{
    #region Variables
    [SerializeField] private Currency playersCurrency;               //what currency we are checking
    [SerializeField] private GameObject fundsText;                   //the text in game;

    private TMP_Text amountText;                                    //ref to the gameobject text;

    private float currentCost;                                      //current cost of the room
    private GameObject currentRoomButton;                           //button that the player pressed
    private GameObject currentArrowButton;                          //button the pressed button swaps to
    private GameObject newRoomText;                                 //UI pop up;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        amountText = GetComponent<TMP_Text>();
        transform.parent.gameObject.SetActive(false);
    }

    public void DisplayCostAndGetButton(float cost, GameObject button, GameObject arrowButton, GameObject UIRoom)               //passes through all the required values and displays them onto the UI correctly
    {
        amountText.text = "£" + cost;
        currentCost = cost;
        currentRoomButton = button;
        currentArrowButton = arrowButton;
        newRoomText = UIRoom;
    }

   public void CheckPlayerCurrency()                                                                                            //runs from the yes button
    {
        if(playersCurrency.ReturnCurrency() >= currentCost)
        {
            playersCurrency.SetCurrency(playersCurrency.ReturnCurrency() - currentCost);                                        // takes away the amount that the room costs
            currentCost = 0;
            currentRoomButton.SetActive(false);                                                                                 //switches the buttons from paying button to move room button
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
