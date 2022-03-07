using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public BallManager ballManager;
    public SpawnBasket basketSpawner;
    public Score score;
    public RGBDemo rgb;
    public BallExit ballExit;

    // Start is called before the first frame update

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        DataStore.instance.SetData(0, "scoreCurrent");
    }

    public void ResetGame()
    {
        ballManager.ResetBalls();
        basketSpawner.RepositionBaskets();
        score.ClearScore();
        ballExit.ResetCount();
    }

    public void EndGame()
    {
        
        StartCoroutine(StopGame());
    }

    IEnumerator StopGame()
    {
        yield return new WaitForSeconds(3);
        Debug.Log("Game ended.");
        DataStore.instance.SetData(score.score, "scoreCurrent");
        DataStore.instance.SaveData();
        SceneManager.LoadScene("EndBall");
    }
}
