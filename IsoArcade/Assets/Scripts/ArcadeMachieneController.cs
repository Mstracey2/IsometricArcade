using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ArcadeMachieneController : Clickable
{
    #region 
    [SerializeField]
    private GameObject coin;                //reference to the coin prefab that the script dispenses
    [SerializeField]
    private GameObject location;            // reference to the transform of where the coin is instanciated
    [SerializeField]
    private GameObject RemoveTimeText;      //reference to the time remover prefab
    private float countDown;                // counter
    private float fullTime = 0;            // length of time it takes for the machine to dispense
    private float minusTimerClick = 5;      // amount the player can click off the time
    [SerializeField]
    private TMP_Text timerText;             // reference of the timer
    [SerializeField]
    private GameObject MinigameScene;

    private float machineIncome = 1;
    private string arcadeName;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        fullTime = 300 * machineIncome;
        countDown = fullTime;                                                           // makes the countdown equal to the starting count
        
    }

    // Update is called once per frame
    void Update()                                                           
    {
        CountDown();
    }

    void displayTime(float TimeToDisplay, TMP_Text time)                                // function to display the time in minutes instead of a single int
    {

        
        float seconds = (int)(TimeToDisplay % 60);                           // gets the remainder of division of 60 to get the seconds
        float minutes = (int)(TimeToDisplay / 60) % 60;                           // divides the int by 60 to get minutes
        float hours = (int)(TimeToDisplay / 3600) % 24;                                   // divides the mins by 60 to get hours

        time.text = string.Format("{0:0}:{1:00}:{2:00}", hours,minutes,seconds);                     //formats the string
    }

    void minusCount()
    {
        countDown -= minusTimerClick;                                                   //removes the time from the countdown
        GameObject text = Instantiate(RemoveTimeText, timerText.gameObject.transform.position, timerText.transform.rotation);           //instantiates removal time in the position of the time text
        TMP_Text removeTimeText = text.GetComponent<TMP_Text>();                                                                        // gets the text component from the new remove text
        displayTime(minusTimerClick, removeTimeText);                                                                                   // displays the removed time in minutes format
        StartCoroutine(MoveRemoveTimer(removeTimeText));                                                                                // a couroutine used to make the timer move up
        
    }

    IEnumerator MoveRemoveTimer(TMP_Text text)                                                                                          // function used for moving the remove timer upwards
    {
        float objective = text.rectTransform.position.y + 1;                                                                            // makes the objective position the text transform y + 1

        while(text.rectTransform.position.y <= objective)                                                                               // while the text transform has not reached the objective
        { 
            text.rectTransform.position = new Vector3(text.rectTransform.position.x, text.rectTransform.position.y + 1f * Time.deltaTime, text.rectTransform.position.z);       //changes the transform of y to move upwards
            yield return null;          //waits for next frame
        }
        Destroy(text.gameObject);                   //when the objective transform is reached, the text is deleted
        yield break;                                //breaks the coroutine
    }

    public void SpawnCoin()                                     //function used to spawn coin
    {
        for (int i = 0; i < machineIncome; i++)
        {
            GameObject newCoin = Instantiate(coin, location.transform.position, location.transform.rotation);                           //instantiates a coin in the spawner location

            newCoin.GetComponent<Rigidbody>().AddForce(transform.up * 500);                                                             // applys force upwards
            newCoin.GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-10, 10), 0, Random.Range(-10, 10)) * 50);              // applys force in a random direction on the x and z axis
        }
        

        countDown = fullTime;                                                                                                       // resets the countdown

        inheritedFunction = minusCount;                                                                                             //click function returns to removing time from timer
    }


    public void CountDown()
    {
        if (Mouse.Instance.CheckEditor() == false)                                        //checks if the game isn't in editor mode
        {
            countDown -= Time.deltaTime;                                                // starts the countdown
            if (countDown < 0)                                                          // if the countdown is 0, the display will stay 0 and the inherited function will spawn a coin
            {
                countDown = 0;
                inheritedFunction = SpawnCoin;   
            }
            else
            {
                inheritedFunction = minusCount;
            }

            displayTime(countDown, timerText);                                          // displays the time correctly formatted on the timer
        }
        else
        {
            inheritedFunction = null;                                                   // defaults to this if the game is in editor mode
            timerText.text = "";
        }
    }

    public GameObject ShowMinigame()
    {
        return MinigameScene;
    }


    public void SetNameAndIncome(string name, float income)
    {
        arcadeName = name;
        machineIncome = income;
    }
}
