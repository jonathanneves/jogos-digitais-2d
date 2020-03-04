using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Console : MonoBehaviour
{
    [HideInInspector]
    public bool isCompiling = false;
    [HideInInspector]
    public string[] commands;
    private InputField textInput;
    private Color actualColor;
    private bool isReseting = false;
    public Button compileButton;
    public GameObject newLevel;
    private GameObject currentLevel;

    void Start(){
        currentLevel = Instantiate(newLevel, newLevel.transform.position, newLevel.transform.rotation) as GameObject;
        textInput = GameObject.Find("InputField").GetComponent<InputField>();
        actualColor = textInput.GetComponent<Image>().color;
    }

    public void compile() {
        if (!isCompiling) {
            commands = textInput.text.Split(char.Parse("\n"));
            isCompiling = true;
            compileButton.enabled = false;
            textInput.GetComponent<Image>().color = new Color(0, 1, 0);
        }
    }

    public void resetUiStatus(){
        for (int i = 0; i < commands.Length; i++) {
            commands[i] = null;
        }
        compileButton.enabled = true;
        textInput.GetComponent<Image>().color = actualColor;
    }

    public void resetLevel() {
        if(!isReseting){
            StartCoroutine(StartAnimationChangeLevel());
        }
    }

    private IEnumerator StartAnimationChangeLevel(){
        isReseting = true;
        compileButton.enabled = false;
        currentLevel.GetComponent<Animator>().SetBool("MoveLeft", true);
        yield return new WaitForSeconds(1f);
        if(currentLevel != null){
            Destroy(currentLevel);
            currentLevel = Instantiate(newLevel, newLevel.transform.position, newLevel.transform.rotation) as GameObject; ;
        }
        yield return new WaitForSeconds(1f);
        isReseting = false;
        compileButton.enabled = true;
    }
}
