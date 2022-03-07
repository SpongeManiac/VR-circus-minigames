using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShaderColors : MonoBehaviour
{
    public List<Renderer> renderers = new List<Renderer>();
    Color _color = new Color(1,1,1,1);
    public Color color
    {
        get { return _color; }
        set { SetColors(value); }
    }

    void SetColors(Color c)
    {
        Debug.Log("Setting colors");
        _color = c;
        foreach (var r in renderers)
        {
            r.material.SetColor("_FirstOutlineColor", c);
        }
    }

    public void SetOutline(float f)
    {
        Debug.Log("Setting outline");
        foreach (var r in renderers)
        {
            r.material.SetFloat("_FistOutlineWidth", f);
        }
    }
}
