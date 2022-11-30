using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Item")]
public class Item : ScriptableObject
{
    public string itemName;
    public Image imageSprite;
}
