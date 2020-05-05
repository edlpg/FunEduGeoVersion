using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackedObjectEvent : MonoBehaviour
{
    // Start is called before the first frame update
    private static TrackedObjectEvent _instance;
    public GameObject scanSign;
    public bool MenuOpened;

    public static TrackedObjectEvent Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<TrackedObjectEvent>();
            }

            return _instance;
        }
    }
    public void HasBeenTracked(bool e)
    {
        Debug.Log("Entra y es: " + e+"     "+MenuOpened);
      if(e == true &&  MenuOpened)
      return;


       scanSign.SetActive(e);
    }
    public void ScanSignStats(bool status){
      scanSign.SetActive(status);
    }
    public void MenuClosed()
    {
        MenuOpened = false;
        MenuManager.Instance.ClosedMenu();

        if (!TrackingObject.Instance.isDetected)
        {
            scanSign.SetActive(true);
        }
         
    }

    public void ButtonHasBeenPressed(int i)
    {
       MenuOpened = true;
       MenuManager.Instance.OpenMenu(i);
    }
}
