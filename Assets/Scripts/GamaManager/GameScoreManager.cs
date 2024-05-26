using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScoreManager : MonoBehaviour
{
    public ScoreText ScoreText;
    public float Score;
    void Start()
    {
        
    }

    public void ScoreMathf(float enemyKind)
    {
        if(enemyKind == 1)
        {
            Score += 100;
        } else if(enemyKind == 2)
        {
            Score += 500;
        } else if(enemyKind == 3)
        {
            Score += 250;
        }
        ScoreText.UpdateScoreText();
    }

    void Update()
    {
        
    }
}
