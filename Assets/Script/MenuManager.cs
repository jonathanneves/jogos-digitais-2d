using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    private Constants constants;
    private Animator animator;
    public Transform canvas;
    private string currentScene;


    void Start() {
        constants = FindObjectOfType<Constants>();
        animator = GameObject.Find("Transition").GetComponent<Animator>();
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

    public void returnToMenu(string scene){
        constants.currentScene = scene;
        SceneManager.LoadScene("Menu");
    }
}
