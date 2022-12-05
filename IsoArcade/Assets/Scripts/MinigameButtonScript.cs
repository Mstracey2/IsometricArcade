using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MinigameButtonScript : MonoBehaviour
{
    public static MinigameButtonScript Instance;

    [SerializeField] private TMP_Text textDeal;

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
    private Camera main;
    private Vector3 originalCamposition;
    private Quaternion originalCamrotation;
    private GameObject scene;
    private Button gameButton;
    private Image buttonImage;
    private TMP_Text buttonText;
    private HideUI UI;

    private void Start()
    {
        UI = GetComponentInParent<HideUI>();
        main = Camera.main;
        gameButton = GetComponent<Button>();
        buttonImage = GetComponent<Image>();
        buttonText = GetComponentInChildren<TMP_Text>();
        gameButton.onClick.AddListener(StartMinigame);
    }

    public void StartMinigame()
    {
        if(scene != null)
        {
            MinigameManager thisManager = scene.GetComponent<MinigameManager>();
            thisManager.GetMachine(scene);
            originalCamposition = main.transform.position;
            originalCamrotation = main.transform.rotation;

            GameObject camPos = thisManager.ShowCam();
            main.transform.position = camPos.transform.position;
            main.transform.rotation = camPos.transform.rotation;
            thisManager.ShowManager().RunFunction();
            UI.Hide();
            buttonText.text = "Return";
            gameButton.onClick.RemoveAllListeners();
            gameButton.onClick.AddListener(ReturnToMain);
        }
    }

    public void ReturnToMain()
    {
        main.transform.position = originalCamposition;
        main.transform.rotation = originalCamrotation;
        UI.Show();
        buttonText.text = "Restock";
        gameButton.onClick.RemoveAllListeners();
        gameButton.onClick.AddListener(StartMinigame);
        HideButton();
    }

    public void ChangeScene(GameObject newMinigameScene)
    {
        scene = newMinigameScene;
    }

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

}
