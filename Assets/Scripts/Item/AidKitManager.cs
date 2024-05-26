using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AidKitManager : MonoBehaviour
{
    private LisetManager lisetManager;
    [SerializeField] int AidKitTimeLimit;
    private bool getDelay = true;

    void Start()
    {
        lisetManager = GameObject.Find("Body").GetComponent<LisetManager>();

        Invoke("GetDelay", 0.5f);
        Invoke("AutoGetTime", AidKitTimeLimit);

    }

    private void GetDelay()
    {
        getDelay = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet") && getDelay == false)
        {
            lisetManager.GetAidKit(false);
            Destroy(gameObject);
        }

        if (other.CompareTag("Granade") && getDelay == false)
        {
            CircleCollider2D circleCollider = other.GetComponent<CircleCollider2D>();
            if (circleCollider.enabled == true)
            {
                lisetManager.GetAidKit(true);
                Destroy(gameObject);
            }
        }
    }

    private void AutoGetTime()
    {
        lisetManager.GetAidKit(true);
        Destroy(gameObject);
    }

        void Update()
    {
        
    }
}
