using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimSight : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePosition = Input .mousePosition;
        Vector2 aimPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        transform.position = aimPosition;

    }
}
