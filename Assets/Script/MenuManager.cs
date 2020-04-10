using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MenuManager : MonoBehaviour
{
    private Constants constants;
    private Animator animator;
    public Transform canvas;
    private string currentScene;

    //UI TEXT
    public Text playText;
    public Text creditsText;
    public Text exitText;
    public TMP_Text titleCredits;
    public Text buttonBack;

    void Start() {
        constants = FindObjectOfType<Constants>();
        animator = GameObject.Find("Transition").GetComponent<Animator>();
        setAllText();
    }

    public void StartGame(){
        StartCoroutine(LoadNewSceneAfterTransition());    
    }

    private IEnumerator LoadNewSceneAfterTransition(){
        animator.SetBool("animationOut", true);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(constants.currentScene);
    }

    public void LeaveGame(){
        Application.Quit();    
    }

    public void Creditos(){
        canvas.localPosition = new Vector3(-2200f,0,0);
    }

    public void Voltar(){
        canvas.localPosition = new Vector3(0, 0, 0);
    }

    public void getLanguage(string language) {
        FindObjectOfType<Loader>().currentLanguage(language);
    }

    public void setAllText(){
        if(constants != null){
            playText.text = constants.playText;
            creditsText.text = constants.creditsText;
            exitText.text = constants.exitText;
            titleCredits.text = constants.titleCredits;
            buttonBack.text = constants.buttonBack; 
        }
    }
}
