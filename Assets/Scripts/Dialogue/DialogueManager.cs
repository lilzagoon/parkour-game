using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private AudioClip dialogueTtpingSound;
   
    [Range(-3, 3)]
    [SerializeField] private float minPitch = 0.5f;
   
    [Range(-3, 3)]
    [SerializeField] private float maxPitch = 3f;
    
    [SerializeField] private bool stopAudio;
   
    private AudioSource audioSource;
   
    public TextMeshProUGUI dialogueText;
    
    private Queue<string> sentences;
 
    void Start()
    {
        sentences = new Queue<string>();
        audioSource = this.gameObject.AddComponent<AudioSource>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        Debug.Log("starting conversation");

        sentences.Clear();
        //Setups sentences.
        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence (string sentence)
    {
        dialogueText.text = "";
        dialogueText.maxVisibleCharacters = 0;
        //reveals characters 1 by 1.
        foreach (char letter in sentence.ToCharArray())
        {
            PlayDialogueSound(dialogueText.maxVisibleCharacters);
            dialogueText.text += letter;
            dialogueText.maxVisibleCharacters++;
            yield return new WaitForSeconds(0.04f);
        }
    }

    private void PlayDialogueSound(int currentDisplayedCharacterCount)
    {
        //Decides if sound is played every second, third letter etc.
        if (currentDisplayedCharacterCount % 2 == 0)
        {
            if (stopAudio)
            {
                audioSource.Stop();
            }
            audioSource.pitch = Random.Range(minPitch, maxPitch);
            audioSource.PlayOneShot(dialogueTtpingSound);
        }
    }

    void EndDialogue()
    {
        Debug.Log("end of conversation");
    }
}
