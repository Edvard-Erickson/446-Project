using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AN_DoorScript : MonoBehaviour, IInteractable
{
    [Tooltip("If it is false door can't be used")]
    public bool Locked = true;
    [Tooltip("It is true for remote control only")]
    public bool Remote = false;
    [Space]
    [Tooltip("Door can be opened")]
    public bool CanOpen = true;
    [Tooltip("Door can be closed")]
    public bool CanClose = true;

    public bool isOpened = false;
    [Range(0f, 4f)]
    [Tooltip("Speed for door opening, degrees per sec")]
    public float OpenSpeed = 3f;

    float distance;
    float angleView;
    Vector3 direction;

    // Hinge
    [HideInInspector]
    public Rigidbody rbDoor;
    HingeJoint hinge;
    JointLimits hingeLim;
    float currentLim;

    // collider
    BoxCollider drCollider;

    // Final door stuff
    int numAccept = 0;
    Scene currentScene;
    string sceneName;

    void Start()
    {
        rbDoor = GetComponent<Rigidbody>();
        hinge = GetComponent<HingeJoint>();
        drCollider = GetComponent<BoxCollider>();

        // For final door
        Messenger.AddListener(EventHandler.TARGET_ACCEPTING, newAccept);
        Messenger.AddListener(EventHandler.TARGET_DEACTIVE, notAccept);

        // Create a temporary reference to the current scene.
        currentScene = SceneManager.GetActiveScene();

        // Retrieve the name of this scene.
        sceneName = currentScene.name;
    }

    public void interact()
    {
        Action();
    }

    public void Action() // void to open/close door
    {
        if (!Locked)
        {
            
            // opening/closing
            if (isOpened && CanClose)
            {
                isOpened = false;
            }
            else if (!isOpened && CanOpen)
            {
                isOpened = true;
                rbDoor.AddRelativeTorque(new Vector3(0, 0, 20f));
                if (sceneName.Equals("MainScene"))
                {
                    SceneManager.LoadScene(3);
                }
            }
        
        }
    }


    private void FixedUpdate() // door is physical object
    {
        if (isOpened)
        {
            currentLim = 85f;
        }
        else
        {
            // currentLim = hinge.angle; // door will closed from current opened angle
            if (currentLim > 1f)
                currentLim -= .5f * OpenSpeed;
        }

        if (numAccept == 3 && sceneName.Equals("MainScene"))
        {
            Locked = false;
        } else
        {
            Locked = true;
        }

        // using values to door object
        hingeLim.max = currentLim;
        hingeLim.min = -currentLim;
        hinge.limits = hingeLim;
    }

    //Final door stuff
    void newAccept()
    {
        numAccept++;
    }

    void notAccept()
    {
        numAccept--;
    }
}
