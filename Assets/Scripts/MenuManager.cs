using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
   private static MenuManager _instance;
   public GameObject menu;
   public GameObject AlertBox;
   public GameObject unfortunately;
   public GameObject unlockedObj;
   public TextMeshProUGUI country;
   public GameObject flag;
   public Sprite[] countries;
   public string[] countryName;
   public int countryCode;
   public bool SomeMenuOpened = false;

   public static MenuManager Instance
   {
       get
       {
           if (_instance == null)
           {
               _instance = GameObject.FindObjectOfType<MenuManager>();
           }

           return _instance;
       }
   }

   public void OpenMenu(int code)
   {

     Debug.Log("LLEGA AQUI: "+code + " Y SomeMenuOpened : "+SomeMenuOpened);
if(SomeMenuOpened){
  return;
}
  TrackedObjectEvent.Instance.ScanSignStats(false);
     if(ShallItGoOn(code))
     {
       SomeMenuOpened = true;
       countryCode = code;
       menu.SetActive(true);
       country.text = countryName[code];
       flag.GetComponent<Image> ().sprite = countries[code];
     }else
     {
           SomeMenuOpened = true;
           AlertBox.SetActive(true);
     }
   }

   public bool ShallItGoOn(int code)
   {
      List<string> list = XmlReader.Instance.GetUnlockedCountries();
      if(list.Count>code){
       return true;
      }

      return false;
   }
public void ClosedMenu()
{
  SomeMenuOpened = false;
}
   public void CloseAlertBox()
  {
    SomeMenuOpened = false;
    TrackedObjectEvent.Instance.MenuOpened = false;
     AlertBox.SetActive(false);
     unfortunately.SetActive(false);
     TrackedObjectEvent.Instance.MenuClosed();

  }
  public void MakeThe3Questions()
  {
       SomeMenuOpened = false;
       AlertBox.SetActive(false);
       unfortunately.SetActive(false);
       QuestionOpener.Instance.MakeThe3Questions();
  }

  public void QuizHasFinished(bool pass)
  {
    if(pass)
    {
     StartCoroutine(Unlocked());
    }else{
     unfortunately.SetActive(true);
    }
  }

 IEnumerator Unlocked()
 {
   XmlReader.Instance.UnlockANewCountry("YEMEN");
   AudioMenu.Instance.PlayCelebration();
   unlockedObj.SetActive(true);
   yield return new WaitForSeconds(1.2f);
   unlockedObj.SetActive(false);
   OpenMenu(1);
 }

}
