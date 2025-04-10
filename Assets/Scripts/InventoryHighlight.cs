using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryHighlight : MonoBehaviour
{
    public RectTransform _highlight;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetSize(InventoryItem targetItem)
    {
        Vector2 size = new Vector2();
        size.x = targetItem._itemData._width * 32;
        size.y = targetItem._itemData._height * 32;
        _highlight.sizeDelta = size;
    }

    public void SetPosition(InventoryGrid targetGrid, InventoryItem targetItem)
    {
        _highlight.SetParent(targetGrid.GetComponent<RectTransform>());

        Vector2 pos = targetGrid.CalculatePositionOnGrid(targetItem, targetItem._onGridPosX, targetItem._onGridPosY);

        _highlight.localPosition = pos;

    }
}
