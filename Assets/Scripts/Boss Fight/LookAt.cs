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
    public GameObject projectile;
    private IEnumerator throwCoroutine;
    public Vector3 _FirePointVector3;
    public Transform firePoint;
    
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        throwCoroutine = Throw(3f);
        StartCoroutine(throwCoroutine);
    }

    void Update()
    {
        transform.LookAt(player.transform.position);
        _FirePointVector3 = firePoint.position;
    }

    IEnumerator Throw(float time)
    {
        while (true)
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/BOAR BLAST");
            GameObject currentProj = Instantiate(projectile, _FirePointVector3, Quaternion.identity);
            currentProj.transform.rotation = transform.rotation;
            currentProj.transform.Rotate(0, 90, 0);
            currentProj.GetComponent<Rigidbody>().velocity = Vector3.Normalize(player.transform.position - currentProj.transform.position) * 30;
            yield return new WaitForSeconds(time);   
        }
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