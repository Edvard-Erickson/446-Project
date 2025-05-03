using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MoveScript : MonoBehaviour
{
    CharacterController _controller;

    public GameObject _camera;

    public float _rotationAmount = 0;
    public float _movementSpeed = 5;
    Vector2 _moveVal = Vector2.zero;

    public bool _isInventoryOpen = false;
    public GameObject _inventoryCanvas;

    Transform _transform;

    // Sprint
    public StaminaScript stimBar;
    public float maxStam;
    public float currStam;

    // Start is called before the first frame update
    void Start()
    {
        _transform = transform;
        _controller = GetComponent<CharacterController>();
        Cursor.visible = false;  // Hide the cursor
        Cursor.lockState = CursorLockMode.Locked;  // Lock the cursor to the center of the screen (optional)

       stimBar.setMax(maxStam);

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.LeftShift) && stimBar.GetStamina() > 0 && _moveVal.magnitude >0)
        {
            _controller.Move(_transform.rotation *
                (new Vector3(10 * _moveVal.x, -5f, 10 * _moveVal.y) * Time.deltaTime));

            stimBar.SetStamina(stimBar.GetStamina() - .1f);
        } 
        else if (Input.GetKey(KeyCode.LeftShift))
        {
            _controller.Move(_transform.rotation *
                (new Vector3(5 * _moveVal.x, -5f, 5 * _moveVal.y) * Time.deltaTime));
        } 
        else
        {
            _controller.Move(_transform.rotation *
                (new Vector3(5 * _moveVal.x, -5f, 5 * _moveVal.y) * Time.deltaTime));

            if(stimBar.GetStamina() < maxStam)
            {
                stimBar.SetStamina(stimBar.GetStamina() + .1f);
            }
        }
    }

    private void OnMove(InputValue value)
    {
        _moveVal = value.Get<Vector2>();


    }


    public void OnInventory()
    {
        // Toggle inventory visibility
        _isInventoryOpen = !_isInventoryOpen;
        _inventoryCanvas.SetActive(_isInventoryOpen); // Show or hide the inventory UI canvas

        // Toggle mouse cursor visibility
        if (_isInventoryOpen)
        {
            Cursor.visible = true;  // Show the cursor
            Cursor.lockState = CursorLockMode.None;  // Unlock the cursor
        }
        else
        {
            Cursor.visible = false;  // Hide the cursor
            Cursor.lockState = CursorLockMode.Locked;  // Lock the cursor to the center of the screen (optional)
        }
    }
}
