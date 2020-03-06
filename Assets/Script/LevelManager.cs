using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{

    public GameObject dialogue;
    private string actualScene;
    public UIManager uiManager;
    private bool firstTime = true;

    void Start(){
        actualScene = SceneManager.GetActiveScene().name;
        if(actualScene == "Fase 1"){
            dialogue.GetComponent<Animator>().SetBool("OpenDialogue", true);
            StartCoroutine(uiManager.typpingEffect(actualScene));
        }
    }

    void Update(){
        if (Input.GetKeyDown(KeyCode.Return) && !uiManager.estaDigitando
                && uiManager.nextDialogue < uiManager.loadXml.data.Count) {
            StartCoroutine(uiManager.typpingEffect(actualScene));
            uiManager.nextDialogue++;
            if (uiManager.nextDialogue < uiManager.loadXml.data.Count){
                dialogue.GetComponent<Animator>().SetBool("CloseDialogue", true);
            }
        }          
    }

}
