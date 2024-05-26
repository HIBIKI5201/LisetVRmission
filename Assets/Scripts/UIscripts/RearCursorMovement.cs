using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RearCursorMovement : MonoBehaviour
{
    [SerializeField] private float positionDegree;
    [SerializeField] private float radius;
    [SerializeField] private float spreadOfBulletAngles;

    [SerializeField] private MyGunManager gunManager;
    void Start()
    {
        gunManager = GameObject.Find("Arm").GetComponent<MyGunManager>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePosition = Input.mousePosition;
        Vector2 aimPosition = Camera.main.ScreenToWorldPoint(mousePosition);

        float lengthOfHypotenuse = Mathf.Sqrt(Mathf.Pow(aimPosition.x - gunManager.muzzlePosition.x, 2) + Mathf.Pow(aimPosition.y - gunManager.muzzlePosition.y, 2));
        float spreadRadian = gunManager.spreadLevel * Mathf.PI / 180;

        radius = lengthOfHypotenuse / Mathf.Sin(90 * Mathf.PI / 180) * Mathf.Sin(spreadRadian);

        float positionRadian = positionDegree * Mathf.PI / 180f;

        float X = (radius + 0.14f) * Mathf.Cos(positionRadian) / 2;
        float Y = (radius + 0.14f) * Mathf.Sin(positionRadian) / 2;

        transform.position = aimPosition + new Vector2(X, Y);
    }
}
