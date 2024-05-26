using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Vector2 firstPosition;
    private float enemyMovementAmount;
    [SerializeField] private float enemyMoveSpeed;
    public float formationPosition;
    EnemyManager enemyManager;

    [SerializeField] int guardianPatrolDilayTime;
    private bool guardianPatrolStartDilay = false;
    private bool guardianPatrolStartCoroutine;
    [SerializeField] bool guardianPatrol;

    public void Initialize()
    {
        firstPosition = new Vector2(transform.position.x, transform.position.y);

         enemyManager = GetComponent<EnemyManager>();

        formationPosition = enemyManager.formationPosition;

        transform.position = new Vector2(transform.position.x, enemyManager.formationPosition % 10 - 5);
        enemyMovementAmount = (Mathf.Ceil(enemyManager.formationPosition / 10)) + 2;
    }

    private IEnumerator guardianPatrolStart()
    {
        guardianPatrolStartCoroutine = true;
        yield return new WaitForSeconds(guardianPatrolDilayTime);
        guardianPatrolStartDilay = true;
        guardianPatrol = Random.Range(0, 2) == 0;
    }

    void Update()
    {
        if (firstPosition.x - transform.position.x < enemyMovementAmount)
        {
            transform.Translate(Vector3.left * Time.deltaTime * enemyMoveSpeed);
        }
        else if (enemyManager.enemyKind == 2)
        {
            if(guardianPatrolStartCoroutine == false)
            {
                StartCoroutine(guardianPatrolStart());
            }

            if (guardianPatrolStartDilay == true)
            {
                if (guardianPatrol == true)
                {
                    transform.Translate(Vector3.up * Time.deltaTime * enemyMoveSpeed / 2);

                    if (transform.position.y >= 4)
                    {
                        guardianPatrol = false;
                    }
                }

                if (guardianPatrol == false)
                {
                    transform.Translate(Vector3.down * Time.deltaTime * enemyMoveSpeed / 2);

                    if (transform.position.y <= -4)
                    {
                        guardianPatrol = true;
                    }
                }
            }
        }
    }
}
