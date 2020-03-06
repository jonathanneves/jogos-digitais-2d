using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{

    private Animator animator;

    void Start() {
        animator = GameObject.Find("Transition").GetComponent<Animator>();
    }

    public void StartGame(){
        StartCoroutine(LoadNewSceneAfterTransition());    
    }

    private IEnumerator LoadNewSceneAfterTransition(){
        animator.SetBool("animationOut", true);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LeaveGame(){
        Application.Quit();    
    }
}
