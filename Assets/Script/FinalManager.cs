using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class FinalManager : MonoBehaviour
{
    private Constants constants;
    public TMP_Text theEndText;
    private bool estaDigitando = true;
    private AudioSource audioSource;
    public AudioClip dialogueFX;
    public float delayTypping = 0.05f;

    void Start() {
        audioSource = this.gameObject.GetComponent<AudioSource>();
        constants = GameObject.Find("Loader").GetComponent<Constants>();
        StartCoroutine(typpingEffect(constants.theEndText));
    }

    void Update(){
        if(!estaDigitando){
            if (Input.anyKey){
                constants.currentScene = "Fase 1";
                SceneManager.LoadScene("Menu");
            }
        }
    }

    private IEnumerator typpingEffect(string actualText) {

        theEndText.text = actualText;

        int totalVisibleCharacters = actualText.Length;
        int counter = 0;

        while (counter <= totalVisibleCharacters) {
            int visibleCount = counter % (totalVisibleCharacters + 1);
            theEndText.maxVisibleCharacters = visibleCount;
            if (counter < totalVisibleCharacters && actualText[counter] == '.') {
                delayTypping = delayTypping * 3;
            }
            counter++;
            audioSource.PlayOneShot(dialogueFX);
            yield return new WaitForSeconds(delayTypping);
        }
        estaDigitando = false;
    }
}
