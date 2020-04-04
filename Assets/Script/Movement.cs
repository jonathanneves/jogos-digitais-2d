using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour
{
    private Constants constants;
    public float waitTime = 0.55f;
    public float moveSpeed = 1.5f;
    private Console console;

    private Vector2 currentPos;
    private Rigidbody2D rb;
    private Vector2 movement;
    private Color actualColor;
    private Animator animator;

    [HideInInspector] public bool canMove = false;
    [HideInInspector] public bool stopPlayer = false;
    [HideInInspector] public bool gameOver = false;

    private int wrongCommands = 0;
    private int rightCommands = 0;
    private Color[] cores = {Color.magenta, Color.yellow, Color.red, Color.green, Color.cyan};
    private int indexColor = 0;
    private SpriteRenderer spritePlayer;

    void Start()
    {
        constants = FindObjectOfType<Constants>();
        rb = this.GetComponent<Rigidbody2D>();
        animator = this.GetComponent<Animator>();
        console = FindObjectOfType<Console>();
        spritePlayer = this.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (console.isCompiling){
            wrongCommands = 0;
            rightCommands = 0;
            console.isCompiling = false;
            StartCoroutine(startMovement());
        }
        if (canMove) {
            if (Vector2.Distance(this.transform.position, movement) > 0.01) {
                transform.position = Vector2.MoveTowards(transform.position, movement, moveSpeed * Time.deltaTime);
            }
        } 
        if(Input.GetKeyDown(KeyCode.F5)){
            indexColor++;
            if (indexColor == cores.Length)
                indexColor = 0;
            spritePlayer.color = cores[indexColor];
        }
    }
    
    private IEnumerator startMovement(){
        int index = 0;
        while (index != console.commands.Length) {

            if(!stopPlayer){
                movement = newMovement(index);     

                animator.SetFloat("Horizontal", movement.x);
                animator.SetFloat("Vertical", movement.y);
                animator.SetFloat("speed", movement.magnitude);

                canMove = true;
                yield return new WaitForSeconds(waitTime);
                canMove = false;
                movement = new Vector2(0f, 0f);
                index++;
            } else {
                canMove = false;
                yield return new WaitForSeconds(waitTime);
                movement = new Vector2(0f, 0f);
            }
        }
        resetStatus();
    }

    private Vector2 newMovement(int index){

        currentPos = new Vector2(Mathf.Round(this.transform.position.x), Mathf.Round(this.transform.position.y));
        string currentCommand = console.commands[index].ToLower();

        if (currentCommand == constants.inputLeft) {
            movement.x = -1f;
        }
        else if (currentCommand == constants.inputRight) {
            movement.x = 1f;
        }
        else if (currentCommand == constants.inputUp) {
            movement.y = 1f;
        }
        else if (currentCommand == constants.inputDown) {
            movement.y = -1f;
        }
        else{
            wrongCommands++;
            StartCoroutine(console.GetComponent<Console>().redConsole());
        }
        rightCommands++;
        movement = movement + currentPos;
        return movement;
    }

    private void resetStatus(){
        console.resetUiStatus();
        canMove = false;
        console.isCompiling = false;
        gameOver = true;
    }

    public int[] getScore(){
        int[] score = new int[3];
        score[0] = rightCommands;
        score[1] = wrongCommands;
        score[2] = console.countReset;
        return score;
    }
}

