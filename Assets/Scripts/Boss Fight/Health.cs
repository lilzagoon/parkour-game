using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float HP = 3;
    public float damage = 1;

    public EndScreen endScreen;

    public void OnTriggerEnter(Collider other)
    {
        HP -= damage;
    }

    void Update()
    {
        if (HP <= 0)
        {
            endScreen.gameObject.SetActive(true);
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
            Time.timeScale = 0;
        }
    }

}
