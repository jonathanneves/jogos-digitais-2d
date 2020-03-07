using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Xml.Linq;
using System.IO;

public class Constants : MonoBehaviour
{
    [HideInInspector] public string inputLeft = "esquerda";
    [HideInInspector] public string inputRight = "direita";
    [HideInInspector] public string inputUp = "cima";
    [HideInInspector] public string inputDown = "baixo";
    [HideInInspector] public string placeholderInput;
    [HideInInspector] public string falseButton;
    [HideInInspector] public string trueButton;
    [HideInInspector] public string wrongCommands;
    [HideInInspector] public string rightCommands;
    [HideInInspector] public string finalTitleUI;
    [HideInInspector] public string buttonNext;
    public Text playText;
    public Text creditsText;
    public Text exitText;

    public void mappingUI(IEnumerable<XElement> constants) {
        foreach (var map in constants) {
            playText.text = map.Parent.Element("buttonPlay").Value.Trim();
            creditsText.text = map.Parent.Element("buttonCredits").Value.Trim();
            exitText.text = map.Parent.Element("buttonExit").Value.Trim();
            inputLeft = map.Parent.Element("inputLeft").Value.Trim();
            inputRight = map.Parent.Element("inputRight").Value.Trim();
            inputUp = map.Parent.Element("inputUp").Value.Trim();
            inputDown = map.Parent.Element("inputDown").Value.Trim();
            placeholderInput = map.Parent.Element("placeholder").Value.Trim();
            falseButton = map.Parent.Element("falseButton").Value.Trim();
            trueButton = map.Parent.Element("trueButton").Value.Trim();
            wrongCommands = map.Parent.Element("wrongCommands").Value.Trim();
            rightCommands = map.Parent.Element("rightCommands").Value.Trim();
            finalTitleUI = map.Parent.Element("finalTitleUI").Value.Trim();
            buttonNext = map.Parent.Element("buttonNext").Value.Trim();
        }
    }
}
