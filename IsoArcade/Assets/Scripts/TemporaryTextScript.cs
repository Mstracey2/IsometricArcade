using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TemporaryTextScript : MonoBehaviour
{
    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        timer = 3;
    }

    // Update is called once per frame
    void Update()
    {
        if(this.gameObject.activeSelf == true)
        {
            timer -= Time.deltaTime;
            if(timer <= 0)
            {
                timer = 3;
                this.gameObject.SetActive(false);
            }
        }

    }
}
