using System.Collections;
using System.Collections.Generic;
using System.Data;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MSManager : MonoBehaviour
{
    int numAccept = 0;
    int numTorchLit = 0;

    public TextMeshProUGUI objectiveTxt;

    // Start is called before the first frame update
    void Start()
    {
        Messenger.AddListener(EventHandler.TARGET_ACCEPTING, newAccept);
        Messenger.AddListener(EventHandler.TARGET_DEACTIVE, notAccept);
        Messenger.AddListener(EventHandler.TORCH_LIT, torchLit);

    }

    // Update is called once per frame
    void Update()
    {

        if (numTorchLit == 6)
        {
            objectiveTxt.text = "Locks Left: " + (3 - numAccept);
            Messenger.Broadcast(EventHandler.ALL_TORCH_LIT);

            if (numAccept == 3)
            {
                objectiveTxt.text = "Escape";
            }
        }
        else
        {
            objectiveTxt.text = "Unlit Torches Left: " + (6 - numTorchLit);
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

    void torchLit()
    {
        numTorchLit++;
    }
}
