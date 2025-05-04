using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolTip : MonoBehaviour
{
    public string message;

    public void DispMessage()
    {
        ToolTipManager._instance.SetAndShow(message);
    }

    public void HideMessage()
    {
        ToolTipManager._instance.HideToolTip();
    }
}
