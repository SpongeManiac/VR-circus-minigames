using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LineColor : MonoBehaviour
{
    public Material lineMaterial;
    public LineRenderer lineRenderer;
    public Color color
    {
        get { return lineRenderer.material.color; }
        set { lineRenderer.material.color = value; }
    }

    private void Awake()
    {
        lineMaterial.color = color;
        lineRenderer.material = lineMaterial;
    }
}
