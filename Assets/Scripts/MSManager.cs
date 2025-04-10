using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MSManager : MonoBehaviour
{
    int numAccept = 0;
    // Start is called before the first frame update
    void Start()
    {
        Messenger.AddListener(EventHandler.TARGET_ACCEPTING, newAccept);
        Messenger.AddListener(EventHandler.TARGET_DEACTIVE, notAccept);

    }

    // Update is called once per frame
    void Update()
    {
        if(numAccept == 3)
        {
            print("here");
            SceneManager.LoadScene(3);
        }
    }

    void newAccept()
    {
        numAccept++;
    }

    void notAccept()
    {
        numAccept--;
    }
}
