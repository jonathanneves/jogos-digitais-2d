using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plataform : MonoBehaviour
{
    public int totalConnected;
    private int currentConnected;
    public string quiz;
    public bool answer;

 
    void Start()
    {
        totalConnected = GameObject.FindGameObjectsWithTag("Plataform").Length;
    }

    void OnTriggerEnter2D(Collider2D col) {
        Debug.Log("Computador conectado");
        currentConnected++;
        if(currentConnected == totalConnected){
            Debug.Log("Completou");
        }
    }
}
