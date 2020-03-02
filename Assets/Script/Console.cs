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
    public Button compileButton;
    private Color actualColor;

    void Start(){
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
        compileButton.enabled = true;
        textInput.GetComponent<Image>().color = actualColor;
    }

    public void resetLevel() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
