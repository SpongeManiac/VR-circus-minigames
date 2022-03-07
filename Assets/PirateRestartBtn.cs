using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PirateRestartBtn : VRButton
{
    protected override void onBtnReleaseIn()
    {
        base.onBtnReleaseIn();
        SceneManager.LoadScene("pirate");
    }
}
