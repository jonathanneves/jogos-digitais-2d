using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [HideInInspector] public int nextDialogue = 0;
    public Loader loadXml;
    public TMP_Text textDialogue;
    [HideInInspector] public bool estaDigitando = false;
    [HideInInspector] public bool endDialogue = false;
    private AudioSource audioSource;
    public AudioClip dialogueFX;

    [Header("Typping Effect Coroutine")]
    public float delayTypping = 0.05f;

    void Start(){
        audioSource = this.gameObject.GetComponent<AudioSource>();
        loadXml = GameObject.Find("Loader").GetComponent<Loader>();
    }
  

    public IEnumerator typpingEffect(){

        estaDigitando = true;
        if(nextDialogue == 0)
            yield return new WaitForSeconds(1.5f);

        string actualText = loadXml.data[nextDialogue].dialogueText;
        textDialogue.text = actualText;

        int totalVisibleCharacters = actualText.Length;
        int counter = 0;

        while(counter <= totalVisibleCharacters){
            int visibleCount = counter % (totalVisibleCharacters + 1);
            textDialogue.maxVisibleCharacters = visibleCount;
            counter++;
            audioSource.PlayOneShot(dialogueFX);
            yield return new WaitForSeconds(delayTypping);
        }
        nextDialogue++;
        if (nextDialogue == 7)
            endDialogue = true;
        estaDigitando = false;
    }
}
