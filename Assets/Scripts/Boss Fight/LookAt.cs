using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{
    public Transform target;
    public Transform tarLook;
    public Transform target2;
    public float speed = 1f;
    public Vector3 tempTarget;
    GameObject player;

    public Coroutine Look;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        transform.LookAt(player.transform.position);
    }

    /*public void StartRot()
    {
        if (Look != null)
        {
            StopCoroutine(Look);
        }

        Look = StartCoroutine(LookTar());
    }

    private IEnumerator LookTar()
    {
        Quaternion lookRotation = Quaternion.LookRotation(tarLook.position - transform.position);
    }

}*/
}