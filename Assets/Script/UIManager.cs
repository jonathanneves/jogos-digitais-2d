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

    [Header("Typping Effect Coroutine")]
    //public float timeBtwChar = 0f;
    public float delayTypping = 0.05f;

    void Start(){
        loadXml = GameObject.Find("Loader").GetComponent<Loader>();
    }
  
   /*void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return) && nextDialogue < loadXml.data.Count && !estaDigitando){
            StartCoroutine(typpingEffect());
            nextDialogue++;
        }
    }*/

    public IEnumerator typpingEffect(string actualScene){

        estaDigitando = true;
        if(nextDialogue == 0)
            yield return new WaitForSeconds(1.25f);

        string actualText = loadXml.data[nextDialogue].dialogueText;
        textDialogue.text = actualText;

        int totalVisibleCharacters = actualText.Length;
        int counter = 0;

        while(counter <= totalVisibleCharacters){
            int visibleCount = counter % (totalVisibleCharacters + 1);
            textDialogue.maxVisibleCharacters = visibleCount;
            counter++;
            yield return new WaitForSeconds(delayTypping);
            Debug.Log(visibleCount + " " + counter + " " + totalVisibleCharacters);
        }

        estaDigitando = false;
    }
}
