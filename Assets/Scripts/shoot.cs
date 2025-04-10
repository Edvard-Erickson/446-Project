using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class shoot : MonoBehaviour
{
    public GameObject _bullet;
    public Transform _bulletSpawnPt;
    ObjectPool _bulletPool;
    Transform _transform;
    // Start is called before the first frame update
    void Start()
    {
        _bulletPool = new ObjectPool(_bullet, true, 5);
        _transform = transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnShoot(InputValue val)
    {
        //print("bang");
        GameObject bullet = _bulletPool.GetObject();
        bullet.transform.position = _bulletSpawnPt.position;
        bullet.GetComponent<Rigidbody>().velocity = Camera.main.transform.forward * 10;
        //_transform.rotation * new Vector3(0, 0, 10);
        //_head.transform.eulerAngles
    }
}
