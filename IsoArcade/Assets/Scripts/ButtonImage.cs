using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonImage : MonoBehaviour
{
    #region Variables
    private Button button;
    private Image buttonImage;
    [SerializeField] private TMP_Text buttonText;
    #endregion

    private void Start()
    {
        button = GetComponent<Button>();
        buttonImage = GetComponent<Image>();
    }


    #region ButtonState                                 
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
    #endregion                                  // sets the visiblity of the button
}
