using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CsharpVersion : MonoBehaviour
{
    // Start is called before the first frame update
    private void Awake()
    {
        Debug.Log(typeof(string).Assembly.ImageRuntimeVersion);
    }
}
