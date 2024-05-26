using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class LisetManager : MonoBehaviour
{
    public HealthBarManager healthBarManager;
    public float maxHealth;
    public float currentHealth;

    public GameObject downUI;
    private HideManager hideManager;

    public SpriteRenderer BodySpriteRenderer;
    public SpriteRenderer BodyHideSpriteRenderer;
    public SpriteRenderer ArmSpriteRenderer;


    private bool GetAidKitWithGranadeCoroutine;

    public AudioSource audioSource;
    public AudioClip healSound;
    public AudioClip DamageSound;
    public AudioClip CriticalDamageSound;

    public GameCore gameCore;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBarManager = GameObject.Find("HealthBar").GetComponent<HealthBarManager>();
        hideManager = GetComponent<HideManager>();
        audioSource = GetComponent<AudioSource>();

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("EnemyBulletOne"))
        {
            if(hideManager.HideOn == false)
            {
                currentHealth -= 4;
            } else
            {
                currentHealth -= 1;
            }

            StartCoroutine(DamageEffect());
        }

        if (other.CompareTag("EnemyBulletTwo"))
        {
            if (hideManager.HideOn == false)
            {
                currentHealth -= 10;
                StartCoroutine(DamageEffect());
            }

        }

        healthBarManager.UpdateHealthBar(currentHealth, maxHealth);
        if (currentHealth <= 0)
        {
            audioSource.volume = 0.8f;
            audioSource.PlayOneShot(CriticalDamageSound);
            LisetDown();
        }
    }

    private IEnumerator DamageEffect()
    {
        if (currentHealth > 0)
        {
            audioSource.volume = 0.4f;
            audioSource.PlayOneShot(DamageSound);
        }

        BodySpriteRenderer.color = Color.red;
        BodyHideSpriteRenderer.color = Color.red;
        ArmSpriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        BodySpriteRenderer.color = Color.white;
        BodyHideSpriteRenderer.color = Color.white;
        ArmSpriteRenderer.color = Color.white;
    }

    public void GetAidKit(bool Granade)
    {
        currentHealth += 15;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        healthBarManager.UpdateHealthBar(currentHealth, maxHealth);

        if(Granade == false)
        {
            audioSource.volume = 0.3f;
            audioSource.PlayOneShot(healSound);
        }
        if (Granade == true && GetAidKitWithGranadeCoroutine == false)
        {
            StartCoroutine(GetAidKitWithGranade());
        }
    }

    private IEnumerator GetAidKitWithGranade()
    {
        GetAidKitWithGranadeCoroutine = true;
        audioSource.volume = 0.3f;
        audioSource.PlayOneShot(healSound);
        yield return new WaitForSeconds(0.3f);
        GetAidKitWithGranadeCoroutine = false;
    }

    public void LisetDown()
    {

        downUI.SetActive(true);
        gameCore = GameObject.Find("GameCore").GetComponent<GameCore>();
        gameCore.GameCoreScoreSave();
        Time.timeScale = 0;
    }

    void Update()
    {
        
    }
}
