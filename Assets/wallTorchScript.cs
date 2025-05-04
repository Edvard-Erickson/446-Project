using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wallTorchScript : MonoBehaviour, IInteractable
{
    public InventoryGrid IG;
    public GameObject _fire;

    // Start is called before the first frame update
    void Start()
    {
        IG = FindObjectOfType<InventoryGrid>();

        _fire.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void interact()
    {
        if (IG.GetTorchState() && !_fire.active)
        {
            _fire.SetActive(true);
            Messenger.Broadcast(EventHandler.TORCH_LIT);
        }
    }
}
