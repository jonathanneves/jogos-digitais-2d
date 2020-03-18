using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plataform : MonoBehaviour
{
    public string quiz;
    public bool answer;
 
    void OnTriggerEnter2D(Collider2D col) {
        //Collider2D myCollider = this.GetComponent<Collider2D>();
        //Debug.Log(Vector2.Distance(col.gameObject.transform.position, this.transform.position));
        //if (Vector2.Distance(col.gameObject.transform.position, this.transform.position) <= 0.1
            if(col.gameObject.CompareTag("Computer")){
            //this.transform.position.Intersects(myCollider.bounds) && col.gameObject.CompareTag("Computer")){
                Debug.Log("Entrou");
                GameObject.Find("CheckLevel").GetComponent<CheckPlataform>().increasePlataformCount(col.transform.parent.gameObject);
        }
    }
}