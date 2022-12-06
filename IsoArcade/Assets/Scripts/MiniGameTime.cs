using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MiniGameTime : MonoBehaviour
{
    private float timer = 10;
    private bool active;
    private TMP_Text timeText;

    // Start is called before the first frame update
    void Start()
    {
        timeText = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            timer -= Time.deltaTime;                        //timer counts down
            timeText.text = timer.ToString();               //text equals new value
        }

    }

    public void Active(float time, bool activated)         //function used to activate the minigame timer, called through the minigame manager
    {
        active = activated;
        timer = time;
    }

    public float ReturnTime()                               //returns the value of timer, called for the minigame to know when time is up
    {
        return timer;
    }

}
