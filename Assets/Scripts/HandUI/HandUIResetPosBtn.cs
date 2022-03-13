using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandUIResetPosBtn : HandUIItem
{
    protected override void onSelectOut(VRHand hand)
    {
        if (isEnabled)
        {
            base.onSelectOut(hand);
            var controller = player.GetComponent<CharacterController>();
            var newLocation = new Vector3(0f, controller.height - controller.radius, 0f);
            Debug.Log("New player location: "+newLocation);
            controller.enabled = false;
            player.transform.position = newLocation;
            controller.enabled = true;
        }
    }
}
