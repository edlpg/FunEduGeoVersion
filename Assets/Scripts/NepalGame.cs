using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NepalGame : MonoBehaviour
{
    private static NepalGame _instance;
    // Start is called before the first frame update
    public GameObject backGround;
    public GameObject playObject;
    public GameObject thisGameObject;
    public GameObject[] rocks;
    public GameObject[] positioner;
    public GameObject ReplayButton;
    public GameObject ReadButton;
    public GameObject CloseButton;
    public GameObject MoveBtn1;
    public GameObject MoveBtn2;
    public GameObject canvas;
    public GameObject extraLifeOBJ;
    public int[] speedArray;
    public float speed;
    public TextMeshProUGUI score;
    public TextMeshProUGUI Feedback;
    public TextMeshProUGUI HighestScore;
    public TextMeshProUGUI extraLife;
    float timer;
    int scoreCount;
    int pos = 1;
    bool hasAskedOnce = false;
    bool OnGameEnd = false;
    bool hasMovedItOnce = false;
    bool isAskingAQuestion = false;
    public float plusMov;

    int currentPosRock;

    public static NepalGame Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<NepalGame>();
            }

            return _instance;
        }
    }

    public void SlideOpener(bool state)
    {
      backGround.SetActive(state);
      ReplayButton.SetActive(state);
      ReadButton.SetActive(state);
      CloseButton.SetActive(state);
      if(!state){
        HighestScore.text = "";
      }else{
        string HSfeed = HST();
         HighestScore.text =HSfeed ;
      }
    }

    void SetInitPos()
    {
      int posX = Random.Range(0,3);
     currentPosRock = Random.Range(0,3);
     while(pos == currentPosRock)
     {
       currentPosRock = Random.Range(0,3);
     }

     Vector3 newVector = rocks[0].transform.localPosition;
     newVector.y = 400f;
     newVector.x = positioner[posX].transform.localPosition.x;
     rocks[0].transform.localPosition = newVector;
     Rigidbody2D rb = rocks[0].GetComponent<Rigidbody2D>();
     rb.gravityScale = 1F;

     newVector = rocks[1].transform.localPosition;
     newVector.y = 400f;
     newVector.x = positioner[currentPosRock].transform.localPosition.x;
     rocks[1].transform.localPosition = newVector;
     rb = rocks[1].GetComponent<Rigidbody2D>();
     rb.gravityScale = 1F;
    }

    void GravityChanger(float scale){
      for(int i = 0 ;i<rocks.Length;i++){
        Rigidbody2D rb = rocks[i].GetComponent<Rigidbody2D>();
        rb.gravityScale =scale;
      }
    }
    public void StartGame()
    {
     extraLifeOBJ.SetActive(false);
      ResetAllVars();
      SpeedArayInit();
      moveToPos1();
      SetInitPos();
      ActivateCast(false);
      GravityChanger(0f);
      QuestionOpener.Instance.SetShouldItContinue(false);
      SlidesManager.Instance.LoadingScreen();
    }

    public void Study()
    {
      SlideOpener(false);
      MenuManager.Instance.countryCode = 0;
      SlidesManager.Instance.EverythingReadyToReadOnGame();

    }
    public void RestartGame()
    {
        AudioMenu.Instance.StopLoop();
        StartGame();
    }

    public void CloseTheGame()
    {
        TrackedObjectEvent.Instance.MenuClosed();
        AudioMenu.Instance.StopLoop();
        thisGameObject.SetActive(false);
    }



  void ResetAllVars(){
    pos = 1;
    isAskingAQuestion = false;
     hasMovedItOnce = false;
    hasAskedOnce = false;
    OnGameEnd = false;
      scoreCount = 0;
      timer = 1f;
      speed = 0f;
      Feedback.text = "";
      HighestScore.text = "";
      score.text = "";
      backGround.SetActive(false);
      ReplayButton.SetActive(false);
      ReadButton.SetActive(false);
      CloseButton.SetActive(false);
      MoveBtn1.SetActive(false);
      MoveBtn2.SetActive(false);
     ActivateCast(false);

    }


    public void UpdateHitPoint()
    {
      if(SlidesManager.Instance.isOnLoading ||isAskingAQuestion)
      return;
      if(!hasAskedOnce)
      {
         isAskingAQuestion = true;
         hasAskedOnce = true;
         AskAQuestion();
      }
      else
      {

         EndGame();
      }
    }

    void UpdateScore(){
      score.text = "Score: "+scoreCount;
    }
   void SpeedArayInit(){
     speedArray = new int[rocks.Length];
     for(int i =0;i<speedArray.Length;i++){
       speedArray[i] = Random.Range(1,3);
     }
   }
    // Update is called once per frame
    void Update()
    {
      if(CanItContinue())
      {
          MoveTheRocks();
      }

      if (Input.GetKeyDown("a"))
        {
          ButtonPressedMove(0);
        }
        if (Input.GetKeyDown("d"))
          {
            ButtonPressedMove(1);
          }
    }

    void returnsetPosY()
    {
      for(int i =0;i<rocks.Length;i++)
      {
        Vector3 newVector = rocks[i].transform.localPosition;
        newVector.y = 400f;
        rocks[i].transform.localPosition = newVector;
      }
    }

   bool CanItContinue()
   {

    if(SlidesManager.Instance.isOnLoading || OnGameEnd)
    return false;

    if(!SlidesManager.Instance.isOnLoading && !hasMovedItOnce)
    {
      ActivateAfterLoading();
      AudioManagerNepal.Instance.PlayLoop();
      hasMovedItOnce=true;
      GravityChanger(1f);
    }

    if(QuestionOpener.Instance.QuestionExisting)
    {
      return false;
    }
    else
    {
      return true;
    }
  }
  void ActivateAfterLoading(){
    ActivateCast(true);
    backGround.SetActive(true);
    ReplayButton.SetActive(false);
    ReadButton.SetActive(false);
    CloseButton.SetActive(false);
    MoveBtn1.SetActive(true);
    MoveBtn2.SetActive(true);
    UpdateScore();
    backGround.SetActive(true);
  }
  void TakeOutText()
  {
    score.text = "";
    HighestScore.text = "";
    ReplayButton.SetActive(false);
    ReadButton.SetActive(false);
    CloseButton.SetActive(false);
    MoveBtn1.SetActive(false);
    MoveBtn2.SetActive(false);
    backGround.SetActive(false);
    ActivateCast(false);
  }
   void AskAQuestion()
   {
     AudioManagerNepal.Instance.StopLoop();
     TakeOutText();
     QuestionOpener.Instance.AskOneQuestion("NEPAL");
     StartCoroutine(WaitTillAnswer());
   }

   IEnumerator WaitTillAnswer(){
     while(QuestionOpener.Instance.QuestionExisting)
     {
       yield return null;
     }
     DecideWhatsNext();
   }
   void ActivateCast(bool stat){
     playObject.SetActive(stat);
     for(int i = 0; i<rocks.Length;i++)
     {
       rocks[i].SetActive(stat);
     }
   }
   void EndGame(){
       backGround.SetActive(true);
     extraLifeOBJ.SetActive(false);
     AudioManagerNepal.Instance.StopLoop();
     AudioMenu.Instance.PlayLoop();
    ActivateCast(false);
     ReplayButton.SetActive(true);
     ReadButton.SetActive(true);
     CloseButton.SetActive(true);
     MoveBtn1.SetActive(false);
     MoveBtn2.SetActive(false);
     OnGameEnd = true;
     score.text = "";
    string HSfeed = HST();
     HighestScore.text =HSfeed ;
   }

   string HST(){
    string preHS = XmlReader.Instance.GetHigerScore("NEPAL");
    int hs;
    int.TryParse(preHS, out hs);
    if(scoreCount>hs)
    {
      XmlReader.Instance.SetNewHighScore("NEPAL",scoreCount);
      return "New Highest Score\n "+scoreCount;
    }
    else
    {
      return "Highest score: "+hs+"\n Your Score: "+scoreCount;
    }
   }
   IEnumerator ExtraLife()
   {
     extraLifeOBJ.SetActive(true);
     AudioManagerNepal.Instance.PlayResurection();
     yield return new WaitForSeconds(1.5f);
     extraLifeOBJ.SetActive(false);
   }
   void ContinueGame(){
     SpeedArayInit();
     ReplayButton.SetActive(false);
     ReadButton.SetActive(false);
     CloseButton.SetActive(false);
     MoveBtn1.SetActive(true);
     MoveBtn2.SetActive(true);
     ActivateCast(true);
     StartCoroutine(ExtraLife());
     moveToPos1();
     returnsetPosY();
     AudioManagerNepal.Instance.PlayLoop();
     backGround.SetActive(true);
   }

   void DecideWhatsNext(){
     isAskingAQuestion = false;
     Debug.Log("Continue: "+QuestionOpener.Instance.GetShouldItContinue());
     if(QuestionOpener.Instance.GetShouldItContinue()){
        ContinueGame();
     }
     else{
         EndGame();
     }
   }

    void MoveTheRocks()
    {
      for(int i = 0; i<rocks.Length;i++)
      {
        if(rocks[i].transform.localPosition.y<-240f)
        {

        int pos =  Random.Range(0,3);
         while(pos == currentPosRock)
         {
           pos = Random.Range(0,3);
         }

         Vector3 newVector = rocks[i].transform.localPosition;
         newVector.y = 400f;
         newVector.x = positioner[pos].transform.localPosition.x;
         rocks[i].transform.localPosition = newVector;
         Rigidbody2D rb = rocks[i].GetComponent<Rigidbody2D>();
         rb.gravityScale+=Random.Range(.01f,.03f);
         currentPosRock = pos;

         }
       }

      timer += Time.deltaTime;
      if(timer>1f)
      {
        timer = 0f;
        speed+=.1f;
        scoreCount ++;
        UpdateScore();
      }
    }


    /*void RestartGame(){
      QuestionOpener.Instance.ResetEverything();
      ResetAllVars();
    }*/

    void moveToPos1()
    {
      pos = 1;
    playObject.transform.localPosition = new Vector3(positioner[1].transform.localPosition.x,playObject.transform.localPosition.y,playObject.transform.localPosition.z);
    }

    public void ButtonPressedMove(int b)
    {
  if(SlidesManager.Instance.isOnLoading)
  return;

      if(b == 0)
      {
        if(pos>0)
        {
          AudioManagerNepal.Instance.PlayJump();
          pos--;
          playObject.transform.localPosition = new Vector3(positioner[pos].transform.localPosition.x,playObject.transform.localPosition.y,playObject.transform.localPosition.z);
        }
      }
      if(b == 1)
      {
        if(pos<2)
        {
          AudioManagerNepal.Instance.PlayJump();
          pos++;
          playObject.transform.localPosition = new Vector3(positioner[pos].transform.localPosition.x,playObject.transform.localPosition.y,playObject.transform.localPosition.z);
        }
      }
    }

}
