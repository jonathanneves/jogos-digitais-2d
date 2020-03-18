using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Console : MonoBehaviour
{
    private Constants constants;

    [HideInInspector] public bool isCompiling = false;
    [HideInInspector] public string[] commands;

    private InputField textInput;
    private Animation consoleAnim;

    private Color actualColor;
    private bool isReseting = false;
    public GameObject newLevel;
    private GameObject currentLevel;

    public Button compileButton;
    public Button resetButton;
    public Text placeholderInputField;
    public GameObject finalPanel;
    private Animator transition;
    private AudioSource audioSource;
    public AudioClip errorFX;


    void Start(){
        constants = GameObject.Find("Loader").GetComponent<Constants>();
        transition = GameObject.Find("Transition").GetComponent<Animator>();
        textInput = GameObject.Find("InputField").GetComponent<InputField>();
        consoleAnim = GameObject.Find("Console").GetComponent<Animation>();
        audioSource = this.gameObject.GetComponent<AudioSource>();
        placeholderInputField.text = constants.placeholderInput;
        currentLevel = Instantiate(newLevel, newLevel.transform.position, newLevel.transform.rotation) as GameObject;
        actualColor = textInput.GetComponent<Image>().color;
    }

    void Update(){
        if (Input.GetKeyDown(KeyCode.Escape)) {
            SceneManager.LoadScene("Menu");
        }
    }

    public void compile() {
        if (!isCompiling) {
            commands = textInput.text.Split(char.Parse("\n"));
            List<string> gameObjectList = new List<string>(commands);
            gameObjectList.RemoveAll(x => x == "");
            commands = gameObjectList.ToArray();
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

    public IEnumerator redConsole(){
        textInput.GetComponent<Image>().color = new Color(1, 0, 0);
        audioSource.PlayOneShot(errorFX);
        yield return new WaitForSeconds(0.4f);
        textInput.GetComponent<Image>().color = new Color(0, 1, 0);
    }

    private IEnumerator StartAnimationChangeLevel(){
        isReseting = true;
        compileButton.enabled = false;
        resetButton.enabled = false;
        currentLevel.GetComponent<Animator>().SetBool("MoveLeft", true);
        yield return new WaitForSeconds(1f);
        if(currentLevel != null){
            Destroy(currentLevel);
            currentLevel = Instantiate(newLevel, newLevel.transform.position, newLevel.transform.rotation) as GameObject;
        }
        yield return new WaitForSeconds(1f);
        isReseting = false;
        compileButton.enabled = true;
        resetButton.enabled = true;
    }

    public void disableScene(){
        finalPanel.SetActive(true);
    }

    public IEnumerator LoadNewSceneAfterTransition() {
        transition.SetBool("animationOut", true);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
