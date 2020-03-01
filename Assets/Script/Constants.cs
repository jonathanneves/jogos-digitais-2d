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
    public Text titleText;
    public Text playText;
    public Text creditsText;
    public Text exitText;

    public void mappingUI(IEnumerable<XElement> constants) {
        foreach (var map in constants) {
            titleText.text = map.Parent.Element("titleLanguage").Value.Trim();
            playText.text = map.Parent.Element("buttonPlay").Value.Trim();
            creditsText.text = map.Parent.Element("buttonCredits").Value.Trim();
            exitText.text = map.Parent.Element("buttonExit").Value.Trim();
            inputLeft = map.Parent.Element("inputLeft").Value.Trim();
            inputRight = map.Parent.Element("inputRight").Value.Trim();
            inputUp = map.Parent.Element("inputUp").Value.Trim();
            inputDown = map.Parent.Element("inputDown").Value.Trim();
        }
    }
}
