using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plataform : MonoBehaviour
{
    public string quiz;
    public bool answer;
 
    void OnTriggerEnter2D(Collider2D col) {
        Collider2D myCollider = this.GetComponent<Collider2D>();
        if (col.bounds.Intersects(myCollider.bounds)){
            GameObject.Find("CheckLevel").GetComponent<CheckPlataform>().increasePlataformCount(col.gameObject);
        }
    }
}
