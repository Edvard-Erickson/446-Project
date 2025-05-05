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
    public PostProcessVolume ppv;


    // Start is called before the first frame update
    void Start()
    {
        ppv = GetComponent<PostProcessVolume>();
        ppv.weight = 0f;
        sanbar.setMax(maxSan);
        StartCoroutine(tickSanity());
    }

    // Update is called once per frame
    void Update()
    {
        if (sanbar.GetSanity() <= 0)
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
                ppv.weight = 1 - (sanbar.GetSanity() / maxSan);
                sanbar.SetSanity(sanbar.GetSanity() - 1f);
                yield return new WaitForSeconds(1f);
            }
            else
            {
                ppv.weight = 1 - (sanbar.GetSanity() / maxSan);
                sanbar.SetSanity(sanbar.GetSanity() - .1f); 
                
                yield return new WaitForSeconds(1f);
            }

        }
    }
}
