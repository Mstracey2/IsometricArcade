using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameManager : MonoBehaviour
{
    [SerializeField] private GameObject cameraPos;
    [SerializeField] private Minigame manager;
    private GameObject machine;
    public GameObject ShowCam()
    {
        return cameraPos;
    }

    public Minigame ShowManager()
    {
        return manager;
    }

    public void GetMachine(GameObject thisMachine)
    {
        machine = thisMachine;
    }

    public void IncreaseIncomeOnMachine()
    {
        machine.GetComponent<ArcadeMachieneController>().TwoXIncome();
    }

}
