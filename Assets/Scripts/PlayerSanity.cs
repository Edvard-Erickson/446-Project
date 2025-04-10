using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.GraphicsBuffer;

public class PlayerSanity : MonoBehaviour
{
    public SanityBar sanbar;
    public float maxSan;
    public GameObject monster;
    public float currSan;

    Scene currentScene = SceneManager.GetActiveScene();

    // Start is called before the first frame update
    void Start()
    {
        currSan = maxSan;
        sanbar.setMax(maxSan);
        StartCoroutine(tickSanity());
    }

    // Update is called once per frame
    void Update()
    {
        print(currentScene.name);
        if(currSan <= 0)
        {
            SceneManager.LoadScene(4);
        }
    }

    IEnumerator tickSanity()
    {
        while (true)
        {
            if ((monster.transform.position - transform.position).magnitude < 5){
                currSan = currSan - 1f;
                sanbar.SetSanity(currSan);
                yield return new WaitForSeconds(1f);
            } else
            {
                currSan = currSan - .1f;
                sanbar.SetSanity(currSan);
                yield return new WaitForSeconds(1f);
            }

        }
    }
}
