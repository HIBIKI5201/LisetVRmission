using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShieldManager : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    [SerializeField] public float currentHeallth;

    EnemyManager enemyManager;

    // Start is called before the first frame update
    void Start()
    {
        currentHeallth = maxHealth;

        Transform parentTransform = transform.parent;
        enemyManager = parentTransform.GetComponent<EnemyManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            currentHeallth -= 2;
        }
        if (currentHeallth <= 0)
        {
            enemyManager.ShieldRepairStarter();
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
