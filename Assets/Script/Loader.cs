using UnityEngine;
using System.Collections;
using System.Collections.Generic; //Needed for Lists
using System.Xml; //Needed for XML functionality
using System.Xml.Serialization; //Needed for XML Functionality
using System.IO;
using System.Xml.Linq; //Needed for XDocument
using UnityEngine.SceneManagement;

public class Loader : MonoBehaviour {

    XDocument xmlDoc; //create Xdocument. Will be used later to read XML file 
    IEnumerable<XElement> items; //Create an Ienumerable list. Will be used to store XML Items. 
    IEnumerable<XElement> mappings; //Create an Ienumerable list. Will be used to store XML Items. 
    public List <XMLData> data = new List <XMLData>(); //Initialize List of XMLData objects.
    int iteration = 1, pageNum = 1;
    string dialogueText;
    bool finishedLoading = false;

    void Start(){
        if(this != null)
            currentLanguage("portuguese");
    }

    void LoadXML(string currentLanguage) {
        //Assigning Xdocument xmlDoc. Loads the xml file from the file path listed.
        if(finishedLoading)
            resetVariables();
        xmlDoc = XDocument.Load("Assets/Resources/"+currentLanguage+".xml");
        //This basically breaks down the XML Document into XML Elements. Used later. 
        mappings = xmlDoc.Descendants("mappingUI").Elements();
        items = xmlDoc.Descendants("page").Elements();
    }

    //this is our coroutine that will actually read and assign the XML data to our List 
    IEnumerator AssignData()
    {
        this.GetComponent<Constants>().mappingUI(mappings);

        foreach (var item in items)
        {
            //Determine if the <page number> attribute in the XML is equal to whatever our current iteration of the loop is. If it is, then we want to assign our variables to the value of the XML Element that we need.     
            if(item.Parent.Attribute("number").Value == iteration.ToString())
            {
                pageNum = int.Parse(item.Parent.Attribute("number").Value);
  
                dialogueText = item.Parent.Element("dialogue").Value.Trim();
                //Create a new Index in the List, which will be a new XMLData object and pass the previously assigned variables as arguments so they get assigned to the new object’s variables.
                data.Add(new XMLData(pageNum, dialogueText));
                //To test and make sure the data has been applied to properly, print out the musicClip name from the data list’s current index. This will let us know if the objects in the list have been created successfully and if their variables have been assigned the right values.
                Debug.Log(data[iteration-1].dialogueText);
                iteration++; //increment the iteration by 1
            }   
        }
        finishedLoading = true; //tell the program that we’ve finished loading data. 
        yield return null;
    }

    public void currentLanguage(string language){
        DontDestroyOnLoad(gameObject); //Allows Loader to carry over into new scene 
        LoadXML(language); //Loads XML File. 
        StartCoroutine("AssignData"); //Starts assigning XML data to data List. 
    }
    
    void resetVariables(){
        iteration = 1; pageNum = 1;
        data.Clear();
    }
}

public class XMLData {
    
    public int pageNum;
    public string charText, dialogueText;
    // Create a constructor that will accept multiple arguments that can be assigned to our variables. 
    public XMLData (int page, string dialogue)
    {
        pageNum = page;

        //Trocando os {} por <>  porque o xml não funciona com <> + caracter especial
        dialogue = dialogue.Replace("{", "<");
        dialogueText = dialogue.Replace("}", ">");
    }
}