using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    float timer = 0;
    public TMP_Text timerTxt;

    
    void Update() {

        timer += Time.deltaTime;

        string minutes = Mathf.Floor(timer / 60).ToString("00");
        string seconds = (timer % 60).ToString("00");

        timerTxt.text = minutes + ":" + seconds;
    }
}
