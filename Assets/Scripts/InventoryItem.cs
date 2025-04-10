using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour
{

    public ItemData _itemData;

    public int _onGridPosX;
    public int _onGridPosY;


    internal void Set(ItemData itemData)
    {
        _itemData = itemData;

        GetComponent<Image>().sprite = itemData.itemIcon;

        Vector2 size = new Vector2();
        size.x = itemData._width * 32f;
        size.y = itemData._height * 32f;
        GetComponent<RectTransform>().sizeDelta = size;
     }

}
