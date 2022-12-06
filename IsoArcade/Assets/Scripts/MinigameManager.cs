using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MinigameManager : MonoBehaviour
{
    [SerializeField] private GameObject cameraPos;                  //camera position for the minigame
    [SerializeField] private Minigame manager;                      //manager to run the inherited function
    private MiniGameTime timer;                                     //reference to the minigame timer

    
    public void RunGame(MiniGameTime newTimer)
    {
        timer = newTimer;                                               
        Mouse.Instance.mouseMode = Mouse.Instance.MouseMiniGameOne;                 //mouse mode now corrolates to the pooltable minigame
        manager.RunFunction();                                                      //will run the assigned function of the delegate
    }

    #region return Values

    public float CheckTime()                                        //returns the state of the time, used so the minigames know when its game over          
    {
        return timer.ReturnTime();
    }
    public GameObject ShowCam()                                     //returns the minigame cam position for the minigame button to move the camera
    {
        return cameraPos;
    }
    #endregion
}
