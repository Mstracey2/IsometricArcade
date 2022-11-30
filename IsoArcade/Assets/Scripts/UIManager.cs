using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public float timer;
    [SerializeField]
    private GameObject buttonTemplate;
    private List<ButtonManager> itemList = new List<ButtonManager>();
    

    private Image uIImage;


    // Start is called before the first frame update
    void Start()
    {
        timer = 10;
        uIImage = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if(itemList.Count < 5)
        {
            timer -= Time.deltaTime;
        }        
        if(timer <= 0 && itemList.Count < 5)
        {
            GameObject newItem = Instantiate(buttonTemplate, this.gameObject.transform);
            StartCoroutine(AddToList(newItem.GetComponent<ButtonManager>()));
            if (uIImage.enabled == false)
            {
                HideNewItem(newItem);
            }
            timer = 5;
        }
        
        foreach (ButtonManager thisItem in itemList)
        {
            if (thisItem.CheckTime() <= 0)
            {
                itemList.Remove(thisItem);
                break;
            }
        }

    }

    public void RandomizeTimer()
    {
        timer = Random.Range(20, 300);
    }

    IEnumerator AddToList(ButtonManager item)
    {
        yield return new WaitForSeconds(0.1f);

        itemList.Add(item);

        yield break;
    }

    public void HideUI()
    {
        uIImage.enabled = false;
        foreach (ButtonManager thisItem in itemList)
        {
            GameObject button = thisItem.gameObject;
            button.GetComponent<Image>().enabled = false;
            foreach (Transform child in button.transform)
            {
                child.gameObject.SetActive(false);
            }
        }

    }

    public void HideNewItem(GameObject newItem)
    {
        
        newItem.GetComponent<Image>().enabled = false;
        foreach (Transform child in newItem.transform)
        {
            child.gameObject.SetActive(false);
        }
    }

    public void ShowUI()
    {
        uIImage.enabled = true;
        foreach (ButtonManager thisItem in itemList)
        {
            GameObject button = thisItem.gameObject;
            button.GetComponent<Image>().enabled = true;
            foreach (Transform child in button.transform)
            {
                child.gameObject.SetActive(true);
            }
        }
    }
}
