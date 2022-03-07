using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetScore : MonoBehaviour
{
    [SerializeField]
    string game;
    [SerializeField]
    protected RGBDemo rgb;
    [SerializeField]
    int score = 0;
    [SerializeField]
    int[] bestScores = new int[3];
    [SerializeField]
    TMPro.TextMeshPro[] scores = new TMPro.TextMeshPro[4];

    private void Awake()
    {
        LoadScores();
        var list = new List<int>(bestScores);
        foreach(var i in list)
        {
            Debug.Log(i);
        }
        list.Sort((a, b) => b.CompareTo(a));
        Debug.Log("New order: "+list.ToArray()+"\nScore: "+score+"\nWorst: "+list[list.Count-1]);
        if (score > list[list.Count-1])
        {
            Debug.Log("New best score!");
            list.Add(score);
            list.Sort((a, b) => b.CompareTo(a));
            list.RemoveAt(list.Count-1);
        }
        
        bestScores = list.ToArray();
        //DataStore.instance.SetData(0, "scoreCurrent");
        SaveScores();
    }
    void Start()
    {
        //set text to scores
        Debug.Log("Score length: "+scores.Length);
        for (int i = 0; i < bestScores.Length; i++)
        {
            Debug.Log("index: "+i);
            scores[i].text = bestScores[i].ToString();
        }
        rgb.AddComponent(scores[0]);
        rgb.paused = false;
    }
    void SaveScores()
    {
        switch (game)
        {
            case "ball":
                DataStore.instance.SetData(bestScores, "scoreBestBall");
                break;
            case "pirate":
                DataStore.instance.SetData(bestScores, "scoreBestPirate");
                break;
        }
        
        DataStore.instance.SaveData();
    }
    void LoadScores()
    {
        var tempScore = DataStore.instance.GetData("scoreCurrent");
        switch (game)
        {
            case "ball":
                var tempBestBall = DataStore.instance.GetData("scoreBestBall");
                score = tempScore == null ? 0 : (int)tempScore.data;
                bestScores = tempBestBall == null ? new int[] { 0, 0, 0 } : (int[])tempBestBall.data;
                break;
            case "pirate":
                var tempBestPirate = DataStore.instance.GetData("scoreBestPirate");
                score = tempScore == null ? 0 : (int)tempScore.data;
                bestScores = tempBestPirate == null ? new int[] { 0, 0, 0 } : (int[])tempBestPirate.data;
                break;
        }
        
        
        
    }
}
