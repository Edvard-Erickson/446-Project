using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{

    public InventoryGrid _selectedItemGrid;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(_selectedItemGrid == null) { return; } // skip if nothing is selected
        print(_selectedItemGrid.GetTileGridPos(Input.mousePosition));


    }
}
