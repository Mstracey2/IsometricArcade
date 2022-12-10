using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MinigameButtonScript : MonoBehaviour
{
    #region Instanstiate Singleton
    public static MinigameButtonScript Instance;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    #endregion

    [SerializeField] private MiniGameTime timer;
    [SerializeField] private TMP_Text textDeal;
    [SerializeField] private GameObject MinigameInstructions;

   
    #region Variables
    private Camera main;                            //ref to the camere, used to move the camera to each minigame scene
    private Vector3 originalCamposition;            //stores the original position and rotation of the camera to return to when the minigame is finished
    private Quaternion originalCamrotation;
    private GameObject scene;                       //stores the minigame scen
    private GameObject machine;                     //stores the machine the minigame is connected to, used to multiply the income if the player is succesful
    private Button gameButton;                      //stores the button, used to change the on click event listeners
    private Image buttonImage;                      // image of button, used to change the visibility of the button
    private TMP_Text buttonText;                    //text of button (visibility again)
    private HideUI UI;                              //ref to Hide UI scripty, Hides the rest of the UI
    private MinigameManager getSceneManager;
    #endregion

    private void Start()
    {
        UI = GetComponentInParent<HideUI>();
        main = Camera.main;
        gameButton = GetComponent<Button>();
        buttonImage = GetComponent<Image>();
        buttonText = GetComponentInChildren<TMP_Text>();
        gameButton.onClick.AddListener(StartMinigameScene);              // assigns a listener to the button to start the minigame
    }

    public void StartMinigameScene()
    {
        if(scene != null)                                           //if a minigame is assigned
        {
            getSceneManager = scene.GetComponent<MinigameManager>();
            originalCamposition = main.transform.position;          // stores the original camera transform
            originalCamrotation = main.transform.rotation;

            GameObject camPos = getSceneManager.ShowCam();              //changes the camera position to the minigame cam position
            main.transform.position = camPos.transform.position;
            main.transform.rotation = camPos.transform.rotation;
            MinigameInstructions.SetActive(true);
          
        }
    }

    public void MinigameStart()
    {
        timer.gameObject.SetActive(true);                       //sets the minigame timer active to show how much time the player has to complete the task
        timer.Active(10, true);                                  //activates with how much time to give the player
        getSceneManager.RunGame(timer);                          //function which runs the process of game setup and start
        UI.Hide();                                              //hides all UI to lock the player into the minigame
        buttonText.text = "Return";                             //changes the restock minigame button to return to the main scene as indication with this text
        gameButton.onClick.RemoveAllListeners();                //removes the current listeners on the button
        gameButton.onClick.AddListener(ReturnToMain);           //makes a listner to the button to return to the main scene
    }

    public void ReturnToMain()
    {
        main.transform.position = originalCamposition;              //camera returns back to its origninal position
        main.transform.rotation = originalCamrotation;
        UI.Show();                                                  //returns the UI
        buttonText.text = "Restock";                                //minigame button is back to "Restock"
        gameButton.onClick.RemoveAllListeners();                    //removes all listeners again
        gameButton.onClick.AddListener(StartMinigameScene);              //puts the buttons listener to start the minigame again
        Mouse.Instance.mouseMode = Mouse.Instance.MouseMainMode;    //changes the mouse mode, the Mouse has different types of modes, minigames, editor and main
        timer.Active(0,false);                                      //stops and resets timer
        timer.gameObject.SetActive(false);
        HideButton();                                               //hides the minigame button
    }

    public void ChangeScene(GameObject connectedMachine)            //gets infomation on the minigame connected to the machine and also the machine itself
    {
        scene = connectedMachine.GetComponent<ArcadeMachieneController>().ShowMinigame();
        machine = connectedMachine;
    }

    public void TimesIncome()                                       //function to call the machine controller to multiply the Income
    {
        machine.GetComponent<ArcadeMachieneController>().TwoXIncome();
    }

    // changes the visibility of the minigame button
    #region Button Condition
    public void ShowButton()
    {
        gameButton.enabled = true;
        buttonImage.enabled = true;
        buttonText.enabled = true;
        textDeal.enabled = true;
    }

    public void HideButton()
    {
        gameButton.enabled = false;
        buttonImage.enabled = false;
        buttonText.enabled = false;
        textDeal.enabled = false;
    }
    #endregion  

}
