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
    public AudioSource dialogueFX;

    [Header("Typping Effect Coroutine")]
    public float delayTypping = 0.05f;

    void Start(){
        loadXml = GameObject.Find("Loader").GetComponent<Loader>();
    }
  

    public IEnumerator typpingEffect(string actualScene){

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
            dialogueFX.Play();
            yield return new WaitForSeconds(delayTypping);
        }
        nextDialogue++;
        Debug.Log(nextDialogue);
        if (nextDialogue == 7)
            endDialogue = true;
        estaDigitando = false;
    }
}
