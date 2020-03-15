using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LevelManager : MonoBehaviour
{

    public GameObject dialogue;
    public UIManager uiManager;
    private string actualScene;
    private bool firstTime = true;
    private Animator consoleAnim;
    public Button compile;
    public Button reset;
    public InputField textInput;

    void Start(){
        actualScene = SceneManager.GetActiveScene().name;
        consoleAnim = GameObject.Find("Console").GetComponent<Animator>();
        if(actualScene == "Fase 1"){
            dialogue.GetComponent<Animator>().SetBool("OpenDialogue", true);
            StartCoroutine(uiManager.typpingEffect(actualScene));
        } else {
            consoleAnim.SetBool("AbrirConsole", true);
        }
    }

    void Update(){
        if (actualScene == "Fase 1") {
            if (Input.anyKey && !uiManager.estaDigitando
                    && uiManager.nextDialogue < uiManager.loadXml.data.Count) {
                StartCoroutine(uiManager.typpingEffect(actualScene));
                if(uiManager.nextDialogue == 5){
                    consoleAnim.SetBool("AbrirConsole", true);
                    enableUI(false);
                }
            }
            if (Input.anyKey && !uiManager.estaDigitando && uiManager.endDialogue) {
                dialogue.GetComponent<Animator>().SetBool("OpenDialogue", false);
                enableUI(true);
                uiManager.textDialogue.text = "";
            }
        } else {
            this.enabled = false;
        }
    }

    void enableUI(bool status){
        compile.enabled = status;
        reset.enabled = status;
        textInput.enabled = status;
    }
}
