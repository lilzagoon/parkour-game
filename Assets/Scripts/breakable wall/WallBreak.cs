using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallBreak : MonoBehaviour
{

    public GameObject destroyedVersion;
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer == 10)
        {
            Instantiate(destroyedVersion, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
