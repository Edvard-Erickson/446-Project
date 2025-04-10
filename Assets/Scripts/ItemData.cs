using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class ItemData : ScriptableObject
{
    public string _name;

    public int _width = 1;
    public int _height = 1;

    public Sprite itemIcon;
}
