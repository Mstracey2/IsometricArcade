using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeRoom : Clickable
{
    [SerializeField] private GameObject changeCamPos;
    [SerializeField] private GameObject newSpawner;
    [SerializeField] private InventoryManager playerInv;

    private void Start()
    {
        inheritedFunction = ChangeToRoom;
    }

    public void ChangeToRoom()
    {
        Camera.main.transform.position = changeCamPos.transform.position;
        playerInv.ChangeSpawner(newSpawner);
    }
}
