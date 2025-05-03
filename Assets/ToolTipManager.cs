using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.VersionControl;
using UnityEngine;

public class ToolTipManager : MonoBehaviour
{
    public static ToolTipManager _instance;

    public TextMeshProUGUI _textComp;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Input.mousePosition;
    }

    public void SetAndShow(string tip)
    {
        gameObject.SetActive(true);
        _textComp.text = tip;
    }

    public void HideToolTip()
    {
        gameObject.SetActive(false);
        _textComp.text = "";
    }
}
