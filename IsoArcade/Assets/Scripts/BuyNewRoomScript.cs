using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyNewRoomScript : Clickable
{
    [SerializeField] private GameObject newRoomUI;
    [SerializeField] private AmountCheck checker;
    [SerializeField] private GameObject arrow;
    public float roomCost;

    public void Start()
    {
        inheritedFunction = AskUser;
    }


    public void AskUser()
    {
        newRoomUI.SetActive(true);
        checker.DisplayCostAndGetButton(roomCost, this.gameObject, arrow, newRoomUI);
    }
}
