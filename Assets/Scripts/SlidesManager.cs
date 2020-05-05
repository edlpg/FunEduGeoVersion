using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlidesManager : MonoBehaviour
{
    // Start is called before the first frame update
    private static SlidesManager _instance;
    public Sprite[] nepal;
    public Sprite[] yemen;

    Sprite[] studySprite;

    public GameObject[] buttons;
    public GameObject slide;
    public bool isOnLoading = false;
    int index = 0;
    public bool openedOnGame;

    public static SlidesManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<SlidesManager>();
            }

            return _instance;
        }
    }

    public void LoadingScreen()
    {
        StartCoroutine(Loading());
    }

    IEnumerator Loading(){
      AudioManager.Instance.PlayLoading();
      slide.SetActive(true);
      studySprite = nepal;
      isOnLoading = true;
      int i = Random.Range(0, 4);
      ChangeSprite(i);
      yield return new WaitForSeconds(2.5f);
      int f = Random.Range(0,4);
      while(f==i){
        f = Random.Range(0,4);
      }
      ChangeSprite(f);
        yield return new WaitForSeconds(2.5f);
      isOnLoading = false;
      slide.SetActive(false);
    }

    public void ChangeSprite(int num){
      slide.GetComponent<Image> ().sprite = studySprite[num];
    }

    public void AppearOnRead(){
      if(index == 0){
        buttons[1].SetActive(false);
      }
      if(index == 3){
        buttons[2].SetActive(false);
      }
      if(index!=3 && index!=0 ){
        buttons[1].SetActive(true);
        buttons[2].SetActive(true);
      }

    }
    public void CloseButton(){
      AudioManager.Instance.PlayButton();
      CloseEverything();
    }
    public void MoveForward(){
      if(index<3){
        AudioManager.Instance.PlayButton();
        index++;
        AppearOnRead();
        ChangeSprite(index);
      }
    }
    public void MoveBackwards(){
      if(index>0){
        AudioManager.Instance.PlayButton();
        index--;
        AppearOnRead();
        ChangeSprite(index);
      }
    }

    public void EverythingReadyToRead(){
      if(MenuManager.Instance.countryCode == 0)
      {
        studySprite = nepal;
      }
      else
      {
          studySprite = yemen;
      }
      slide.SetActive(true);
      index = 0;
      PowerButtons();
      AppearOnRead();
      ChangeSprite(index);
    }


    public void EverythingReadyToReadOnGame(){
      if(MenuManager.Instance.countryCode == 0)
      {
        studySprite = nepal;
      }
      else
      {
          studySprite = yemen;
      }
      openedOnGame = true;
      slide.SetActive(true);
      index = 0;
      PowerButtons();
      AppearOnRead();
      ChangeSprite(index);
    }

    public void CloseEverything(){
      if(openedOnGame)
      {
        NepalGame.Instance.SlideOpener(true);
      }
      openedOnGame = false;
      index = 0;
      ShutDownButtons();
      slide.GetComponent<Image> ().sprite = null;
      slide.SetActive(false);
    }
    void PowerButtons(){
      for(int i = 0; i<buttons.Length;i++){
        buttons[i].SetActive(true);
      }
    }
    void ShutDownButtons(){
     for(int i = 0; i<buttons.Length;i++){
       buttons[i].SetActive(false);
     }
    }

}
