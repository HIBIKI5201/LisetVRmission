using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResultTextManager : MonoBehaviour
{
    public GameScoreManager ScoreManager;
    public GameCore gameCore;
    // Start is called before the first frame update
    void Start()
    {
        gameCore = GameObject.Find("GameCore").GetComponent<GameCore>();
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<TextMeshProUGUI>().text = "Mission Completed \n Score " + ScoreManager.Score.ToString() + "\n High Score is " + gameCore.highScore.ToString();
    }
}
