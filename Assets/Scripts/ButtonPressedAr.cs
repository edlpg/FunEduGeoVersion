using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class ButtonPressedAr : MonoBehaviour, IVirtualButtonEventHandler
{
  public GameObject vbBtnObj;
  public int ID;
    // Start is called before the first frame update
    void Start()
    {
     vbBtnObj.GetComponent<VirtualButtonBehaviour>().RegisterEventHandler(this);
    }

    public void OnButtonPressed(VirtualButtonBehaviour vb)
    {
      Debug.Log("ID: "+ID);
     TrackedObjectEvent.Instance.ButtonHasBeenPressed(ID);
    }
    public void OnButtonReleased(VirtualButtonBehaviour vb)
    {

    }
}
