using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LevelManager : MonoBehaviour
{
    private Constants constants;

    public GameObject dialogue;
    public UIManager uiManager;
    private string actualScene;
    private bool firstTime = true;
    private Animator consoleAnim;
    public Button compile;
    public Button reset;
    public InputField textInput;
    private Timer timer;

    void Start(){
        constants = FindObjectOfType<Constants>();
        actualScene = SceneManager.GetActiveScene().name;
        consoleAnim = GameObject.Find("Console").GetComponent<Animator>();
        timer = FindObjectOfType<Timer>();
        if(actualScene == "Fase 1"){
            timer.enabled = false;
            dialogue.GetComponent<Animator>().SetBool("OpenDialogue", true);
            StartCoroutine(uiManager.typpingEffect());
        } else {
            consoleAnim.SetBool("AbrirConsole", true);
        }
    }

    void Update(){
        if (actualScene == "Fase 1") {
            if (Input.anyKey && !uiManager.estaDigitando
                    && uiManager.nextDialogue < uiManager.loadXml.data.Count) {
                StartCoroutine(uiManager.typpingEffect());
                if(uiManager.nextDialogue == 5){
                    consoleAnim.SetBool("AbrirConsole", true);
                    enableUI(false);
                }
                if(uiManager.nextDialogue == 6){
                    textInput.text = constants.inputRight + "\n" + constants.inputDown + "\n" + constants.inputUp + "\n" + constants.inputLeft;
                    compile.GetComponent<Image>().color = new Color(0f, 128f, 0f);
                }
                if(uiManager.nextDialogue == 7){
                    textInput.text = "";
                    compile.GetComponent<Image>().color = new Color(255f, 255f, 255f);
                }
            }
            if (Input.anyKey && !uiManager.estaDigitando && uiManager.endDialogue) {
                dialogue.GetComponent<Animator>().SetBool("OpenDialogue", false);
                enableUI(true);
                uiManager.textDialogue.text = "";
                timer.enabled = true;
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
