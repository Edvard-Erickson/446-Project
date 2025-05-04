using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryGrid : MonoBehaviour
{
    public float _tileSizeWidth = 32f;
    public float _tileSizeHeight = 32f;

    InventoryItem[,] _inventoryItemSlot;

    RectTransform rectTransform;

    public int _gridSizeWidth = 10;
    public int _gridSizeHeight = 4;

    public GameObject _torch;

    public bool torchIsActive = false;


    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        Init(_gridSizeWidth, _gridSizeHeight);

        _torch.SetActive(false);

        /*InventoryItem inventoryItem = Instantiate(prefab).GetComponent<InventoryItem>();
        PlaceItem(inventoryItem, 1, 2);*/
    }

    // Update is called once per frame
    void Update()
    {
        // Loop through each element in the 2D array
        for (int x = 0; x < _inventoryItemSlot.GetLength(0); x++)
        {
            for (int y = 0; y < _inventoryItemSlot.GetLength(1); y++)
            {
                // Check if there is an item in this slot
                if (_inventoryItemSlot[x, y] != null)
                {
                    // Compare the name of the current item to the given itemNameToCheck
                    if (_inventoryItemSlot[x, y]._itemData._name == "torch")
                    {
                        _torch.SetActive(true);
                        torchIsActive = true;
                    }
                }
            }
        }
    }

    private void Init(int width, int height)
    {
        _inventoryItemSlot = new InventoryItem[width, height];
        Vector2 size = new Vector2(width * _tileSizeWidth, height * _tileSizeHeight);
        rectTransform.sizeDelta = size;
    }

    Vector2 _posOnGrid = new Vector2();
    Vector2Int _tileGridPos = new Vector2Int();

    public Vector2Int GetTileGridPos(Vector2 mousePos)
    {
        _posOnGrid.x = mousePos.x - rectTransform.position.x;
        _posOnGrid.y = rectTransform.position.y - mousePos.y;

        _tileGridPos.x = (int)(_posOnGrid.x / _tileSizeWidth);
        _tileGridPos.y = (int)(_posOnGrid.y / _tileSizeHeight);

        return _tileGridPos;
    }

    public bool PlaceItem(InventoryItem inventoryItem, int posX, int posY, ref InventoryItem overlapItem)
    {
        if (BoundryCheck(posX, posY, inventoryItem._itemData._width, inventoryItem._itemData._height) == false)
        {
            return false;
        }

        if (OverlapCheck(posX, posY, inventoryItem._itemData._width, inventoryItem._itemData._height, ref overlapItem) == false)
        {
            overlapItem = null;
            return false;
        }

        if (overlapItem != null)
        {
            CleanGridReference(overlapItem);
        }

        RectTransform rectTransform2 = inventoryItem.GetComponent<RectTransform>();
        rectTransform2.SetParent(rectTransform);

        for (int x = 0; x < inventoryItem._itemData._width; x++)
        {
            for (int y = 0; y < inventoryItem._itemData._height; y++)
            {
                _inventoryItemSlot[posX + x, posY + y] = inventoryItem;
            }
        }

        inventoryItem._onGridPosX = posX;
        inventoryItem._onGridPosY = posY;
        Vector2 position = CalculatePositionOnGrid(inventoryItem, posX, posY);

        rectTransform2.localPosition = position;
        return true;
    }

    public Vector2 CalculatePositionOnGrid(InventoryItem inventoryItem, int posX, int posY)
    {
        Vector2 position = new Vector2();
        position.x = posX * 32;
        position.y = -(posY * 32);
        return position;
    }

    private bool OverlapCheck(int posX, int posY, int width, int height, ref InventoryItem overlapItem)
    {
        for(int x = 0; x < width; x++)
        {
            for(int y = 0; y < height; y++)
            {
                if (_inventoryItemSlot[posX + x, posY + y] != null)
                {
                    if(overlapItem == null)
                    {
                        overlapItem = _inventoryItemSlot[posX + x, posY + y];
                    }
                    else
                    {
                        if(overlapItem != _inventoryItemSlot[posX + x, posY + y])
                        {
                            return false;
                        }
                    }
                }
            }
        }

        return true;
    }

    public InventoryItem PickUpItem(int x, int y)
    {
        InventoryItem toReturn = _inventoryItemSlot[x, y];

        if (toReturn == null) { return null; } //exit gate

        CleanGridReference(toReturn);

        return toReturn;
    }

    private void CleanGridReference(InventoryItem item)
    {
        for (int ix = 0; ix < item._itemData._width; ix++)
        {
            for (int iy = 0; iy < item._itemData._height; iy++)
            {
                _inventoryItemSlot[item._onGridPosX + ix, item._onGridPosY + iy] = null;
            }
        }
    }

    bool PositionCheck(int posX, int posY)
    {
        if(posX < 0 || posY < 0)
        {
            return false;
        }

        if(posX >= _gridSizeWidth || posY >= _gridSizeHeight)
        {
            return false;
        }

        return true;
    }

    bool BoundryCheck(int posX, int posY, int width, int height)
    {
        if(PositionCheck(posX, posY) == false) { return false; }

        posX += width-1;
        posY += height-1;

        if(PositionCheck(posX, posY) == false) { return false; }

        return true;
    }

    internal InventoryItem GetItem(int x, int y)
    {
        return _inventoryItemSlot[x, y];
    }

    public bool GetTorchState()
    {
        return torchIsActive;
    }
}
