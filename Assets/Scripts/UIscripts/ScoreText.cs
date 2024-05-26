using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreText : MonoBehaviour
{
    public GameScoreManager ScoreManager;
    

    void Start()
    {
        UpdateScoreText();
    }

    public void UpdateScoreText()
    {
        GetComponent<TextMeshProUGUI>().text = "Score " + ScoreManager.Score.ToString();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
