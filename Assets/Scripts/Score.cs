using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Score : MonoBehaviour
{
    [SerializeField]
    protected TMPro.TextMeshProUGUI textMesh;
    [SerializeField]
    public int score
    {
        get { return _score; }
        protected set { _score = value; textMesh.text = scoreTxt; }
    }
    protected int _score = 0;
    [SerializeField]
    protected RGBDemo rgb;

    protected string scoreTxt
    {
        get { return string.Format("Score: {0}", (score).ToString()); }
    }


    // Start is called before the first frame update
    private void Start()
    {
        rgb.AddComponent(textMesh);
        rgb.paused = false;
        score = 0;
    }

    public void addScore(int score)
    {
        Debug.Log("Adding to score: "+score);
        this.score += score;
    }

    public void ClearScore()
    {
        score = 0;
    }

    public void LoadScore(string name)
    {
        DataStore.instance.GetData(name);
    }
    public void SaveCurrentScore(string name)
    {
        DataStore.instance.SetData(score, name);
    }
    public void SaveCurrentScore()
    {
        DataStore.instance.SetData(score, "scoreCurrent");
    }
}
