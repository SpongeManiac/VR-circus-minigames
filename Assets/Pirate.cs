using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pirate : MonoBehaviour
{
    public Animator animator;
    public int type;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Shot()
    {
        animator.speed = 1;
        switch (type)
        {
            case 0:
                animator.Play("standing_shot");
                break;
            case 1:
                animator.Play("barrel_shot");
                break;
        }
    }

    public void Reset()
    {
        Debug.Log("Resetting pirate of type "+type);
        animator.speed = 1;
        switch (type)
        {
            case 0:
                animator.Play("standing_reset");
                break;
            case 1:
                Debug.Log("Playing 'barrel_reset'");
                animator.Play("barrel_reset");
                break;
        }
    }
}
