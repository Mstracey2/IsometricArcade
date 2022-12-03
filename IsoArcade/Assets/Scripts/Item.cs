using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Item")]
public class Item : ScriptableObject
{
    public string itemName;
    public Sprite imageSprite;
    public GameObject machine;
}
