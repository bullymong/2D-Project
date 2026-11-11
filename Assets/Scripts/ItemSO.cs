using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item")]
public class ItemSO : ScriptableObject
{
    public string itemName;
    public string itemDescription;
    public Sprite icon;
}
