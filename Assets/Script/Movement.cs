using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Movement : MonoBehaviour
{
    private TMP_InputField textInput;
    private Constants contants = new Constants();
    private string[] commands;
    public float waitTime = 0.5f;
    public float moveSpeed = 5f;

    private Rigidbody2D rb;
    private Vector2 movement;
    private bool isWalking = false;
    private Animator animator;
    private float targetTime;
    public float startTime = 1f;

    void Start()
    {
        textInput = GameObject.Find("InputField (TMP)").GetComponent<TMP_InputField>();
        rb = this.GetComponent<Rigidbody2D>();
        animator = this.GetComponent<Animator>();
    }

    void Update()
    {
        if(isWalking){
            isWalking = false;
            StartCoroutine(startMovement());
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    public void compile(){
        commands = textInput.text.Split(char.Parse("\n"));
        isWalking = true;
        //StartCoroutine(movePlayer());
    }
    /*
    private IEnumerator movePlayer(){
        int index = 0;
        Vector3 movement = new Vector3(0f,0f,0f);
        while (index != commands.Length){
            Debug.Log(commands[index]);
            if (commands[index] == contants.inputLeft) {
                movement.x = -1f;
            }
            else if (commands[index] == contants.inputRight) {
                movement.x = 1f; 
            }
            else if (commands[index] == contants.inputUp) {
                movement.y = 1f;
            }
            else if (commands[index] == contants.inputDown) {
                movement.y = -1f;
            }
            yield return new WaitForSeconds(waitTime);
            index++;
            this.transform.position += movement;
        }
    }*/
    
    private IEnumerator startMovement(){
        int index = 0;
        movement = new Vector2(0f, 0f);
        while (index != commands.Length) {
            movement = newMovement(index);
            Debug.Log(index + ">>" + movement);
            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", movement.y);
            animator.SetFloat("speed", movement.magnitude);
            yield return new WaitForSeconds(waitTime);
            movement = new Vector2(0f, 0f);
            index++;
        }
    }

    private Vector2 newMovement(int index){
        if (commands[index].ToLower() == contants.inputLeft) {
            movement.x = -1f;
        }
        else if (commands[index].ToLower() == contants.inputRight) {
            movement.x = 1f;
        }
        else if (commands[index].ToLower() == contants.inputUp) {
            movement.y = 1f;
        }
        else if (commands[index].ToLower() == contants.inputDown) {
            movement.y = -1f;
        }
        return movement;  
    }
}
