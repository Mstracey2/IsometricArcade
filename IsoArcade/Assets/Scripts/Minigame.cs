using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minigame : MonoBehaviour
{

    #region Variables
    protected delegate void MinigameStart();
    protected MinigameStart inheritedFunction;         //delegate to determine what happens when the player clicks on the object, function changes by an inherited script
    #endregion

    public void RunFunction()
    {
        if (inheritedFunction != null)
        {
            inheritedFunction();
        }
    }
}
