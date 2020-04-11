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
    private Animator animComputer;
    private bool isOver;
    private AudioSource audioSource;
    public AudioClip erroFx;
    public AudioClip successFx;
    public ParticleSystem particleComputerOn;
    private Transform tComputer;

    void Awake(){
        StartCoroutine(getAllReferences());
    }

    void Update()
    {
        if(!isWaiting){
            if (player.GetComponent<Movement>().gameOver && currentConnected != plataforms.Length) {
                FindObjectOfType<Console>().resetLevel();
                currentConnected = 0;
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
        animComputer = computer.GetComponent<Animator>();
        tComputer = computer.transform;
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
        enableButton(false);
    }

    public void checkResult(bool answer) {
        //Resposta Correta
        if (plataforms[currentConnected].GetComponent<Plataform>().answer == answer) {
            QuizUI.GetComponent<Image>().color = new Color(0.1f, 0.8f, 0.1f);
            StartCoroutine(closeQuizUI());
        }
        //Resposta Errada
        else {
            QuizUI.GetComponent<Image>().color = new Color(0.8f, 0.1f, 0.1f);
            StartCoroutine(resetingLevel());
        }
    }

    IEnumerator openQuizUI(){
        QuizUI.SetActive(true);
        yield return new WaitForSeconds(waitTime);
        dialogoText.text = plataforms[currentConnected].GetComponent<Plataform>().quiz;
        player.stopPlayer = true;
        enableButton(true);
    }

    IEnumerator closeQuizUI(){
        enableButton(false);
        player.stopPlayer = false;
        QuizUI.GetComponent<Animator>().SetBool("CloseQuiz", true);
        animComputer.SetBool("ComputerOn", true);
        yield return new WaitForSeconds(waitTime);
        ParticleSystem particleInstance = Instantiate(particleComputerOn, tComputer);
        isWaiting = false;
        dialogoText.text = "";
        QuizUI.SetActive(false);
        Destroy(particleInstance, 0.5f);
        audioSource.PlayOneShot(successFx);
        currentConnected++;
        QuizUI.GetComponent<Image>().color = new Color(1f, 1f, 1f);
    }

    IEnumerator resetingLevel(){
        enableButton(false);
        player.stopPlayer = false;
        QuizUI.GetComponent<Animator>().SetBool("CloseQuiz", true);
        audioSource.PlayOneShot(erroFx);
        currentConnected = 0;
        yield return new WaitForSeconds(waitTime);
        GameObject.Find("GM").GetComponent<Console>().resetLevel();
        QuizUI.SetActive(false);
        dialogoText.text = "";
        QuizUI.GetComponent<Image>().color = new Color(1f, 1f, 1f);
    }


    IEnumerator openFinalUI(){
        FindObjectOfType<Timer>().enabled = false;
        FinalUI.SetActive(true);
        player.stopPlayer = true;
        GameObject.Find("GM").GetComponent<Console>().disableScene();
        setResultadoFinal();      
        yield return new WaitForSeconds(1f);
    }

    public void nextLevel() {
        FinalUI.SetActive(false);
        StartCoroutine(GameObject.Find("GM").GetComponent<Console>().LoadNewSceneAfterTransition());
    }

    private void enableButton(bool status){
        QuizUI.transform.GetChild(1).GetComponent<Button>().enabled = status;
        QuizUI.transform.GetChild(2).GetComponent<Button>().enabled = status;
    }

    private void setResultadoFinal(){
        int[] result = player.GetComponent<Movement>().getScore();
        FinalUI.transform.GetChild(1).GetComponent<TMP_Text>().text += " " + result[0];
        FinalUI.transform.GetChild(2).GetComponent<TMP_Text>().text += " " + result[1];
        FinalUI.transform.GetChild(3).GetComponent<TMP_Text>().text += " " + result[2];
    }
}
