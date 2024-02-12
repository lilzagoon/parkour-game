using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleMove : MonoBehaviour
{
    public Health health;
    public Transform target;
    public Transform self;
    public float speed = 1f;
    public float radius = 2f;
    public float angle = 0f;

    // Update is called once per frame
    void Update()
    {
        float x = target.position.x + Mathf.Cos(angle) * radius;
        float y = self.position.y;
        float z = target.position.z + Mathf.Sin(angle) * radius;

     if(health.HP == 2)
        {
            transform.position = new Vector3(x, y, z);
            angle += speed * Time.deltaTime;
        }

     if (health.HP == 1) 
       {
            speed = 0.4f;
            transform.position = new Vector3(x, y, -z);
            angle += speed * Time.deltaTime;
        }
        
    }
}
