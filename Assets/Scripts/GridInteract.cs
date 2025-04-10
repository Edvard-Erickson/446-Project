using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(InventoryGrid))]

public class GridInteract : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    InventoryController _inventoryController;
    InventoryGrid _invenGrid;

    private void Awake()
    {
        _inventoryController = FindObjectOfType(typeof(InventoryController)) as InventoryController;
        _invenGrid = GetComponent<InventoryGrid>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _inventoryController._selectedItemGrid = _invenGrid;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _inventoryController._selectedItemGrid = null;
    }

    
}
