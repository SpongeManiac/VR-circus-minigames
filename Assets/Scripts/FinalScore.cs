using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalScore : MonoBehaviour
{
    [SerializeField]
    protected TMPro.TextMeshPro score;
    [SerializeField]
    protected RGBDemo colorizer;

    private void Start()
    {
        var currentScore = DataStore.instance.GetData("scoreCurrent");
        score.text = currentScore == null ? "0" : currentScore.data.ToString();
        colorizer.AddComponent(score);
        colorizer.paused = false;
        DataStore.instance.SetData(0, "scoreCurrent");
    }
}
