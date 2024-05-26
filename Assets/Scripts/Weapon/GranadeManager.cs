using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GranadeManager : MonoBehaviour
{
    public GameObject particlePrefab;
    public Collider2D exprosionCollider;
    public Collider2D granadeCollider;
    public Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        particlePrefab = (GameObject)Resources.Load("GranadeParticle");
        exprosionCollider = GetComponent<CircleCollider2D>();
        granadeCollider = GetComponent<CapsuleCollider2D>();
        exprosionCollider.enabled = false;

        StartCoroutine(ExplosionTimer(-2));
    }

    public void StartTimer(float Timer)
    {
        StartCoroutine(ExplosionTimer(Timer));
    }

    private IEnumerator ExplosionTimer(float Timer)
    {
        yield return new WaitForSeconds(4 - Timer);
        exprosionCollider.enabled = true;
        GameObject particle = Instantiate(particlePrefab, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(0.1f);
        Destroy(gameObject);
    }
     
    void Update()
    {
        
    }
}
