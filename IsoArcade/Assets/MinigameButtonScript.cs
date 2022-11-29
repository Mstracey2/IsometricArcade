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
    private GameObject UI;

    private void Start()
    {
        UI = gameObject.transform.parent.gameObject;
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
            UI.SetActive(false);
        }
    }

    public void ReturnToMain()
    {
        main.enabled = true;
        minigameCam.enabled = false;
        UI.SetActive(true);
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
