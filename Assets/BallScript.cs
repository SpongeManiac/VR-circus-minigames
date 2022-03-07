using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    [SerializeField]
    protected AudioPlayer player;
    private void OnCollisionEnter(Collision collision)
    {
        var vel = Mathf.Abs(collision.relativeVelocity.magnitude);
        if (vel > 6)
        {
            player.PlaySoundHit(2);
        }
        else if (collision.relativeVelocity.magnitude > 2)
        {
            player.PlaySoundHit(3);
        }
        else
        {
            player.PlaySoundHit(4);
        }
    }
}
