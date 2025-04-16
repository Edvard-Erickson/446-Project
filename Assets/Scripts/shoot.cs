using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//Kept old bullet code incase we want it for rocks 
// Everything runs off of IInteractable interface

interface IInteractable
{
    public void interact();
}

public class shoot : MonoBehaviour
{
    public GameObject _bullet;
    public Transform _bulletSpawnPt;
    ObjectPool _bulletPool;
    Transform _transform;

    RaycastHit _hit;
    // Start is called before the first frame update
    void Start()
    {

        //_bulletPool = new ObjectPool(_bullet, true, 5);
        //_transform = transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnShoot(InputValue val)
    {

        //GameObject bullet = _bulletPool.GetObject();
        //bullet.transform.position = _bulletSpawnPt.position;
        //bullet.GetComponent<Rigidbody>().velocity = Camera.main.transform.forward * 10;

        if (Physics.Raycast(_bulletSpawnPt.transform.position, _bulletSpawnPt.transform.TransformDirection(Vector3.forward), out _hit, 3f))
        {
            if (_hit.collider.gameObject.TryGetComponent(out IInteractable interactObj))
            {
                interactObj.interact();
            }
        }
    }
}
