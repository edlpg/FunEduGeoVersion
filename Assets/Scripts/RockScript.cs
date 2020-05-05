using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockScript : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col)
    {
      if(col.tag == "Player")
      NepalGame.Instance.UpdateHitPoint();
    }
}
