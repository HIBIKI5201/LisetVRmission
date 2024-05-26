using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGunManager : MonoBehaviour
{
    [SerializeField] private float interval;
    private float intervalTimer;
    public GameObject EnemyBulletPrefab;
    private int enemyKind;

    public AudioSource audioSource;
    public AudioClip LaserFire;
    public AudioClip LaserCharge;

    public ParticleSystem particle;
    private bool particleActive;

    void Start()
    {

    }

    public void Initialize()
    {
        EnemyManager enemyManager = GetComponent<EnemyManager>();
        particle = GetComponent<ParticleSystem>();

        enemyKind = enemyManager.enemyKind;

        if (enemyKind == 1)
        {
            EnemyBulletPrefab = (GameObject)Resources.Load("EnemyBulletOne");
            interval = 5;
        }
        if (enemyKind == 3)
        {
            EnemyBulletPrefab = (GameObject)Resources.Load("EnemyBulletTwo");
            interval = 8;
        }

        intervalTimer = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if(enemyKind == 3)
        {
            if (Time.time - intervalTimer >= interval - 4 && particleActive == false)
            {
                particleActive = true;
                audioSource.volume = 0.05f;
                audioSource.pitch = 1.5f;
                audioSource.PlayOneShot(LaserCharge);
                particle.Play();
            }
        }

        if (Time.time - intervalTimer >= interval && EnemyBulletPrefab != null)
        {
            Transform playerTransform = GameObject.Find("Body").GetComponent<Transform>();
            Vector2 angle = new Vector2(transform.position.x - playerTransform.position.x, transform.position.y - playerTransform.position.y);

            float radianAgnle = Mathf.Atan2(angle.y, angle.x);
            float degreeAngle = radianAgnle * Mathf.Rad2Deg;

            Quaternion rotation = Quaternion.Euler(0f, 0f, degreeAngle);


            GameObject Bullet = Instantiate(EnemyBulletPrefab, transform.position, rotation);

            audioSource.volume = 0.2f;
            audioSource.pitch = 3;
            audioSource.PlayOneShot(LaserFire);

            intervalTimer = Time.time;
            if(enemyKind ==3)
            {
                particleActive =false;
            }
        }
    }
}
