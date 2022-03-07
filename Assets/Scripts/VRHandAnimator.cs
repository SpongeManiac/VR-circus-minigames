using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRHandAnimator : MonoBehaviour
{
    [SerializeField]
    protected VRController controller;
    [SerializeField]
    protected RuntimeAnimatorController rightHand;
    [SerializeField]
    protected RuntimeAnimatorController leftHand;
    [SerializeField]
    protected Animator animator;
    /*
    Poses:
        Required Animations:
    Fist <--- First!
        Flex SubIndex Group
        Flex Index Finger
        Flex Thumb
    Pointing
        Flex SubIndex Group
        Flex Thumb
    Thumbs Up
        Flex SubIndex Group
        Flex Index Finger
    'OK'
        OK Animation
    Finger Gun
        Flex SubIndex Group
        Pow!
    */

    float subIndexFlex = 0;
    float indexFlex = 0;
    bool OK = false;
    bool gun = false;
    bool thumbsUp = false;

    bool checkTrigger = false;
    bool checkSubIndex = false;

    bool right = false;
        
        
    // Start is called before the first frame update
    void Start()
    {
        //get which hand
        if (animator.runtimeAnimatorController == right)
        {
            right = true;
        }
        controller.gripPressIn.AddListener(GripPress);
        controller.gripPressOut.AddListener(GripRelease);
        controller.triggerTouchIn.AddListener(TriggerTouchBegin);
        controller.thumbTouchIn.AddListener(ThumbTouchBegin);
    }

    // Update is called once per frame
    void Update()
    {
        //animator.SetFloat("", );
    }

    void TriggerTouchBegin()
    {
        controller.triggerTouchIn.RemoveListener(ThumbTouchBegin);
        checkTrigger = !checkTrigger;
        controller.triggerTouchOut.AddListener(ThumbTouchEnd);
    }
    void TriggerTouchEnd()
    {
        controller.triggerTouchOut.RemoveListener(ThumbTouchEnd);
    }

    void ThumbTouchBegin()
    {
        controller.thumbTouchIn.RemoveListener(ThumbTouchBegin);
        controller.thumbTouchOut.AddListener(ThumbTouchEnd);
    }
    void ThumbTouchEnd()
    {
        controller.thumbTouchOut.RemoveListener(ThumbTouchEnd);
    }

    void GripPress()
    {
        controller.gripPress.RemoveListener(GripPress);
        //activate SubIndexgroup
        subIndexFlex = OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger);
        controller.gripPressOut.AddListener(GripRelease);
    }
    void GripRelease()
    {
        controller.gripPressOut.RemoveListener(GripRelease);
    }
}
