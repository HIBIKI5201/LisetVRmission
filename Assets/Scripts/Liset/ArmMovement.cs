using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmMovement : MonoBehaviour
{
    public float Radius;
    public Vector2 armPosition;
    public Transform BodyTransform;

    [SerializeField] private float maxRotation;
    [SerializeField] private float minRotation;

    void Start()
    {
        Transform Transform = gameObject.GetComponent<Transform>();

        Radius = Transform.localScale.x / 2;
        Vector3 armToBodyDistance = new Vector3(-0.1f, 0.1f, 0);

        armPosition = BodyTransform.position + armToBodyDistance;
        Transform.position = new Vector2(Radius, 0f) + armPosition;
    }

    void Update()
    {
        if(Time.timeScale != 0)
        {
            Vector3 mousePosition = Input.mousePosition;
            Vector2 aimPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            Vector2 angle = new Vector2(aimPosition.x - armPosition.x, aimPosition.y - armPosition.y);


            float radianAgnle = Mathf.Atan2(angle.y, angle.x);
            float degreeAngle = Mathf.Clamp(radianAgnle * Mathf.Rad2Deg, minRotation, maxRotation);

            transform.eulerAngles = new Vector3(0f, 0f, degreeAngle);

            Vector2 circumference = new Vector2(Mathf.Cos(degreeAngle * Mathf.Deg2Rad), Mathf.Sin(degreeAngle * Mathf.Deg2Rad));

            Vector2 newPosition = new Vector2(circumference.x * Radius, circumference.y * Radius) + armPosition;
            transform.position = newPosition;
        }
    }
}
