using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetHit : MonoBehaviour
{
    [SerializeField]
    Target target;
    [SerializeField]
    SphereCollider targetCollider;
    public Transform targetTransform;
    public float targetRadius { get { return targetCollider.radius; } }
    public void Shot()
    {
        target.TargetShot.Invoke();
    }
}