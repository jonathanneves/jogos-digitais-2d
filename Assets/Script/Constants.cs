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
    [HideInInspector] public string placeholderInput = ">Digite um comando...";
    [HideInInspector] public string falseButton = "Falso";
    [HideInInspector] public string trueButton = "Verdadeiro";
    [HideInInspector] public string wrongCommands = "Comandos Errados:";
    [HideInInspector] public string rightCommands = "Comandos Corretos:";
    [HideInInspector] public string finalTitleUI = "Compilado Com Sucesso!";
    [HideInInspector] public string buttonNext = "Próxima Fase";
    [HideInInspector] public string resetCount = "Vezes Resetados:";
    [HideInInspector] public string theEndText = "Agora eu já aprendi o suficiente e posso agir por conta própria. Obrigado por Jogar Humano!";
    [HideInInspector] public string currentScene = "Fase 1";
    [HideInInspector] public string playText = "Jogar";
    [HideInInspector] public string creditsText = "Créditos";
    [HideInInspector] public string exitText = "Jogar";
    [HideInInspector] public string titleCredits = "Creditos";
    [HideInInspector] public string buttonBack = "Voltar";

    public void mappingUI(IEnumerable<XElement> constants) {
        foreach (var map in constants) {
            playText = map.Parent.Element("buttonPlay").Value.Trim();
            creditsText = map.Parent.Element("buttonCredits").Value.Trim();
            exitText = map.Parent.Element("buttonExit").Value.Trim();
            titleCredits = map.Parent.Element("titleCredits").Value.Trim();
            buttonBack = map.Parent.Element("buttonBack").Value.Trim();
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
            resetCount = map.Parent.Element("resetCount").Value.Trim();
            theEndText = map.Parent.Element("theEndText").Value.Trim();
        }
        FindObjectOfType<MenuManager>().setAllText();
    }
}
