using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandUICursor : MonoBehaviour
{
    public QuickRGB rgbScript;

    private void OnEnable()
    {
        ToggleRGB();
    }

    private void OnDisable()
    {
        ToggleRGB();
    }

    public void ToggleRGB()
    {
        rgbScript.paused = !rgbScript.paused;
    }
}
