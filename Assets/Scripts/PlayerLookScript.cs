using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLookScript : MonoBehaviour
{
    Transform _transform;
    public GameObject _head;
    float yAngle = 0;
    MoveScript _moveScript;
    // Start is called before the first frame update

    public Texture _reticule;
    void Start()
    {
        _transform = transform;
        _moveScript = gameObject.GetComponent<MoveScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!_moveScript._isInventoryOpen)
        {
            float xAngle = _transform.localEulerAngles.y
                + Mouse.current.delta.value.x * .5f;

            yAngle -= 0.5f * Mouse.current.delta.value.y;
            //yAngle = _head.transform.localEulerAngles.x
            //    + Mouse.current.delta.value.y * -.5f;

            yAngle = Mathf.Clamp(yAngle, -80, 80);

            _transform.localEulerAngles = new Vector3(0, xAngle, 0);
            _head.transform.localEulerAngles = new Vector3(yAngle, 0, 0);
        }

    }
    private void OnGUI()
    {
        if (!_moveScript._isInventoryOpen)
        {
            int size = 30;
            float posX = Camera.main.pixelWidth / 2 - size / 4;
            float posY = Camera.main.pixelHeight / 2 - size / 2;
            GUI.Label(new Rect(posX, posY, size, size), _reticule);
        }
    }
}
