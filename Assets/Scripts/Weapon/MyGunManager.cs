using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class MyGunManager : MonoBehaviour
{
    public float gunDamage = 1;
    public float spreadLevel;
    [SerializeField] private float interval;
    public float reloadTime;
    public int magazineSize;

    public Vector2 muzzlePosition;
    private float intervalTimer;
    public GameObject bulletPrefab;

    public MagazineBar magazineBar;
    public ReloadBar reloadBar;
    public int remainBullets;
    public float reloadTimer;
    public bool Reloading = false;

    public AudioSource audioSource;
    public AudioClip BratonFire;
    public AudioClip BratonReload;

    [SerializeField] private ParticleSystem particle;

    public GameObject CartridgePrefab;
    void Start()
    {
        remainBullets = magazineSize;
        magazineBar = GameObject.Find("MagazineBar").GetComponent<MagazineBar>();
        reloadBar = GameObject.Find("ReloadBar").GetComponent<ReloadBar>();

        audioSource = GetComponent<AudioSource>();
    }

    IEnumerator Reload()
    {
        if(Reloading == false && remainBullets != magazineSize)
        {
            reloadTimer = Time.time;
            Reloading = true;
            audioSource.volume = 1;
            audioSource.PlayOneShot(BratonReload);

            yield return new WaitForSeconds(reloadTime);

            remainBullets = magazineSize;
            magazineBar.UpdateReloadBar(remainBullets, magazineSize);
            Reloading = false;
        }
    }

    void Update()
    {
        if (Input.GetMouseButton(0) && Time.time - intervalTimer >= interval && Reloading == false && Time.timeScale != 0 && !Input.GetKey(KeyCode.Space))
        {
            ArmMovement ArmMovement = GetComponent<ArmMovement>();

            Vector2 circumference = new Vector2(Mathf.Cos(transform.rotation.eulerAngles.z * Mathf.Deg2Rad), Mathf.Sin(transform.rotation.eulerAngles.z * Mathf.Deg2Rad));
            muzzlePosition = new Vector2(circumference.x * ArmMovement.Radius * 2, circumference.y * ArmMovement.Radius * 2) + ArmMovement.armPosition;

            float spreadOfBulletAngles = transform.rotation.eulerAngles.z + Random.Range(spreadLevel * -1 , spreadLevel);

            Quaternion rotation = Quaternion.Euler(0f, 0f, spreadOfBulletAngles);
            GameObject Bullet = Instantiate(bulletPrefab, muzzlePosition, rotation);

            remainBullets -= 1;
            magazineBar.UpdateReloadBar(remainBullets, magazineSize);

            particle.Play();

            Vector2 ChamberPosition = new Vector2(circumference.x * ArmMovement.Radius * 1.6f, circumference.y * ArmMovement.Radius * 1.6f) + ArmMovement.armPosition;
            GameObject Cartridge = Instantiate(CartridgePrefab, ChamberPosition, rotation);

            if (remainBullets == 0)
            {
                StartCoroutine("Reload");
            }

            audioSource.volume = 0.6f;
            audioSource.PlayOneShot(BratonFire);

            intervalTimer = Time.time;
        }

        if(Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine("Reload");
        }
    }
}