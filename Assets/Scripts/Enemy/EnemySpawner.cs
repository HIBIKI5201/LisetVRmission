using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject enemyBulletPrefab;
    public EnemySpawner enemySpawner;
    public MyGunManager gunManager;
    public GameScoreManager gameScoreManager;
    [SerializeField] private int fieldEnemyLimit;
    public int fieldEnemyNumber;

    public int formationPosition;
    public List<int> formationList = new List<int>();

    [SerializeField] private float enemySpawnSpeed;
    [SerializeField] private int enemyLevel;
    [SerializeField] private float RepeatInterval;

    private int selectEnemyKind;
    public int enemyKind;
    [SerializeField] private int guardianLimit;
    [SerializeField] private int sniperLimit;
    [SerializeField] private int guardianNumber;
    [SerializeField] private int sniperNumber;

    public GranadeThrowManager granadeThrowManager;

    [SerializeField] private int soldierSpawnChance;
    [SerializeField] private int guardianSpawnChance;
    [SerializeField] private int sniperSpawnChance;
    [SerializeField] private int allEnemyChance;

    void Start()
    {
        EnemySpawnRepeating(fieldEnemyLimit, enemyLevel);
    }

    private void EnemySpawnRepeating(float fieldEnemyLimit, float enemyLevel)
    {
        CancelInvoke("EnemySpawn");
        RepeatInterval = enemySpawnSpeed / (fieldEnemyLimit + enemyLevel);
        InvokeRepeating("EnemySpawn", 0, RepeatInterval);
    }

    private void EnemySpawn()
    {
        if(fieldEnemyNumber < fieldEnemyLimit + enemyLevel)
        {
            Vector2 enemyPosition = new Vector2(11, 0);

            RandomNumberSelecter();

            while (formationList.Contains(formationPosition))
            {
                RandomNumberSelecter();
            }


            formationList.Add(formationPosition);
            fieldEnemyNumber += 1;

            GameObject Enemy = Instantiate(enemyPrefab, enemyPosition, Quaternion.identity);

            Enemy.GetComponent<EnemyManager>().Initialize(enemySpawner, gunManager, gameScoreManager);
            Enemy.GetComponent<EnemyMovement>().Initialize();
            Enemy.GetComponent<EnemyGunManager>().Initialize();
        } 
    }

    private void RandomNumberSelecter()
    {
        formationPosition = 0;

        do
        {
            soldierSpawnChance = 70;
            guardianSpawnChance = 10 + (enemyLevel / 3) * 5;
            sniperSpawnChance = 20 + (enemyLevel / 2) * 5;
            allEnemyChance = soldierSpawnChance + guardianSpawnChance + sniperSpawnChance;

            selectEnemyKind = UnityEngine.Random.Range(1, allEnemyChance);
            if(selectEnemyKind <= guardianSpawnChance)
            {
                enemyKind = 2;
            } else if (selectEnemyKind <= sniperSpawnChance + guardianSpawnChance)
            {
                enemyKind = 3;
            } else
            {
                enemyKind = 1;
            }

        } while ((enemyKind == 2 && guardianLimit + (enemyLevel / 3) <= guardianNumber) || (enemyKind == 3 && sniperLimit + (enemyLevel / 2) <= sniperNumber));


        if (enemyKind == 1)
        {
            formationPosition += UnityEngine.Random.Range(2, 9) * 10;
        }
        else if (enemyKind == 2)
        {
            guardianNumber += 1;
            formationPosition += 90;
        }
        else if (enemyKind == 3)
        {
            sniperNumber += 1;
            formationPosition += UnityEngine.Random.Range(1, 3) * 10;
        }

        formationPosition += UnityEngine.Random.Range(2, 10);
    }
    

    public void FieldEnemyCount(int DestroyEnemyKind, int DestroyFormationPosition)
    {
        if(DestroyEnemyKind == 2)
        {
            guardianNumber -= 1;
        }
        if(DestroyEnemyKind == 3)
        {
            sniperNumber -= 1;
        }

        formationList.Remove(DestroyFormationPosition);
        fieldEnemyNumber -= 1;

        if(gameScoreManager.Score % 5000 >= 1)
        {
            enemyLevel = Mathf.FloorToInt(gameScoreManager.Score / 5000);
            EnemySpawnRepeating(fieldEnemyLimit, enemyLevel);
        }

        granadeThrowManager.GranadeCharge();
    }

    void Update()
     {

    }
}
