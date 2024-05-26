using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletComponet : MonoBehaviour
{
    [SerializeField] private float bulletSpeed;
    private Vector2 fieldOut = new Vector2(Mathf.Abs(16), Mathf.Abs(8));

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
        if (other.CompareTag("EnemyShield")){
            Destroy(gameObject);
        }
    }

    void Update()
    {
        Vector2 movement = new Vector2(1, 0);
        transform.Translate(movement * bulletSpeed * Time.deltaTime);

        if (transform.position.x >= fieldOut.x || transform.position.y >= fieldOut.y)
        {
            Destroy(gameObject);
        }
    }
}
