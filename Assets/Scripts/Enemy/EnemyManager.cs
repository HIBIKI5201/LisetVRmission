using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyManager : MonoBehaviour
{
    private EnemySpawner Spawner;
    private MyGunManager GunManager;
    private GameScoreManager ScoreManager;
    private float maxHealth;
    private float currentHeallth;
    [SerializeField] private float soldierHealth;
    [SerializeField] private float guardianHealth;
    [SerializeField] private float sniperHealth;

    [SerializeField] private Sprite soldierImage;
    [SerializeField] private Sprite guardianImage;
    [SerializeField] private Sprite sniperImage;

    private SpriteRenderer spriteRenderer;

    [SerializeField] private float ShieldRepairTime;

    public int enemyKind;
    public int formationPosition;

    private GameObject AidKitPrefab;

    private GameObject EnemyShieldPrefab;

    private GameObject EnemyDeadParticlePrefab;

    public AudioSource audioSource;
    public AudioClip ShieldBreakSound;
    public AudioClip ShieldRepairSound;

    void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        AidKitPrefab = (GameObject)Resources.Load("AidKit");
        EnemyDeadParticlePrefab = (GameObject)Resources.Load("EnemyDeadParticle");

        if(enemyKind == 1 )
        {
            maxHealth = soldierHealth;

            spriteRenderer.sprite = soldierImage;
        }
        if(enemyKind == 2 )
        {
            maxHealth = guardianHealth;
            EnemyShieldPrefab = (GameObject)Resources.Load("EnemyShield");
            GameObject enemyShield = Instantiate(EnemyShieldPrefab, transform.position + new Vector3(-1,0,0), Quaternion.identity);
            enemyShield.transform.parent = transform;

            spriteRenderer.sprite = guardianImage;
        }
        if (enemyKind == 3 )
        {
            maxHealth = sniperHealth;

            spriteRenderer.sprite = sniperImage;
        }
        currentHeallth = maxHealth;
    }

    public void Initialize(EnemySpawner spawner, MyGunManager gunManager, GameScoreManager gameScoreManager)
    {
        Spawner = spawner;
        GunManager = gunManager;
        ScoreManager = gameScoreManager;

        enemyKind = spawner.enemyKind;
        formationPosition = spawner.formationPosition;

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            currentHeallth -= GunManager.gunDamage;

            if (currentHeallth <= 0f)
            {
                Dead();
            }
        }

        if(other.CompareTag("Granade"))
        {
            CircleCollider2D circleCollider = other.GetComponent<CircleCollider2D>();
            if (circleCollider.enabled == true)
            {
                currentHeallth -= 5;

                if (currentHeallth <= 0f)
                {
                    Dead();
                }
            }
        }
    }

    private void Dead()
    {
        ScoreManager.ScoreMathf(enemyKind);
        Spawner.FieldEnemyCount(enemyKind, formationPosition);

        GameObject DeadParticle = Instantiate(EnemyDeadParticlePrefab, transform.position, Quaternion.identity);

        int AidKitDropChance = 5;
        if (enemyKind == 2)
        {
            AidKitDropChance = 50;
        }
        if (enemyKind == 3)
        {
            AidKitDropChance = 20;
        }

        if (Random.Range(1, 101) <= AidKitDropChance)
        {
            Quaternion aidKitRotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, transform.rotation.z + Random.Range(0f, 30f));
            GameObject AidKit = Instantiate(AidKitPrefab, transform.position, aidKitRotation);
        }

        Destroy(gameObject);
    }

    public void ShieldRepairStarter()
    {
        StartCoroutine(ShieldRepair());
    }

    private IEnumerator ShieldRepair()
    {
        audioSource.volume = 0.2f;
        audioSource.pitch = 1;
        audioSource.PlayOneShot(ShieldBreakSound);

        yield return new WaitForSeconds(ShieldRepairTime);

        EnemyShieldPrefab = (GameObject)Resources.Load("EnemyShield");
        GameObject enemyShield = Instantiate(EnemyShieldPrefab, transform.position + new Vector3(-1, 0, 0), Quaternion.identity);
        enemyShield.transform.parent = transform;

        audioSource.volume = 0.2f;
        audioSource.pitch = 1;
        audioSource.PlayOneShot(ShieldRepairSound);
    }

    void Update()
    {

    }
}