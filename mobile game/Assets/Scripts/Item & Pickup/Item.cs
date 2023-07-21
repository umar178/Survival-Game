using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Create New Item")]
public class Item : ScriptableObject
{
    public int id;
    public int ItemCount;
    public int MaxCount;
    public string ItemName;
    public Sprite Image;
}
