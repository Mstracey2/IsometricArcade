using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyNewRoomScript : Clickable
{
    #region Variables
    [SerializeField] private GameObject newRoomUI;
    [SerializeField] private AmountCheck checker;
    [SerializeField] private GameObject arrow;
    public float roomCost;
    #endregion

    public void Start()
    {
        inheritedFunction = AskUser;                                                    //inherited delegate from clickable script is the ask user function
    }


    public void AskUser()                                                               //asks the User whether they want to buy the room they clicked on
    {
        newRoomUI.SetActive(true);
        checker.DisplayCostAndGetButton(roomCost, this.gameObject, arrow, newRoomUI);
    }
}
