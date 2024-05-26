using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCore : MonoBehaviour
{
    public GameScoreManager gameScoreManager;
    public float highScore;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("GameCore");

        if (objs.Length > 1)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    public void GameCoreScoreSave()
    {
        gameScoreManager = GameObject.Find("GameSystemManager").GetComponent<GameScoreManager>();
        float nowScore = gameScoreManager.Score;
        if (nowScore > highScore)
        {
            highScore = nowScore;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
