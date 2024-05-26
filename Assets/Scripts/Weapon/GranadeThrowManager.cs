using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class GranadeThrowManager : MonoBehaviour
{
    private float acceleration;
    private float Timer;
    private float ThrowCharge;

    [SerializeField] private float maxAngle;
    [SerializeField] private float minAngle;

    public Transform BodyTransform;
    public GameObject GranadePrefab;
    private Rigidbody2D GranadeRB;
    public GameObject GranadePinPrefab;
    private Rigidbody2D PinRB;

    [SerializeField] private int havingGranade;
    public GranadeBar granadeBar;
    private float ChargeAmount;
    [SerializeField] private float ChargeLimit;
    public GranadeTextManager GranadeTextManager;

    [SerializeField] private float Radius;
    private float angleRadians;
    [SerializeField] GameObject arrow;
    [SerializeField] Transform arrowTransform;
    private bool pressLimiter;


    void Start()
    {
        arrow.SetActive(false);
    }

    public void GranadeCharge()
    {
        ChargeAmount += 1;

        if(ChargeAmount >= ChargeLimit) 
        {
            ChargeAmount = 0;
            havingGranade += 1;
            GranadeTextManager.UpdateGranadeText(havingGranade);
        }
        
        granadeBar.UpdateGranadeChargeBar(ChargeAmount, ChargeLimit);
    }

    private void GranadeThrow()
    {
        arrow.SetActive(false);
        pressLimiter = true;

        acceleration = 10 + 10 * ThrowCharge;


        float X = Mathf.Cos(angleRadians) * acceleration;
        float Y = Mathf.Sin(angleRadians) * acceleration;

        Quaternion Rotation = Quaternion.Euler(0, 0, Random.Range(-120, 120));

        GameObject Granade = Instantiate(GranadePrefab, BodyTransform.transform.position, Rotation);
        GranadeRB = Granade.GetComponent<Rigidbody2D>();
        GameObject Pin = Instantiate(GranadePinPrefab, BodyTransform.transform.position, Rotation);
        PinRB = Pin.GetComponent<Rigidbody2D>();

        GranadeRB.velocity = new Vector3(X, Y, 0);
        PinRB.velocity = new Vector3(X / 3, Y / 3, 0);

        GranadeManager granadeManager = GameObject.Find("Granade(Clone)").GetComponent<GranadeManager>();
        granadeManager.StartTimer(Time.time - Timer);

        havingGranade -= 1;
        GranadeTextManager.UpdateGranadeText(havingGranade);
    }

    // Update is called once per frame
    void Update()
    {
        if (havingGranade > 0 && pressLimiter == false)
        {
            if (Input.GetKeyDown(KeyCode.G))
            {
                Timer = Time.time;
                arrow.SetActive(true);
            }

            if (Input.GetKey(KeyCode.G))
            {
                ThrowCharge = Mathf.Clamp((Time.time - Timer) / 1.5f, 0, 1);

                Vector3 mousePosition = Input.mousePosition;
                Vector2 aimPosition = Camera.main.ScreenToWorldPoint(mousePosition);
                Vector2 angle = new Vector2(aimPosition.x - BodyTransform.transform.position.x, aimPosition.y - BodyTransform.transform.position.y);

                float radianAgnle = Mathf.Atan2(angle.y, angle.x);
                float degreeAngle = Mathf.Clamp(radianAgnle * Mathf.Rad2Deg, minAngle, maxAngle);
                angleRadians = degreeAngle * Mathf.PI / 180f;

                Vector3 arrowPosition = new Vector3(Mathf.Cos(angleRadians) * Radius, Mathf.Sin(angleRadians) * Radius);
                arrowTransform.position = arrowPosition + BodyTransform.position;
                arrowTransform.rotation = Quaternion.Euler(0, 0, degreeAngle + 180);

                Debug.Log(Time.time - Timer);
            }

            if ((Input.GetKeyUp(KeyCode.G) || Time.time - Timer >= 3.5f) && arrow.activeSelf == true)
            {
                GranadeThrow();
            }
        }

        if (Input.GetKeyUp(KeyCode.G))
        {
            pressLimiter = false;
        }
    }
}
