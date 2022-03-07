using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlintlockPistol : Grabbable
{
    [SerializeField]
    Animator animator;
    [SerializeField]
    OVRInput.Controller controller;

    [SerializeField]
    AudioSource source;
    [SerializeField]
    List<AudioClip> sounds;

    [SerializeField]
    ParticleSystem sparkParticle;

    [SerializeField]
    Transform shotOrigin;
    [SerializeField]
    Transform shotDirection;

    [SerializeField]
    LayerMask targetMask;

    [SerializeField]
    Score score;
    [SerializeField]
    bool animating = false;
    [SerializeField]
    bool loaded = false;
    bool stickEnabled { get { return !animating && !loaded; } }
    float stickPos { get { return OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, controller).y; } }
    bool triggerEnabled { get { return !animating && loaded; } }
    float triggerPos { get { return OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, controller); } }
    [SerializeField]
    bool stopped = false;
    private void Start()
    {
        sparkParticle.Stop();
    }
    [SerializeField]
    bool triggerFirst = true;
    [SerializeField]
    bool stickFirst = true;

    public void Update()
    {
        
        if (grabbed)
        {
            if (stopped)
            {
                //check for current enabled to reset
                if ((stickEnabled && stickPos > -0.1) || (triggerEnabled && triggerPos < 0.1))
                {
                    stopped = false;
                }
            }
            else
            {
                if (stickEnabled)
                {
                    //reset trigger
                    triggerFirst = true;
                    //check if stick was already pressed some on the first check
                    if (stickFirst)
                    {
                        //not first check anymore
                        stickFirst = false;
                        if (stickPos < -0.3f)
                        {
                            animator.Play("unloaded");
                            stopped = true;
                            return;
                        }
                    }
                    //map stick pos to animation pos
                    Debug.Log("Mapping stick pos");
                    var animTime = Map(0, -0.92f, 0, 1, stickPos);
                    animator.speed = 0;
                    animator.Play("load", 0, animTime);
                }
                if (triggerEnabled)
                {
                    //reset stick
                    stickFirst = true;
                    //check if trigger was pressed some on the first go
                    if (triggerFirst)
                    {
                        triggerFirst = false;
                        if (triggerPos > 0.3f)
                        {
                            animator.Play("loaded");
                            stopped = true;
                            return;
                        }
                    }
                    //map trigger pos to animation
                    var animTime = Map(0, 0.92f, 0, 1, triggerPos);
                    animator.speed = 0;
                    animator.Play("trigger", 0, animTime);
                }
            }
        }
    }



    public override void GrabbedBy(VRHand hand)
    {
        base.GrabbedBy(hand);
        controller = grabbedBy.controller.controller;
    }

    public override void Drop()
    {
        base.Drop();
    }

    void Sparks()
    {
        sparkParticle.Play();
    }

    public void PlaySound()
    {

    }

    public void StartAnimation(string animation)
    {
        switch (animation)
        {
            case "loaded":
                Load();
                break;
        }
        animating = true;
        animator.Play(animation);
        animator.speed = 1;
    }

    public void AnimationComplete()
    {
        animating = false;
    }

    public void Load()
    {
        //play loaded noise
        source.clip = sounds[2];
        source.Play();
        loaded = true;
    }

    public void Fire()
    {
        Debug.Log("Fired!");
        //play click sound
        source.clip = sounds[1];
        source.Play();
        //play gunshot
        source.clip = sounds[0];
        source.Play();
        Sparks();
        Raycast();
        loaded = false;
        stopped = true;
        animating = false;
    }

    void Raycast()
    {
        Debug.Log("Casting ray!");
        
        var start = shotOrigin.position;
        var end = shotDirection.position;
        Debug.Log("Ray start: " + start + "\nRay end: " + end);

        //get direction of ray
        var direction = -(start - end).normalized;
        Debug.DrawRay(start, direction, Color.red, 60);
        Ray shot = new Ray(start, direction);
        if (Physics.Raycast(shot, out RaycastHit hit, 300f, targetMask))
        {

            //target was hit
            Debug.Log("hit");
            //get hitbox script
            var script = hit.collider.gameObject.GetComponent<TargetHit>();
            if (script == null)
            {
                return;
            }
            //get hit pos on hitbox
            var hitPos = script.targetTransform.InverseTransformPoint(hit.point);
            //get dist
            var distance = Vector2.Distance(hitPos, Vector2.zero);
            //check to make sure hit point was within radius
            var radius = script.targetRadius;
            if (distance <= radius)
            {
                //calculate score based on distance
                //score is based inversely on distance, map function helps a ton.
                script.Shot();
                var points = Mathf.RoundToInt(Map(0, radius, 20, 1, distance));
                Debug.Log("Score: "+points);
                score.addScore(points);
            }
        }
    }

    float Map(float a1, float a2, float b1, float b2, float val)
    {
        return b1 + (val - a1) * (b2 - b1) / (a2 - a1);
    }
}
