using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinSceneScript : MonoBehaviour
{
    public TMP_Text menuButtonText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene(1);
    }
}
