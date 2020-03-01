using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    private int nextDialogue = 0;
    private Loader loadXml;
    public TextMeshProUGUI nameDialogue;
    public TextMeshProUGUI textDialogue;
    bool estaDigitando = false;

    [Header("Typping Effect Coroutine")]
    //public float timeBtwChar = 0f;
    public float delayTypping = 0.05f;

    void Start(){
        nameDialogue = GameObject.Find("Name").GetComponent<TMPro.TextMeshProUGUI>();
        textDialogue = GameObject.Find("Dialogue").GetComponent<TMPro.TextMeshProUGUI>();
        loadXml = GameObject.Find("Loader").GetComponent<Loader>();
    }
  
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return) && nextDialogue < loadXml.data.Count && !estaDigitando){
            StartCoroutine(typpingEffect());
            nextDialogue++;
        }

        if(Input.GetKeyDown(KeyCode.X)){
            nextDialogue = 0;
            StartCoroutine(typpingEffect());
        }
    }

    //Animação de texto sendo digitado
    IEnumerator typpingEffect(){

        estaDigitando = true;

        nameDialogue.text = loadXml.data[nextDialogue].charText;
        textDialogue.text = loadXml.data[nextDialogue].dialogueText;

        int totalVisibleCharacters = textDialogue.textInfo.characterCount;
        int counter = 0;

        while(counter <= totalVisibleCharacters){
            int visibleCount = counter % (totalVisibleCharacters + 1);
            textDialogue.maxVisibleCharacters = visibleCount;
            counter++;
            yield return new WaitForSeconds(delayTypping);
        }

        estaDigitando = false;
    }
}
