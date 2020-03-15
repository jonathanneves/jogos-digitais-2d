using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SetTextUI : MonoBehaviour
{

    private Constants constants;

    public Text trueText;
    public Text falseText;
    public TMP_Text titleUI;
    public TMP_Text rightText;
    public TMP_Text wrongText;
    public TMP_Text resetText;
    public Text nextButton;

    void Start()
    {
        constants = GameObject.Find("Loader").GetComponent<Constants>();
        trueText.text = constants.trueButton;
        falseText.text = constants.falseButton;
        titleUI.text = constants.finalTitleUI;
        rightText.text = constants.rightCommands;
        wrongText.text = constants.wrongCommands;
        resetText.text = constants.resetCount;
        nextButton.text = constants.buttonNext;
    }
}
