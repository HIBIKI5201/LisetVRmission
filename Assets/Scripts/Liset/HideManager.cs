using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideManager : MonoBehaviour
{
    public GameObject Arm;
    public GameObject StandLiset;
    public GameObject HideLiset;

    private Vector2 firstPosition;
    private BoxCollider2D boxCollider2D;

    public bool HideOn;
    private float Timer;

    // Start is called before the first frame update
    void Start()
    {
        firstPosition = transform.position;
        boxCollider2D = GetComponent<BoxCollider2D>();

        HideUp();
    }

    private void HideUp()
    {
        transform.position = firstPosition;
        boxCollider2D.offset = new Vector2(0, -1.2f);
        boxCollider2D.size = new Vector2(1, 5);

        Arm.SetActive(true);
        StandLiset.SetActive(true);
        HideLiset.SetActive(false);
        HideOn = false;
    }

    private void HideDown()
    {
        transform.position = new Vector2(-5.6f, -1.8f);
        boxCollider2D.offset = new Vector2(0.6f, -0.2f);
        boxCollider2D.size = new Vector2(2, 3);

        Arm.SetActive(false);
        StandLiset.SetActive(false);
        HideLiset.SetActive(true);

        HideOn = true;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Timer = Time.time;
        }
        if(Input.GetKey(KeyCode.Space) && Time.time - Timer > 0.2f)
        {
            HideDown();
        } else
        {
            HideUp();
        }
    }
}
