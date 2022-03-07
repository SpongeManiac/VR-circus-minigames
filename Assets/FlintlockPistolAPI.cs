using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlintlockPistolAPI : MonoBehaviour
{
    [SerializeField]
    private FlintlockPistol pistol;

    public void StartAnimation(string animation)
    {
        pistol.StartAnimation(animation);
    }

    public void AnimationComplete()
    {
        pistol.AnimationComplete();
    }

    public void Fire()
    {
        pistol.Fire();
    }
}
