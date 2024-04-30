using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{

    public GameObject sign;

    public Dialogue dialogue;
    public GameObject dialogueManager;
    private DialogueManager dm;
    private bool isInRange;
    
    private void Start()
    {
        dm = dialogueManager.GetComponent<DialogueManager>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && isInRange == true)
        {
            FindObjectOfType<DialogueManager>().DisplayNextSentence();
        }
    }

    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
        sign.SetActive(true);
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            isInRange = true;
            Debug.Log(".in");
            TriggerDialogue();
            sign.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            isInRange = false;
            Debug.Log("Mouse is no longer on GameObject.");
            sign.SetActive(false);
            //dm.audioSource.Stop();
        }
    }
}

//     void OnMouseEnter()
//     {
//         //If your mouse hovers over the GameObject with the script attached, output this message
//         Debug.Log("Mouse is over GameObject.");
//         lookingAt = true;
//         if (lookingAt == true)
//         {
//             if (Physics.CheckSphere(transform.position, 5, whatisPlayer))
//             {
//                 TriggerDialogue();
//                 sign.SetActive(true);
//             }
//         }
//     }
//
//     void OnMouseExit()
//     {
//         //The mouse is no longer hovering over the GameObject so output this message each frame
//         Debug.Log("Mouse is no longer on GameObject.");
//         lookingAt = false;
//         sign.SetActive(false);
//     }
// }
