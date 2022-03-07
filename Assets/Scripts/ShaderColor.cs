using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShaderColor : MonoBehaviour
{
    [SerializeField]
    protected Renderer renderer;
    public Color color
    {
        get { return renderer.material.GetColor("_FirstOutlineColor"); }
        set { renderer.material.SetColor("_FirstOutlineColor", value); }
    }
    public float outlineWidth
    {
        get { return renderer.material.GetFloat("_FirstOutlineWidth"); }
        set { renderer.material.SetFloat("_FirstOutlineWidth", value); }
    }
}
