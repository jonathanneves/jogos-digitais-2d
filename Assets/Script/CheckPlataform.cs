using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CheckPlataform : MonoBehaviour
{
    private int currentConnected = 0;
    private GameObject player;
    private GameObject[] plataforms;
    public GameObject QuizUI;
    public GameObject FinalUI;
    public TMP_Text dialogoText;
    private bool isWaiting = true;

    void Start(){
        StartCoroutine(getAllPlataforms());
    }

    void Update()
    {
        if(!isWaiting){
            if (player.GetComponent<Movement>().gameOver && currentConnected != plataforms.Length) {
                GameObject.Find("GM").GetComponent<Console>().resetLevel();
                getAllPlataforms();
            }
        }
    }

    public void increasePlataformCount(GameObject computer){
        computer.GetComponent<Animator>().SetBool("ComputerOn", true);
        StartCoroutine(openQuizUI());
        if(currentConnected == plataforms.Length){
            Debug.Log("Fim do Level");
        }
    }

    IEnumerator getAllPlataforms(){
        yield return new WaitForSeconds(1f);
        plataforms = GameObject.FindGameObjectsWithTag("Plataform");
        player = GameObject.FindGameObjectWithTag("Player");
        isWaiting = false;
    }

    public void checkResult(bool answer) {
        if (plataforms[currentConnected].GetComponent<Plataform>().answer == answer) {
            StartCoroutine(closeQuizUI());
        }
        else {
            StartCoroutine(resetingLevel());
        }
    }

    IEnumerator openQuizUI(){
        QuizUI.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        //Time.timeScale = 0;
        dialogoText.text = plataforms[currentConnected].GetComponent<Plataform>().quiz;
    }

    IEnumerator closeQuizUI(){
        currentConnected++;
        //Time.timeScale = 1;
        QuizUI.GetComponent<Animator>().SetBool("CloseQuiz", true);
        Debug.Log(currentConnected);
        yield return new WaitForSeconds(0.4f);
        QuizUI.SetActive(false);
    }

    IEnumerator resetingLevel(){
        GameObject.Find("GM").GetComponent<Console>().resetLevel();
        currentConnected = 0;
        yield return new WaitForSeconds(1f);
    }

}
