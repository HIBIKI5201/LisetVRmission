using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GranadePinManager : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(ExplosionTimer());
    }

    private IEnumerator ExplosionTimer()
    {
        yield return new WaitForSeconds(4);
        Destroy(gameObject);
    }

        // Update is called once per frame
        void Update()
    {
        
    }
}
