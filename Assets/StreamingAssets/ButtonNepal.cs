using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonNepal : MonoBehaviour
{
  public int buttonId;


    public void OnPressBtn()
    {
      NepalGame.Instance.ButtonPressedMove(buttonId);
    }
}
