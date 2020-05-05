using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
  private static AudioManager _instance;
  public AudioSource audioSource;
  public AudioClip Right;
  public AudioClip Wrong;
  public AudioClip MenuLoop;
  public AudioClip timeUp;
  public AudioClip button;
  public AudioClip loading;

  public static AudioManager Instance
  {
      get
      {
          if (_instance == null)
          {
              _instance = GameObject.FindObjectOfType<AudioManager>();
          }

          return _instance;
      }
  }
  public void PlayMenu(){

  }
  public void PlayLoading()
  {
    audioSource.PlayOneShot(loading,.3f);
  }
  public void PlayButton()
  {
    audioSource.PlayOneShot(button,1f);
  }
  public void PlayRight()
  {
   audioSource.PlayOneShot(Right,1f);
  }
  public void PlayWrong()
  {
    audioSource.PlayOneShot(Wrong,1f);
  }
  public void TimesUp()
  {
    audioSource.PlayOneShot(timeUp,1f);
  }
  public void PlayLoop()
  {
       audioSource.Play();
  }
  public void StopLoop()
  {
     audioSource.Stop();
  }
}
