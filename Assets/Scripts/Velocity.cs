using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Velocity : MonoBehaviour
{
    public Vector3 velocity { get { return _velocity; } }
    Vector3 _velocity = Vector3.zero;
    Vector3 last;

    void FixedUpdate()
    {
        if (last != null)
        {
            //calculate velocity
            var delta = transform.position - last;
            var xV = delta.x / 0.02f;
            var yV = delta.y / 0.02f;
            var zV = delta.z / 0.02f;
            _velocity = new Vector3(xV, yV, zV);
        }
        last = transform.position;
    }
}
