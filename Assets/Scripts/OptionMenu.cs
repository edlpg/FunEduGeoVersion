using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionMenu : MonoBehaviour
{
  public GameObject thisObject;
  public GameObject Scan;
  public GameObject nepalGame;

   public void PlayButton()
   {
     if(MenuManager.Instance.countryCode == 0)
     {
       nepalGame.SetActive(true);
       NepalGame.Instance.StartGame();
       thisObject.SetActive(false);
     } 
   }

   public void CloseButton()
   {
     AudioManager.Instance.PlayButton();
     TrackedObjectEvent.Instance.MenuClosed();
     thisObject.SetActive(false);
   }

   public void StudyButton()
   {
     AudioManager.Instance.PlayButton();
     SlidesManager.Instance.EverythingReadyToRead();
   }
}
