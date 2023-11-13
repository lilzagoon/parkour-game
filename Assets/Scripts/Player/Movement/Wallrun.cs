using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wallrun : MonoBehaviour
{
    [SerializeField] private Transform orientation;

    [Header("Wall Running")] 
    [SerializeField] private float wallDistance = .5f;
    [SerializeField] private float minimumJumpHeight = 1.5f;

    private bool wallLeft = false;
    void Start()
    {
        
    }


    bool CanWallRun()
    {
        return !Physics.Raycast(transform.position, Vector3.down, minimumJumpHeight);
    }
    void CheckWall()
    {
        wallLeft = Physics.Raycast(transform.position, -orientation.right, wallDistance);
    }
    
    // Update is called once per frame
    void Update()
    {
        CheckWall();

        if (CanWallRun())
        {
            if (wallLeft)
            {
                Debug.Log("wall running left");
            }
        }
    }
}
