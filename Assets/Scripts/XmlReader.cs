using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml; //Needed for XML functionality
using System.Xml.Serialization; //Needed for XML Functionality
using System.IO;
using System.Xml.Linq;
using System;

public class XmlReader : MonoBehaviour
{
    XmlDocument questionXml = new XmlDocument();
    XmlDocument dataXml = new XmlDocument();
    private static XmlReader _instance;
    // Start is called before the first frame update
    public static XmlReader Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<XmlReader>();
            }

            return _instance;
        }
    }
    void Start()
    {
        LoadXML();
    }

    void LoadXML()
    {
        questionXml.Load(Application.dataPath + "/XML/Questions.xml");
        dataXml.Load(Application.dataPath + "/XML/data.xml");

    }

    void ReadQuestions()
    {
        XmlNodeList countriesList = questionXml.GetElementsByTagName("pais");
        foreach (XmlNode countries in countriesList)
        {
            XmlNodeList questioncontent = countries.ChildNodes;
            foreach (XmlNode question in questioncontent)
            {
                if(question.Name == "nombre")
                {
                    Debug.Log("El nombre del Pais es: "+question.InnerText);
                }
                if(question.Name == "pregunta")
                {
                    XmlNodeList preguntaDezglozada = question.ChildNodes;
                    foreach (XmlNode dezgloce in question)
                    {
                        if(dezgloce.Name == "question")
                        {
                            Debug.Log("La Pregunta es:" +dezgloce.InnerText);
                        }
                    }
                }
            }
        }
    }

    public string[] ReturnOneRandomQuestion(string country)
    {

        int random = UnityEngine.Random.Range(0,8);
        int counter = 0;
        string[] returnValue = new string[6];
        bool hasFoundTheCountry = false;

        XmlNodeList countriesList = questionXml.GetElementsByTagName("pais");
        foreach (XmlNode countries in countriesList)
        {

            XmlNodeList questioncontent = countries.ChildNodes;
            foreach (XmlNode question in questioncontent)
            {
                if (question.Name == "nombre")
                {

                    if (question.InnerText == country)
                    {

                        hasFoundTheCountry = true;
                    }
                }
                if (question.Name == "pregunta" && hasFoundTheCountry)
                {
                    if (counter == random)
                    {
                        XmlNodeList preguntaDezglozada = question.ChildNodes;

                        foreach (XmlNode dezgloce in preguntaDezglozada)
                        {
                            if (dezgloce.Name == "question")
                            {
                                returnValue[0] = dezgloce.InnerText;
                            }
                            if (dezgloce.Name == "respuesta1")
                            {
                                returnValue[1] = dezgloce.InnerText;
                            }
                            if (dezgloce.Name == "respuesta2")
                            {
                                returnValue[2] = dezgloce.InnerText;
                            }
                            if (dezgloce.Name == "respuesta3")
                            {
                                returnValue[3] = dezgloce.InnerText;
                            }
                            if (dezgloce.Name == "respuesta4")
                            {
                                returnValue[4] = dezgloce.InnerText;
                            }
                            if (dezgloce.Name == "correcta")
                            {
                                returnValue[5] = dezgloce.InnerText;
                            }
                        }
                        return returnValue;
                    }
                    counter++;
                }
            }
        }
        return returnValue;
    }


    public string[] ReturnOneSpecificQuestion(string country, int qstn)
    {
        XmlNodeList countriesList = questionXml.GetElementsByTagName("pais");
        int random = qstn;
        int counter = 0;
        string[] returnValue = new string[6];
        bool hasFoundTheCountry = false;
        foreach (XmlNode countries in countriesList)
        {
            XmlNodeList questioncontent = countries.ChildNodes;
            foreach (XmlNode question in questioncontent)
            {
                if (question.Name == "nombre")
                {
                    if (question.InnerText == country)
                    {
                        hasFoundTheCountry = true;
                    }
                }
                if (question.Name == "pregunta" && hasFoundTheCountry)
                {
                    if (counter == random)
                    {
                        XmlNodeList preguntaDezglozada = question.ChildNodes;

                        foreach (XmlNode dezgloce in question)
                        {

                            if (dezgloce.Name == "question")
                            {
                                returnValue[0] = dezgloce.InnerText;
                            }
                            if (dezgloce.Name == "respuesta1")
                            {
                                returnValue[1] = dezgloce.InnerText;
                            }
                            if (dezgloce.Name == "respuesta2")
                            {
                                returnValue[2] = dezgloce.InnerText;
                            }
                            if (dezgloce.Name == "respuesta3")
                            {
                                returnValue[3] = dezgloce.InnerText;
                            }
                            if (dezgloce.Name == "respuesta4")
                            {
                                returnValue[4] = dezgloce.InnerText;
                            }
                            if (dezgloce.Name == "correcta")
                            {
                                returnValue[5] = dezgloce.InnerText;
                            }
                        }
                    }
                    counter++;
                }
            }
        }


        return returnValue;
    }

    public List<string> GetUnlockedCountries()
    {
        XmlNodeList countriesList = dataXml.GetElementsByTagName("Paises");
        List<string> unlockedCountries= new List<string>();

        foreach (XmlNode unlock in countriesList)
        {
            XmlNodeList lastLayer = unlock.ChildNodes;

            foreach(XmlNode node in lastLayer)
            {
                unlockedCountries.Add(node.Name);
            }

        }
            return unlockedCountries;
    }

    public void UnlockANewCountry(string countryName)
    {
        XmlElement node = dataXml.CreateElement(countryName);
        node.InnerText = "N/A";
        dataXml.DocumentElement.AppendChild(node);
        dataXml.Save(Application.dataPath + "/XML/data.xml");
        dataXml.Load(Application.dataPath + "/XML/data.xml");
    }
    public bool IsItUnlocked(string countryName)
    {
        XmlNodeList countriesList = dataXml.GetElementsByTagName("Paises");
        List<string> unlockedCountries = new List<string>();

        foreach (XmlNode unlock in countriesList)
        {
            XmlNodeList lastLayer = unlock.ChildNodes;

            foreach (XmlNode node in lastLayer)
            {
                if(node.Name == countryName)
                {
                    return true;
                }
            }

        }

        return false;
    }
    public string GetHigerScore(string countryName)
    {
        string returnValue = "N/A";

        XmlNodeList countriesList = dataXml.GetElementsByTagName("Paises");
        foreach (XmlNode unlock in countriesList)
        {
            XmlNodeList lastLayer = unlock.ChildNodes;

            foreach (XmlNode node in lastLayer)
            {
                if (node.Name == countryName)
                {
                    return node.InnerText;
                }
            }
        }

        return returnValue;
    }

    public void SetNewHighScore(string countryName, int newScore)
    {
        XmlNodeList countriesList = dataXml.GetElementsByTagName("Paises");
        foreach (XmlNode unlock in countriesList)
        {
            XmlNodeList lastLayer = unlock.ChildNodes;

            foreach (XmlNode node in lastLayer)
            {
               if(node.Name == countryName)
                {
                    node.InnerText = newScore.ToString();
                    dataXml.Save(Application.dataPath + "/XML/data.xml");
                    dataXml.Load(Application.dataPath + "/XML/data.xml");

                    return;
                }
            }

        }

    }
}
