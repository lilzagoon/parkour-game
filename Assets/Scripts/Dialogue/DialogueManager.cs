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
    private bool yapping = false;
    public bool boar = false;
   
    //public AudioSource audioSource;
   
    public TextMeshProUGUI dialogueText;

    public GameObject tesxtBox;
    
    private Queue<string> sentences;

    private int maxChar;
 
    void Start()
    {
        sentences = new Queue<string>();
        //audioSource = this.gameObject.AddComponent<AudioSource>();
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
      

        if (yapping == true)
        {
            
            dialogueText.maxVisibleCharacters = 9000;
            yapping = false;
            return;
        }
        else if (yapping == false)
        {
            string sentence = sentences.Dequeue();
            StopAllCoroutines();
            maxChar = dialogueText.maxVisibleCharacters;
            StartCoroutine(TypeSentence(sentence));
        }

        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
    }

    IEnumerator TypeSentence (string sentence)
    {
        yapping = true;
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
        Debug.Log("Stop Yapping");
        yapping = false;
    
    }

    private void PlayDialogueSound(int currentDisplayedCharacterCount)
    {
        //Decides if sound is played every second, third letter etc.
        if (currentDisplayedCharacterCount % 2 == 0)
        {
            if (stopAudio)
            {
                //audioSource.Stop();
            }
            // audioSource.pitch = Random.Range(minPitch, maxPitch);
            // audioSource.PlayOneShot(dialogueTtpingSound);
            if (boar == false)
            { 
                FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Dialogue Noise");
            }
            else
            {
                FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/BoarBeep");
            }
        }
    }

    public void EndDialogue()
    {
        tesxtBox.SetActive(false);
        Debug.Log("end of conversation");
    }
}
