using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField]
    private TMP_Text TimeText;
    // Start is called before the first frame update
    void Start()
    {
        TimeText = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        TimeText.text = DateTime.Now.ToString("hh:mm:ss");
    }
}
