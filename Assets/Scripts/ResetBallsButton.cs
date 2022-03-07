using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetBallsButton : ButtonPress
{
    [SerializeField]
    protected GameManager script = GameManager.instance;
    protected override void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Layer: " + other.gameObject.layer);
        if (other.CompareTag("ButtonHead"))
        {
            Debug.Log("Trigger enter");
            script.ResetGame();
        }
    }
}
