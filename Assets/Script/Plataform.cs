using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plataform : MonoBehaviour
{
    public string quiz;
    public bool answer;
 
    void OnTriggerEnter2D(Collider2D col) {
        if(col.gameObject.CompareTag("Computer"))
            GameObject.Find("CheckLevel").GetComponent<CheckPlataform>().increasePlataformCount(col.transform.parent.gameObject);
    }
}