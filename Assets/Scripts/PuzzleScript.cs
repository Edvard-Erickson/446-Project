using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Net;
using UnityEngine;

public class puzzelScript : MonoBehaviour, IInteractable
{
    public int numStates;
    int currentState = 0;
    public int acceptState;
    MeshRenderer mr;
    public int boxNum;

    public Material green;
    public Material yellow;
    public Material red;

    bool accepting = false;

    Animator anim;
    void updateState1()
    {
        if (boxNum == 1)
        {
            currentState++;
        }
    }

    void updateState2()
    {
        if (boxNum == 2)
        {
            currentState++;
        }

    }

    void updateState3()
    {

        if (boxNum == 3)
        {
            currentState++;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        mr = GetComponent<MeshRenderer>();
        mr.material = red;

        Messenger.AddListener(EventHandler.TARGET_ONE_UPDATE, updateState1);
        Messenger.AddListener(EventHandler.TARGET_TWO_UPDATE, updateState2);
        Messenger.AddListener(EventHandler.TARGET_THREE_UPDATE, updateState3);
        Messenger.AddListener(EventHandler.ALL_TORCH_LIT, activePuzzle);

        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (currentState == acceptState)
        {
            mr.material = green;
            if (!accepting)
            {
                accepting = true;
                Messenger.Broadcast(EventHandler.TARGET_ACCEPTING);
            }

        }
        else if (currentState == acceptState - 1)
        {
            mr.material = yellow;
        }
        else
        {
            mr.material = red;
            if (accepting)
            {
                accepting = false;
                Messenger.Broadcast(EventHandler.TARGET_DEACTIVE);
            }
        }

        if (currentState == numStates)
        {
            currentState = 0;
        }
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.collider.CompareTag("player"))
    //    {
    //        if (boxNum == 1)
    //        {
    //            Messenger.Broadcast(EventHandler.TARGET_TWO_UPDATE);
    //        }
    //        else if (boxNum == 2)
    //        {
    //            Messenger.Broadcast(EventHandler.TARGET_THREE_UPDATE);
    //        }
    //        else if (boxNum == 3)
    //        {
    //            Messenger.Broadcast(EventHandler.TARGET_ONE_UPDATE);
    //        }

    //        currentState++;


    //    }
    //}

    public void interact()
    {
        anim.SetTrigger("ButtonPress");
        if (boxNum == 1)
        {
            Messenger.Broadcast(EventHandler.TARGET_TWO_UPDATE);
        }
        else if (boxNum == 2)
        {
            Messenger.Broadcast(EventHandler.TARGET_THREE_UPDATE);
        }
        else if (boxNum == 3)
        {
            Messenger.Broadcast(EventHandler.TARGET_ONE_UPDATE);
        }

        currentState++;
    }

    public void activePuzzle()
    {
        gameObject.SetActive(true);
        print("All Lit");
    }
}
