using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestionHandler : MonoBehaviour
{
    private int respuesta;
    public bool isWaitingForAnswer;
    public TextMeshProUGUI pregunta;

    public GameObject correctTM;
    public TextMeshProUGUI cORRECT;
    public TextMeshProUGUI wrong;
    public TextMeshProUGUI timeUp;

    public TextMeshProUGUI timerText;
    public GameObject[] Botones;
    public TextMeshProUGUI[] textoBotones;

    private static QuestionHandler _instance;

    public static  QuestionHandler Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<QuestionHandler>();
            }

            return _instance;
        }
    }


    public void ButtonPressedOne()
    {
        respuesta = 1;

    }
    public void ButtonPressedTwo()
    {
        respuesta = 2;

    }
    public void ButtonPressedThree()
    {
        respuesta = 3;

    }

    public void ButtonPressedFour()
    {
        respuesta = 4;

    }

    void GetQuestion(string question)
    {
        string[] randomQuestion = XmlReader.Instance.ReturnOneRandomQuestion(question);
        for(int i = 0; i < randomQuestion.Length; i++)
        {
            Debug.Log(randomQuestion[i]);
        }
    }

    void GetSpecificQuestion(string country, int number)
    {
        string[] randomQuestion = XmlReader.Instance.ReturnOneSpecificQuestion(country, number);
        for (int i = 0; i < randomQuestion.Length; i++)
        {
            Debug.Log(randomQuestion[i]);
        }
    }
    public void MakeOneQuestion(string country)
    {
        StartCoroutine(MakeOneQuestionCorr(country));
    }

    int returnInArrayWay(string a)
    {
        if (a == "1")
            return 0;
        if (a == "2")
            return 1;
        if (a == "3")
            return 2;

        return 3;
    }

    int returnValueNormalWay(string a)
    {
        if (a == "1")
            return 1;
        if (a == "2")
            return 2;
        if (a == "3")
            return 3;

        return 4;
    }

    public void PutTheRightAnswer(int answer)
    {
       timeUp.text = "TIME'S UP";;
        Botones[answer].GetComponent<Image>().color = Color.green;
    }

    int[] return3RandomQuestionsNumber()
    {
        int[] returnValue = new int[3];
        int i = Random.Range(0, 8);
        returnValue[0] = i;
        i = Random.Range(0, 8);
        while(i == returnValue[0])
        {
            i = Random.Range(0, 8);
        }
        returnValue[1] = i;

        i = Random.Range(0, 8);
        while (i == returnValue[0] || i== returnValue[1])
        {
            i = Random.Range(0, 8);
        }
        returnValue[2] = i;
        return returnValue;
    }

    void MakeButtonsAndTextReady(string[] info)
    {
        pregunta.text = info[0];
        textoBotones[0].text = info[1];
        textoBotones[1].text = info[2];
        textoBotones[2].text = info[3];
        textoBotones[3].text = info[4];
    }

    IEnumerator MakeOneSpecificQuestioCorr(string country, int specificQuestion)
    {
        RestartAllSettings();
        string[] randomQuestion = XmlReader.Instance.ReturnOneSpecificQuestion(country,specificQuestion);
        MakeButtonsAndTextReady(randomQuestion);
        isWaitingForAnswer = true;
        float timer = 10f;
        bool timeuP = false;
        while (respuesta == 0 && !timeuP)
        {
            timer -= Time.deltaTime;
            if (timer < 0)
            {
                wasItRight = false;
                timeuP = true;
                AudioManager.Instance.TimesUp();
            }
            timerText.text = timer.ToString("F0");
            yield return null;
        }
        timerText.text = "";

        if (respuesta != 0)
        {
            ReviewAnswer(randomQuestion[5], respuesta);
        }
        else
        {
            PutTheRightAnswer(returnInArrayWay(randomQuestion[5]));
        }
        yield return new WaitForSeconds(5f);
        RestartAllSettings();
        isWaitingForAnswer = false;
    }

    IEnumerator MakeOneQuestionCorr(string country)
    {
        isWaitingForAnswer = true;
        RestartAllSettings();
        yield return new WaitForSeconds(1f);
        string[] randomQuestion = XmlReader.Instance.ReturnOneRandomQuestion(country);
        MakeButtonsAndTextReady(randomQuestion);
        float timer = 10f;
        bool timeuP = false;
        while (respuesta == 0 && !timeuP)
        {
            timer -= Time.deltaTime;
            if (timer < 0)
            {
                timeuP = true;
                AudioManager.Instance.TimesUp();
            }
            timerText.text = timer.ToString("F0");
            yield return null;
        }
        timerText.text = "";

        if (respuesta != 0)
        {
            ReviewAnswer(randomQuestion[5],respuesta);
        }
        else
        {
            PutTheRightAnswer(returnInArrayWay(randomQuestion[5]));
        }
        yield return new WaitForSeconds(5f);
        QuestionOpener.Instance.SetShouldItContinue(WasItRight(randomQuestion[5],respuesta));
        QuestionOpener.Instance.FinishedQuestion();
        RestartAllSettings();
        isWaitingForAnswer = false;
    }
    bool WasItRight(string rightAswer, int answerGiven){

      int rightAnser = returnValueNormalWay(rightAswer);
      if(answerGiven == rightAnser)
      return true;

      return false;
    }
    public void MakeThreeQuestions(string country)
    {
        StartCoroutine(MakeThreeQuestionsCorr(country));
    }

    bool wasItRight = false;
    IEnumerator MakeThreeQuestionsCorr(string country)
    {
      wasItRight = true;
        int[] questions = return3RandomQuestionsNumber();
        yield return new WaitForSeconds(.01f);
        isWaitingForAnswer = true;
        StartCoroutine(MakeOneSpecificQuestioCorr(country, questions[0]));
        while (isWaitingForAnswer)
        {
            yield return null;
        }
        isWaitingForAnswer = true;
        StartCoroutine(MakeOneSpecificQuestioCorr(country, questions[1]));
        while (isWaitingForAnswer)
        {
            yield return null;
        }
        isWaitingForAnswer = true;
        StartCoroutine(MakeOneSpecificQuestioCorr(country, questions[2]));
        while (isWaitingForAnswer)
        {
            yield return null;
        }

        QuestionOpener.Instance.QuestionsStopped(wasItRight);
    }

    private void ReviewAnswer(string rightAswer,int answerGiven)
    {
        int rightAnser = returnValueNormalWay(rightAswer);

        if(answerGiven == rightAnser)
        {
            AudioManager.Instance.PlayRight();
            correctTM.SetActive(true);
            cORRECT.text = "CORRECT!!";

            Botones[answerGiven-1].GetComponent<Image>().color = Color.green;
        }
        else
        {
          wasItRight = false;
          AudioManager.Instance.PlayWrong();
            wrong.text = "WRONG";
            Botones[answerGiven - 1].GetComponent<Image>().color = Color.red;
            Botones[rightAnser - 1].GetComponent<Image>().color = Color.green;
        }
    }

    private void RestartAllSettings()
    {
      correctTM.SetActive(false);
        respuesta = 0;

        cORRECT.text = "";
        wrong.text = "";
        timeUp.text = "";

        pregunta.text = "";
        timerText.text = "";
        for (int i = 0; i < textoBotones.Length; i++)
        {
            textoBotones[i].text="";
            Botones[i].GetComponent<Image>().color = Color.white;
        }
    }
}
