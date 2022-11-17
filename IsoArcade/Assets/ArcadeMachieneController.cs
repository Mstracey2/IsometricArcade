using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ArcadeMachieneController : Clickable
{
    public GameObject coin;
    public GameObject location;
    public GameObject RemoveTimeText;

    private Mouse mouseManager;
    private float countDown;
    private float fullTime = 80;
    private float minusTimerClick = 5;
    public TMP_Text timerText;
    // Start is called before the first frame update
    void Start()
    {
        mouseManager = GameObject.Find("MouseManager").GetComponent<Mouse>();
        
        countDown = fullTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (mouseManager.CheckEditor() == false)
        {
            inheritedFunction = minusCount;
            countDown -= Time.deltaTime;
            if (countDown < 0)
            {
                countDown = 0;
                inheritedFunction = SpawnCoin;
            }
            displayTime(countDown, timerText);
        }
        else
        {
            inheritedFunction = null;
            timerText.text = "";
        }
       
    }

    void displayTime(float TimeToDisplay, TMP_Text time)
    {
        float minutes = Mathf.FloorToInt(TimeToDisplay / 60);
        float seconds = Mathf.FloorToInt(TimeToDisplay % 60);

        time.text = string.Format("{0:00}:{1:00}",minutes,seconds);
    }

    void minusCount()
    {
        countDown -= minusTimerClick;
        GameObject text = Instantiate(RemoveTimeText, timerText.gameObject.transform.position, timerText.transform.rotation);
        TMP_Text removeTimeText = text.GetComponent<TMP_Text>();
        displayTime(minusTimerClick, removeTimeText);
        StartCoroutine(MoveRemoveTimer(removeTimeText));
        
    }

    IEnumerator MoveRemoveTimer(TMP_Text text)
    {
        float objective = text.rectTransform.position.y + 1;

        while(text.rectTransform.position.y <= objective)
        { 
            text.rectTransform.position = new Vector3(text.rectTransform.position.x, text.rectTransform.position.y + 1f * Time.deltaTime, text.rectTransform.position.z);
            yield return null;
        }
        Destroy(text.gameObject);
        yield break;
    }

    public void SpawnCoin()
    {
        GameObject newCoin = Instantiate(coin, location.transform.position, location.transform.rotation);

        newCoin.GetComponent<Rigidbody>().AddForce(transform.up * 500);
        newCoin.GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-10, 10), 0, Random.Range(-10, 10)) * 50);

        countDown = fullTime;

        inheritedFunction = minusCount;
    }
}
