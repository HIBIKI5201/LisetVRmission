using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeadEffectManager : MonoBehaviour
{
    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Granade"))
        {
            audioSource.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
