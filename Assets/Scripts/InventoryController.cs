using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class InventoryController : MonoBehaviour
{

    public InventoryGrid _selectedItemGrid;

    InventoryItem _selectedItem;
    InventoryItem _overlapItem;
    RectTransform _itemRect;

    public List<ItemData> _items;
    public GameObject _itemPrefab;
    public Transform _canvasTransform;

    InventoryHighlight _inventoryHighlight;

    public GameObject _canvas;


    private void Awake()
    {
        _inventoryHighlight = GetComponent<InventoryHighlight>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _canvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        ItemIconDrag();

        if (Input.GetKeyDown(KeyCode.Q))
        {
            CreateRandomItem();
        }

        if (_selectedItemGrid == null) { return; } // skip if nothing is selected
        //print(_selectedItemGrid.GetTileGridPos(Input.mousePosition));

        //HandleHighlight();

        if (Input.GetMouseButtonDown(0))
        {
            LeftMouseButtonPress();
        }

    }

    /*InventoryItem _itemToHighlight;
    private void HandleHighlight()
    {
        Vector2Int positionOnGrid = GetTileGridPosition();
        if ( _selectedItemGrid == null)
        {
            _itemToHighlight = _selectedItemGrid.GetItem(positionOnGrid.x, positionOnGrid.y);

            if (_itemToHighlight != null)
            {

                _inventoryHighlight.SetSize(_itemToHighlight);
                _inventoryHighlight.SetPosition(_selectedItemGrid, _itemToHighlight);
            }
        }
        else
        {

        }
    }*/

    private void CreateRandomItem()
    {
        InventoryItem inventoryItem = Instantiate(_itemPrefab).GetComponent<InventoryItem>();
        _selectedItem = inventoryItem;
        _itemRect = inventoryItem.GetComponent<RectTransform>();
        _itemRect.SetParent(_canvasTransform);

        int selectedItemID = UnityEngine.Random.Range(0, _items.Count);
        inventoryItem.Set(_items[selectedItemID]);
    }

    private void LeftMouseButtonPress()
    {
        Vector2Int tileGridPosition = _selectedItemGrid.GetTileGridPos(Input.mousePosition);

        if (_selectedItem == null)
        {
            PickUpItem(tileGridPosition);
        }
        else
        {
            PlaceItem(tileGridPosition);
        }
    }

    /*public Vector2Int GetTileGridPosition()
    {
        Vector2 position = Input.mousePosition;

        if (_selectedItem != null)
        {
            position.x -= (_selectedItem._itemData._width - 1) * 32 / 2;
            position.y += (_selectedItem._itemData._height - 1) * 32 / 2;
        }

        Vector2Int tileGridPosition = _selectedItemGrid.GetTileGridPos(position);
        return tileGridPosition;
    }*/

    private void PlaceItem(Vector2Int tileGridPosition)
    {
        bool complete = _selectedItemGrid.PlaceItem(_selectedItem, tileGridPosition.x, tileGridPosition.y, ref _overlapItem);
        if (complete)
        {
            _selectedItem = null;
            if(_overlapItem != null)
            {
                _selectedItem = _overlapItem;
                _overlapItem = null;
                _itemRect = _selectedItem.GetComponent<RectTransform>();
            }
        }
        
    }

    private void PickUpItem(Vector2Int tileGridPosition)
    {
        _selectedItem = _selectedItemGrid.PickUpItem(tileGridPosition.x, tileGridPosition.y);
        if (_selectedItem != null)
        {
            _itemRect = _selectedItem.GetComponent<RectTransform>();
        }
    }

    private void ItemIconDrag()
    {
        if (_selectedItem != null)
        {
            _itemRect.position = Input.mousePosition;
        }
    }

    public void CreateItem(int selectItem)
    {
        InventoryItem inventoryItem = Instantiate(_itemPrefab).GetComponent<InventoryItem>();
        _selectedItem = inventoryItem;
        _itemRect = inventoryItem.GetComponent<RectTransform>();
        _itemRect.SetParent(_canvasTransform);

        inventoryItem.Set(_items[selectItem]);
    }
}
