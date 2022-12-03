using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    [SerializeField]
    private Item[] infoList;
    private Item selectedInfo;

    private string itemName;
    private Sprite itemImage;
    private float potIncome;
    public float price;
    private float time;
    public bool pauseTime = false;
    private GameObject machine;

    [SerializeField]
    private Button buttonEvent;

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

    // Start is called before the first frame update
    void Start()
    {
        selectedInfo = infoList[Random.Range(0, infoList.Length)];
        itemName = selectedInfo.itemName;
        itemImage = selectedInfo.imageSprite;
        potIncome = Random.Range(1, 20);
        price = Mathf.Round(Random.Range(potIncome * 2, potIncome * 3) * 10);
        time = Random.Range(20, 300);

        textName.text = itemName;
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
                Destroy(this.gameObject);
            }
            else
            {
                displayTime(time, texttime);
            }
        }
       

    }

    void displayTime(float TimeToDisplay, TMP_Text time)                                // function to display the time in minutes instead of a single int
    {
        float minutes = Mathf.FloorToInt(TimeToDisplay / 60);                           // divides the int by 60 to get minutes
        float seconds = Mathf.FloorToInt(TimeToDisplay % 60);                           // gets the remainder of division of 60 to get the seconds

        time.text = string.Format("{0:00}:{1:00}", minutes, seconds);                     //formats the string
    }

    public float CheckTime()
    {
        return time;
    }

    public float CheckPrice()
    {
        return price;
    }

    public void setShop(ShopManager thisShop)
    {
        shopRef = thisShop;
    }

    public void MoveLocation()
    {
      shopRef.MoveToInventory(this.gameObject);  
    }

    public void NoLongerInShop()
    {
        textPrice.text = null;
        texttime.text = null;
        pauseTime = true;
        RemoveListeners();
    }

    public void AddMoveToInventoryListener()
    {
        buttonEvent.onClick.AddListener(MoveLocation);
    }

    public void AddSpawnInRoomListener(InventoryManager playerInv)
    {
        buttonEvent.onClick.AddListener(() => playerInv.SpawnObject(this));
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

    public void RemoveListeners()
    {
        buttonEvent.onClick.RemoveAllListeners();
    }
}
