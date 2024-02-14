using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{
    public Transform target;
    public Transform tarLook;
    public Transform target2;
    public float speed = 1f;
    public float time = 0;
    public float timmer = 0.005f;
    public Vector3 tempTarget;

    public Coroutine Look;

    void Update()
    {
        StartRot();

        tempTarget = new Vector3(target.position.x, 0f, target.position.z);
        tarLook.position = tempTarget;

        if (time >= timmer)
        {
            tarLook.position = target2.position;
        }

        if (time >= timmer + 0.001)
        {
            time = 0;
        }
    }

    public void StartRot()
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

        while (time < 1)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, time);

            time += Time.deltaTime * speed;
            yield return null;
        }

    }
 
}
