using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    #region Variables
    [SerializeField]
    private Item[] infoList;                            //gets list of possible machines
    private Item selectedInfo;                          //selected machine

    private string itemName;                            
    private Sprite itemImage;
    private float potIncome;
    public float price;
    private float time;
    public bool pauseTime = false;                      //timer pause
    private GameObject machine;                         //ref to the actual machine object

    [SerializeField]
    private Button buttonEvent;                         //ref to all the button text
    [SerializeField]
    private TMP_Text textName;
    [SerializeField]
    private Image textImage;
    [SerializeField]
    private TMP_Text textIncome;
    [SerializeField]
    private TMP_Text textPrice;
    [SerializeField]
    private TMP_Text texttime;

    public ShopManager shopRef;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        selectedInfo = infoList[Random.Range(0, infoList.Length)];          //randomly selects info from the list
        itemName = selectedInfo.itemName;
        itemImage = selectedInfo.imageSprite;
        potIncome = Random.Range(1, 20);                                    // the Income, price and time are randomized. The price value becomes more expensive due to the income
        price = Mathf.Round(Random.Range(potIncome * 2, potIncome * 3) * 10);
        time = Random.Range(20, 300);

        textName.text = itemName;                                           //sets all the text correctly
        textImage.sprite = itemImage;
        textIncome.text = "Income: " + potIncome.ToString();
        textPrice.text = "Price: " + price.ToString();
        texttime.text = time.ToString();

        buttonEvent = GetComponent<Button>();

        machine = selectedInfo.machine;
    }

    // Update is called once per frame
    void Update()
    {
        if(pauseTime == false)
        {
            time -= Time.deltaTime;

            if (time <= 0)
            {
                Destroy(this.gameObject);               //if the timer reaches 0, the item disappears from the shop
            }
            else
            {
                displayTime(time, texttime);            // if the timer is still going, then it carrys on displaying the time
            }
        }
       

    }

    void displayTime(float TimeToDisplay, TMP_Text time)                                // function to display the time in minutes instead of a single int
    {
        float minutes = Mathf.FloorToInt(TimeToDisplay / 60);                           // divides the int by 60 to get minutes
        float seconds = Mathf.FloorToInt(TimeToDisplay % 60);                           // gets the remainder of division of 60 to get the seconds

        time.text = string.Format("{0:00}:{1:00}", minutes, seconds);                   
    }

    #region Checkers
    public float CheckTime()
    {
        return time;
    }

    public float CheckPrice()
    {
        return price;
    }

    public GameObject Getmachine()
    {
        return machine;
    }

    public string GetName()
    {
        return itemName;
    }

    public float GetIncome()
    {
        return potIncome;
    }
    #endregion

    public void setShop(ShopManager thisShop)                                            //gets the reference to the shop
    {
        shopRef = thisShop;
    }

    public void MoveLocation()                                                           //process of moving item from shop to inventory
    {
        shopRef.MoveToInventory(this.gameObject);  
    }

    public void NoLongerInShop()                                                         // anything on the button thats no longer relevent due to not being in the sho is set to null and the timer is paused
    {
        textPrice.text = null;
        texttime.text = null;
        pauseTime = true;
        RemoveListeners();                                                              
    }


    #region Change Listeners
    public void AddMoveToInventoryListener()
    {
        buttonEvent.onClick.AddListener(MoveLocation);
    }

    public void AddSpawnInRoomListener(InventoryManager playerInv)                          //when the button is clicked, object will spawn in room
    {
        buttonEvent.onClick.AddListener(() => playerInv.SpawnObject(this));
    }
    public void RemoveListeners()
    {
        buttonEvent.onClick.RemoveAllListeners();
    }
    #endregion
}
