using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplayManager : MonoBehaviour
{
    public EnemySpawner spawner;

    public LisetManager lisetManager;
    public HealthBarManager healthBarManager;

    public GameScoreManager gameScoreManager;
    public ScoreText ScoreText;

    public MyGunManager gunManager;
    public MagazineBar magazineBar;

    void Start()
    {

    }

    public void Replay()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy);
        }
        GameObject[] enemyBulletOnes = GameObject.FindGameObjectsWithTag("EnemyBulletOne");
        foreach (GameObject enemyBullet in enemyBulletOnes)
        {
            Destroy(enemyBullet);
        }
        GameObject[] enemyBulletTwos = GameObject.FindGameObjectsWithTag("EnemyBulletTwo");
        foreach (GameObject enemyBullet in enemyBulletTwos)
        {
            Destroy(enemyBullet);
        }
        GameObject[] Bullets = GameObject.FindGameObjectsWithTag("Bullet");
        foreach (GameObject Bullet in Bullets)
        {
            Destroy(Bullet);
        }
        GameObject[] AidKits = GameObject.FindGameObjectsWithTag("AidKit");
        foreach (GameObject AidKit in AidKits)
        {
            Destroy(AidKit);
        }

        spawner.fieldEnemyNumber = 0;
        spawner.formationList.Clear();

        lisetManager.currentHealth = lisetManager.maxHealth;
        healthBarManager.UpdateHealthBar(lisetManager.currentHealth, lisetManager.maxHealth);

        gameScoreManager.Score = 0;
        ScoreText.UpdateScoreText();

        gunManager.remainBullets = gunManager.magazineSize;
        magazineBar.UpdateReloadBar(gunManager.remainBullets, gunManager.magazineSize);

        Time.timeScale = 1;
    }

    void Update()
    {
        
    }
}
