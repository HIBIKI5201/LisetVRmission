using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyBulletComponent : MonoBehaviour
{
    [SerializeField] private float bulletSpeed;
    private Vector2 fieldOut = new Vector2(16, 8);

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if(gameObject.tag != "EnemyBulletTwo" || !Input.GetKey(KeyCode.Space))
            {
                Destroy(gameObject);
            }
        }
    }

    void Update()
    {
        Vector2 movement = new Vector2(-1, 0);
        transform.Translate(movement * bulletSpeed * Time.deltaTime);

        if (transform.position.x >= fieldOut.x || transform.position.x <= fieldOut.x * -1 || transform.position.y >= fieldOut.y || transform.position.y <= fieldOut.y * -1)
        {
            Destroy(gameObject);
        }
    }
}
