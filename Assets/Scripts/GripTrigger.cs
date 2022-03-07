using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GripTrigger : MonoBehaviour
{
    public VRHand hand;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Hand collided with "+other.gameObject);
        if (other.TryGetComponent<Grabbable>(out var script) && script.grabbable)
        {
            hand.OnGrabbableEnter(script);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<Grabbable>(out var script))
        {
            hand.OnGrabbableExit(script);
        }
    }
}
