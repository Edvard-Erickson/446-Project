using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryGrid : MonoBehaviour
{
    const float _tileSizeWidth = 32f;
    const float _tileSizeHeight = 32f;

    InventoryItem[,] _inventoryItemSlot;

    RectTransform _rectTrans;

    // Start is called before the first frame update
    void Start()
    {
        _rectTrans = GetComponent<RectTransform>();
        Init(10, 4);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Init(int width, int height)
    {
        _inventoryItemSlot = new InventoryItem[width, height];
        Vector2 size = new Vector2(width * _tileSizeWidth, height * _tileSizeHeight);
        _rectTrans.sizeDelta = size;
    }

    Vector2 _posOnGrid = new Vector2();
    Vector2Int _tileGridPos = new Vector2Int();

    public Vector2Int GetTileGridPos(Vector2 mousePos)
    {
        _posOnGrid.x = mousePos.x - _rectTrans.position.x;
        _posOnGrid.y = _rectTrans.position.y - mousePos.y;

        _tileGridPos.x = (int)(_posOnGrid.x / _tileSizeWidth);
        _tileGridPos.y = (int)(_posOnGrid.y / _tileSizeHeight);

        return _tileGridPos;
    }

    public void PlaceItem(InventoryItem inventoryItem, int posX, int posY)
    {
        RectTransform rectTransform = inventoryItem.GetComponent<RectTransform>();
        rectTransform.parent = rectTransform;
        _inventoryItemSlot[posX, posY] = inventoryItem;
    }
}
