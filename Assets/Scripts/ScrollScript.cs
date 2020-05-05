using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollScript : MonoBehaviour
{
float speed = .05f;
public GameObject thisPos;
public float initPos;
public float finalPos;
public GameObject[] backImage;
    // Update is called once per frame
    void Start()
    {
      thisPos.transform.localPosition = new Vector3(thisPos.transform.localPosition.x,thisPos.transform.localPosition.y,100f);
    }
    void Update()
    {
        MoveBackGround();

    }

    void MoveBackGround()
    {
      for(int i = 0;i<backImage.Length;i++)
      {

        backImage[i].transform.localPosition = new Vector2(backImage[i].transform.localPosition.x,backImage[i].transform.localPosition.y -(Time.time * speed));
        if(backImage[i].transform.localPosition.y<finalPos)
        {
           backImage[i].transform.localPosition = new Vector2(backImage[i].transform.localPosition.x,initPos);
        }
      }
    }
}
