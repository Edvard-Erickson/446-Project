using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpashArtScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ArtTime());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator ArtTime()
    {
        yield return new WaitForSeconds(3);

        SceneManager.LoadScene(1);
    }
}
