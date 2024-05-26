using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplayButtonController : MonoBehaviour
{
    public ReplayManager replayManager;
    public GameObject downUI;

    void Start()
    {
        downUI.SetActive(false);
    }

    public void Replay()
    {
        replayManager.Replay();
        downUI.SetActive(false);
    }

    void Update()
    {
        
    }
}
