using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour
{
    private Constants contants = new Constants();
    public float waitTime = 0.5f;
    public float moveSpeed = 5f;
    private Console console;

    private Vector2 currentPos;
    private Rigidbody2D rb;
    private Vector2 movement;
    private bool canMove = false;
    private Animator animator;
    private Color actualColor;
    [HideInInspector] public bool gameOver = false;

    private int wrongCommands = 0;
    private int rightCommands = 0;

    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        animator = this.GetComponent<Animator>();
        console = GameObject.Find("GM").GetComponent<Console>();
    }

    void Update()
    {
        if (console.isCompiling){
            console.isCompiling = false;
            StartCoroutine(startMovement());
        }
    }

    void FixedUpdate()
    {
        if(canMove){
            if(Vector2.Distance(this.transform.position, movement) > 0.01){
                transform.position = Vector2.MoveTowards(transform.position, movement, moveSpeed * Time.fixedDeltaTime);
            }
        }
    }
    
    private IEnumerator startMovement(){
        int index = 0;
        while (index != console.commands.Length) {
            movement = newMovement(index);     

            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", movement.y);
            animator.SetFloat("speed", movement.magnitude);

            canMove = true;
            yield return new WaitForSeconds(waitTime);
            canMove = false;
            movement = new Vector2(0f, 0f);
            index++;
        }
        resetStatus();
    }

    private Vector2 newMovement(int index){
        currentPos = new Vector2(Mathf.Round(this.transform.position.x), Mathf.Round(this.transform.position.y));
        string currentCommand = console.commands[index].ToLower();
        if (currentCommand == contants.inputLeft) {
            movement.x = -1f;
            rightCommands++;
        }
        else if (currentCommand == contants.inputRight) {
            movement.x = 1f;
            rightCommands++;
        }
        else if (currentCommand == contants.inputUp) {
            movement.y = 1f;
            rightCommands++;
        }
        else if (currentCommand == contants.inputDown) {
            movement.y = -1f;
            rightCommands++;
        }
        else
            wrongCommands++;

        movement = movement + currentPos;
        return movement;
    }

    private void resetStatus(){
        console.resetUiStatus();
        canMove = false;
        console.isCompiling = false;
        gameOver = true;
    }
}
