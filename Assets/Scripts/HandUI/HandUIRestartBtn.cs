using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HandUIRestartBtn : HandUIItem
{
    private void Awake()
    {
        if (SceneManager.GetActiveScene().name == "Title")
        {
            //user is already in the main menu, disable self
            Disable();
        }
        base.Awake();
    }

    protected override void onSelectOut(VRHand hand)
    {
        if (isEnabled)
        {
            base.onSelectOut(hand);
        }
    }

    public void LoadBall()
    {
        SceneManager.LoadScene("Game");
    }

    public void LoadPirate()
    {
        SceneManager.LoadScene("pirate");
    }
}
