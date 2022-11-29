using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MinigameButtonScript : MonoBehaviour
{
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
    private Camera main;
    private Camera minigameCam;
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
    }

    public void StartMinigame()
    {
        if(scene != null)
        {
            minigameCam = scene.GetComponentInChildren<Camera>();
            main.enabled = false;
            minigameCam.enabled = true;
            UI.Hide();
            buttonText.text = "Return";
            gameButton.onClick.RemoveAllListeners();
            gameButton.onClick.AddListener(ReturnToMain);
        }
    }

    public void ReturnToMain()
    {
        main.enabled = true;
        minigameCam.enabled = false;
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
    }

    public void HideButton()
    {
        gameButton.enabled = false;
        buttonImage.enabled = false;
        buttonText.enabled = false;
    }

}
