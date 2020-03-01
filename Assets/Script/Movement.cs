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

    // Start is called beore the first frame update
    void Start()
    {
        textInput = GameObject.Find("InputField (TMP)").GetComponent<TMP_InputField>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void compile(){
        commands = textInput.text.Split(char.Parse("\n"));
        StartCoroutine(movePlayer());
    }

    private IEnumerator movePlayer(){
        int index = 0;
        Vector3 movement = new Vector3(0f,0f,0f);
        while (index != commands.Length){
            Debug.Log(commands[index]);
            if (commands[index] == contants.inputLeft) {
                movement = new Vector3(-1f, 0f, 0f);
            }
            else if (commands[index] == contants.inputRight) {
                movement = new Vector3(1f, 0f, 0f);
            }
            else if (commands[index] == contants.inputUp) {
                movement = new Vector3(0, 1f, 0f);
            }
            else if (commands[index] == contants.inputDown) {
                movement = new Vector3(0f, -1f, 0f);
            }
            yield return new WaitForSeconds(waitTime);
            index++;
            this.transform.position += movement;
        }
    }
}
