using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionFollower : MonoBehaviour
{

    public Transform targetTransform;

    public Vector3 Offset;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = targetTransform.position + Offset;
    }
}
