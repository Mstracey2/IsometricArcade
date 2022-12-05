using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonImage : MonoBehaviour
{
    private Button button;
    private Image buttonImage;
    [SerializeField] private TMP_Text buttonText;

    private void Start()
    {
        button = GetComponent<Button>();
        buttonImage = GetComponent<Image>();
    }


    public void HideButton()
    {
        button.enabled = false;
        buttonImage.enabled = false;
        buttonText.enabled = false;
    }

    public void ShowButton()
    {
        button.enabled = true;
        buttonImage.enabled = true;
        buttonText.enabled = true;
    }
}
