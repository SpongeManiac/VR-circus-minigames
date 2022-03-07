using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class ShipSpawner : MonoBehaviour
{
    public bool gameEnd { get { return _gameEnd; } }
    public int shipCount { get { return _shipCount; } }
    public int endCount { get { return _endCount; } }
    public int maxShips { get { return _maxShips; } }
    [SerializeField]
    Score score;
    [SerializeField]
    bool _gameEnd;
    [SerializeField]
    int _shipCount;
    [SerializeField]
    int _endCount;
    [SerializeField]
    int _maxShips;
    [SerializeField]
    int lastShips = 0;
    [SerializeField]
    FlintlockPistol pistol;
    [SerializeField]
    List<Track> tracks; 
    [SerializeField]
    GameObject shipPrefab;
    [SerializeField]
    int passedShips = 0;
    // Start is called before the first frame update
    void Start() { }

    public void startGame()
    {
        Debug.Log("Starting game");
        pistol.OnSelect.RemoveListener(this.startGame);
        foreach (var track in tracks)
        {
            track.spawner = this;
            track.shipAdded.AddListener(ShipAdded);
            track.shipDestroyed.AddListener(ShipRemoved);
            track.StartSpawns();
        }
    }

    void ShipAdded()
    {
        _shipCount += 1;
    }

    void ShipRemoved()
    {
        _shipCount -= 1;
        passedShips += 1;
    }

    public void EndGame()
    {
        if (!_gameEnd)
        {
            Debug.Log("Ending game");
            _gameEnd = true;
            foreach (var track in tracks)
            {
                track.lastShip.AddListener(AllShipsDestroyed);
            }
        }
    }

    void AllShipsDestroyed()
    {
        lastShips++;
        if (lastShips >= tracks.Count)
        {
            Debug.Log("All ships destroyed");
            score.SaveCurrentScore();
            SceneManager.LoadScene("EndPirate");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (passedShips >= endCount)
        {
            //end game
            EndGame();
        }   
    }
}
