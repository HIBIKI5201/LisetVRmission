using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartridgeManager : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        float randomValue = UnityEngine.Random.Range(100f, 200f);

        rb.AddForce(-transform.right * randomValue);
        rb.AddForce(transform.up * randomValue);

        StartCoroutine(DestroyTimer());
    }

    private IEnumerator DestroyTimer()
    {
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
