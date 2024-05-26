using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AidKitParticleController : MonoBehaviour
{
    Transform aidKitTransform;
    // Start is called before the first frame update
    void Start()
    {
        aidKitTransform = transform.parent.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
       if(aidKitTransform.rotation.z != 0)
        {
            float particleRotation = -1 * aidKitTransform.rotation.z -90;
            transform.eulerAngles = new Vector3(particleRotation, 90, -90);
        }
    }
}
