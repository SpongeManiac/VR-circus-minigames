using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuRestartBtn : VRButton
{
    protected override void onSelectOut(VRHand hand)
    {
        SceneManager.LoadScene("Game");
    }
}
