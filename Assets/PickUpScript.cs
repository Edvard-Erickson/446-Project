using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpScript : MonoBehaviour, IInteractable
{
    private MoveScript ms;
    private InventoryController iC;

    public int itemNum;

    // Start is called before the first frame update
    void Start()
    {
        ms = FindObjectOfType<MoveScript>();
        iC = FindObjectOfType<InventoryController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void interact()
    {
        ms.OnInventory();
        iC.CreateItem(itemNum);

        Invoke("DestroyItem", .1f);
    }

    public void DestroyItem()
    {
        Destroy(gameObject);
    }
}
