using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CheckPlataform : MonoBehaviour
{

    public float waitTime = 0.55f;
    private int currentConnected = 0;
    private Movement player;
    private GameObject[] plataforms;
    public GameObject QuizUI;
    public GameObject FinalUI;
    private TMP_Text dialogoText;
    private bool isWaiting = true;
    private Animator goComputer;
    private bool isOver;
    private AudioSource audioSource;

    void Awake(){
        StartCoroutine(getAllReferences());
    }

    void Update()
    {
        if(!isWaiting){
            if (player.GetComponent<Movement>().gameOver && currentConnected != plataforms.Length) {
                GameObject.Find("GM").GetComponent<Console>().resetLevel();
                player.gameOver = false;
            }
            if (currentConnected == plataforms.Length){
                isWaiting = true;
                StartCoroutine(openFinalUI());
            }
        }
    }

    public void increasePlataformCount(GameObject computer){
        isWaiting = true;
        goComputer = computer.GetComponent<Animator>();
        StartCoroutine(openQuizUI());
    }

    IEnumerator getAllReferences(){
        isWaiting = true;
        yield return new WaitForSeconds(1f);
        plataforms = GameObject.FindGameObjectsWithTag("Plataform");
        audioSource = this.GetComponent<AudioSource>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Movement>();
        dialogoText = QuizUI.transform.GetChild(0).GetComponent<TMP_Text>();
        isWaiting = false;
    }

    public void checkResult(bool answer) {
        if (plataforms[currentConnected].GetComponent<Plataform>().answer == answer) {
            QuizUI.GetComponent<Image>().color = new Color(0.1f, 0.8f, 0.1f);
            StartCoroutine(closeQuizUI());
        }
        else {
            QuizUI.GetComponent<Image>().color = new Color(0.8f, 0.1f, 0.1f);
            StartCoroutine(resetingLevel());
        }
    }

    IEnumerator openQuizUI(){
        QuizUI.SetActive(true);
        yield return new WaitForSeconds(waitTime);
        Time.timeScale = 0f;
        dialogoText.text = plataforms[currentConnected].GetComponent<Plataform>().quiz;
    }

    IEnumerator closeQuizUI(){
        QuizUI.transform.GetChild(1).GetComponent<Button>().enabled = false;
        QuizUI.transform.GetChild(2).GetComponent<Button>().enabled = false;
        QuizUI.GetComponent<Animator>().SetBool("CloseQuiz", true);
        Time.timeScale = 1f;
        yield return new WaitForSeconds(waitTime);
        isWaiting = false;
        dialogoText.text = "";
        QuizUI.transform.GetChild(1).GetComponent<Button>().enabled = true;
        QuizUI.transform.GetChild(2).GetComponent<Button>().enabled = true;
        QuizUI.SetActive(false);
        goComputer.SetBool("ComputerOn", true);
        audioSource.Play();
        goComputer.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        currentConnected++;
        QuizUI.GetComponent<Image>().color = new Color(1f, 1f, 1f);
    }

    IEnumerator resetingLevel(){
        QuizUI.transform.GetChild(1).GetComponent<Button>().enabled = false;
        QuizUI.transform.GetChild(2).GetComponent<Button>().enabled = false;
        dialogoText.text = "";
        Time.timeScale = 1f;
        QuizUI.GetComponent<Animator>().SetBool("CloseQuiz", true);
        GameObject.Find("GM").GetComponent<Console>().resetLevel();
        currentConnected = 0;
        yield return new WaitForSeconds(waitTime);
        QuizUI.GetComponent<Image>().color = new Color(1f, 1f, 1f);
    }


    IEnumerator openFinalUI(){
        FinalUI.SetActive(true);
        GameObject.Find("GM").GetComponent<Console>().disableScene();
        int[] result = player.GetComponent<Movement>().getScore();
        FinalUI.transform.GetChild(1).GetComponent<TMP_Text>().text += " " + result[0];
        FinalUI.transform.GetChild(2).GetComponent<TMP_Text>().text += " " + result[1];
        FinalUI.transform.GetChild(3).GetComponent<TMP_Text>().text += " " + result[2];
        yield return new WaitForSeconds(1f);
    }

    public void nextLevel() {
        FinalUI.SetActive(false);
        StartCoroutine(GameObject.Find("GM").GetComponent<Console>().LoadNewSceneAfterTransition());
    }
}
