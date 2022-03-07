using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PiratePlayBtn : VRButton
{
    protected override void onBtnReleaseIn()
    {
        base.onBtnReleaseIn();
        SceneManager.LoadScene("pirate");
    }
}
