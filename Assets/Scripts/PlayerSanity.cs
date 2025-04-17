using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;
using static UnityEngine.GraphicsBuffer;

public class PlayerSanity : MonoBehaviour
{
    public SanityBar sanbar;
    public float maxSan;
    public GameObject monster;
    public float currSan;
    public PostProcessVolume ppv;


    // Start is called before the first frame update
    void Start()
    {
        ppv = GetComponent<PostProcessVolume>();
        ppv.weight = 0f;
        currSan = maxSan;
        sanbar.setMax(maxSan);
        StartCoroutine(tickSanity());
    }

    // Update is called once per frame
    void Update()
    {
        if (currSan <= 0)
        {
            SceneManager.LoadScene(4);
        }
    }

    IEnumerator tickSanity()
    {
        while (true)
        {
            if ((monster.transform.position - transform.position).magnitude < 5)
            {
                ppv.weight = 1 - (currSan / maxSan);
                currSan = currSan - 1f;
                sanbar.SetSanity(currSan);
                yield return new WaitForSeconds(1f);
            }
            else
            {
                ppv.weight = 1 - (currSan / maxSan);
                currSan = currSan - .1f;
                sanbar.SetSanity(currSan);
                yield return new WaitForSeconds(1f);
            }

        }
    }
}
