using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionOpener : MonoBehaviour
{
  public GameObject canvas;
  public bool QuestionExisting = false;
  private static QuestionOpener _instance;
  bool wasItRight = false;
  bool mustEndTheGame;
  public static QuestionOpener Instance
  {
      get
      {
          if (_instance == null)
          {
              _instance = GameObject.FindObjectOfType<QuestionOpener>();
          }

          return _instance;
      }
  }

public void StartNewGame(){

}
public void ResetEverything(){
  QuestionExisting = false;
  bool wasItRight = false;
}

  public void AskOneQuestion(string country){
    Debug.Log("122222");
    canvas.SetActive(true);
    QuestionExisting = true;
    QuestionHandler.Instance.MakeOneQuestion(country);
    AudioManager.Instance.PlayLoop();
  }
  public void FinishedQuestion(){
    QuestionExisting = false;
    canvas.SetActive(false);
    AudioManager.Instance.StopLoop();
  }
  public void SetShouldItContinue(bool newValue)
  {
    wasItRight = newValue;
  }
  public bool GetShouldItContinue()
  {
    return wasItRight;
  }
    // Start is called before the first frame update
    public void MakeThe3Questions()
    {
      canvas.SetActive(true);
      QuestionExisting = true;
      QuestionHandler.Instance.MakeThreeQuestions("NEPAL");
      AudioManager.Instance.PlayLoop();
    }
    public void QuestionsStopped(bool res)
    {
      MenuManager.Instance.QuizHasFinished(res);
      QuestionExisting = false;
      canvas.SetActive(false);
      AudioManager.Instance.StopLoop();
    }

}
