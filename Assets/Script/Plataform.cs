using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plataform : MonoBehaviour
{
    public string quiz;
    public bool answer;
 
    void OnTriggerEnter2D(Collider2D col) {
        if(col.gameObject.CompareTag("Computer")){
            GameObject parent = col.transform.parent.gameObject;
            parent.GetComponent<Rigidbody2D>().isKinematic = true;
            parent.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            parent.transform.position = this.transform.position;
            GameObject.Find("CheckLevel").GetComponent<CheckPlataform>().increasePlataformCount(parent);
        }
    }
}