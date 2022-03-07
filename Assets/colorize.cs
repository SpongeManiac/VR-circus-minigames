using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class colorize : MonoBehaviour
{
    [SerializeField]
    private RGBDemo colorizer;
    [SerializeField]
    private Component thingToColorize;
    // Start is called before the first frame update
    void Start()
    {
        colorizer.AddComponent(thingToColorize);
    }
}
