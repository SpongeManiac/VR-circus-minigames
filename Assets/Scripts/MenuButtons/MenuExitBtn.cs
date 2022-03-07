using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuExitBtn : VRButton
{
    protected override void onBtnReleaseIn()
    {
        base.onBtnReleaseIn();
        Application.Quit();
    }
}
