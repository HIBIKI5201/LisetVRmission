using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GranadeTextManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void UpdateGranadeText(int HavingGranade)
    {
        GetComponent<TextMeshProUGUI>().text = "Å~"Å@+ HavingGranade;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
