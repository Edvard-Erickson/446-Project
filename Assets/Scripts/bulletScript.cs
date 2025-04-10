using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class bulletScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnCollisionEnter(Collision collision)
    {
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        gameObject.SetActive(false);
    }
    IEnumerator despawn()
    {
        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);

    }

    private void OnEnable()
    {
        StartCoroutine(despawn());
    }
}
