using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertBox : MonoBehaviour
{
    public void CloseButton(){
    MenuManager.Instance.CloseAlertBox();
    }
    public void AnswerButton(){
      MenuManager.Instance.MakeThe3Questions();
    }

}
