using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeRoom : Clickable
{
    #region Variables
    [SerializeField] private GameObject changeCamPos;
    [SerializeField] private GameObject newSpawner;
    [SerializeField] private InventoryManager playerInv;
    #endregion

    private void Start()
    {
        inheritedFunction = ChangeToRoom;
    }

    public void ChangeToRoom()                                                  //swiches room the player is choosing to go to
    {
        Camera.main.transform.position = changeCamPos.transform.position;
        playerInv.ChangeSpawner(newSpawner);
    }
}
