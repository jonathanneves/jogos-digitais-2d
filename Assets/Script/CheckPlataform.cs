using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CheckPlataform : MonoBehaviour
{

    public float waitTime = 0.6f;
    private int currentConnected = 0;
    private Movement player;
    private GameObject[] plataforms;
    public GameObject QuizUI;
    public GameObject FinalUI;
    private TMP_Text dialogoText;
    private bool isWaiting = true;
    private Animator goComputer;
    private bool isOver;

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
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Movement>();
        dialogoText = QuizUI.transform.GetChild(0).GetComponent<TMP_Text>();
        isWaiting = false;
    }

    public void checkResult(bool answer) {
        if (plataforms[currentConnected].GetComponent<Plataform>().answer == answer) {
            QuizUI.GetComponent<Image>().color = new Color(0, 0.8f, 0);
            StartCoroutine(closeQuizUI());
        }
        else {
            QuizUI.GetComponent<Image>().color = new Color(0.8f, 0, 0);
            StartCoroutine(resetingLevel());
        }
    }

    IEnumerator openQuizUI(){
        QuizUI.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        Time.timeScale = 0;
        dialogoText.text = plataforms[currentConnected].GetComponent<Plataform>().quiz;
    }

    IEnumerator closeQuizUI(){
        QuizUI.transform.GetChild(1).GetComponent<Button>().enabled = false;
        QuizUI.transform.GetChild(2).GetComponent<Button>().enabled = false;
        Time.timeScale = 1;
        QuizUI.GetComponent<Animator>().SetBool("CloseQuiz", true);
        yield return new WaitForSeconds(waitTime);
        isWaiting = false;
        QuizUI.transform.GetChild(1).GetComponent<Button>().enabled = true;
        QuizUI.transform.GetChild(2).GetComponent<Button>().enabled = true;
        QuizUI.SetActive(false);
        goComputer.SetBool("ComputerOn", true);
        currentConnected++;

    }

    IEnumerator resetingLevel(){
        QuizUI.transform.GetChild(1).GetComponent<Button>().enabled = false;
        QuizUI.transform.GetChild(2).GetComponent<Button>().enabled = false;
        Time.timeScale = 1;
        QuizUI.GetComponent<Animator>().SetBool("CloseQuiz", true);
        GameObject.Find("GM").GetComponent<Console>().resetLevel();
        currentConnected = 0;
        yield return new WaitForSeconds(waitTime);
    }


    IEnumerator openFinalUI(){
        FinalUI.SetActive(true);
        GameObject.Find("GM").GetComponent<Console>().disableScene();
        int[] result = player.GetComponent<Movement>().getScore();
        FinalUI.transform.GetChild(1).GetComponent<TMP_Text>().text += " " + result[0];
        FinalUI.transform.GetChild(2).GetComponent<TMP_Text>().text += " " + result[1];
        yield return new WaitForSeconds(1f);
        //Time.timeScale = 0;
    }

    public void nextLevel() {
        FinalUI.SetActive(false);
        StartCoroutine(GameObject.Find("GM").GetComponent<Console>().LoadNewSceneAfterTransition());
    }
}
