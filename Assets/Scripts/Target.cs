using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Target : MonoBehaviour
{
    public UnityEvent TargetShot = new UnityEvent();
    [SerializeField]
    Pirate pirate;
    [SerializeField]
    Animator animator;
    [SerializeField]
    AudioSource source;

    bool shot = false;
    bool animating = false;

    public void ToggleAnimating(int tf)
    {
        animating = tf >= 1;
    }

    void Start()
    {
        TargetShot.AddListener(Shot);
    }

    void Shot()
    {
        if (!shot)
        {
            //toggle shot
            shot = true;
            if (pirate != null)
            {
                pirate.Shot();
            }
            
            //play sound
            source.Play();
            //play shot animation
            animator.Play("shot");
            //trigger reset condition
            StartCoroutine("ResetCondition");
        }
    }

    protected virtual IEnumerator ResetCondition()
    {
        yield return new WaitForSeconds(5f);
        Reset();
    }

    void Reset()
    {
        if (shot)
        {
            if (pirate != null)
            {
                pirate.Reset();
            }
            
            //play sound
            source.Play();
            //play animation
            animator.Play("reset");
            //toggle shot
            shot = false;
        }
    }
}
