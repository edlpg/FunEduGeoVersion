using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagerNepal : MonoBehaviour
{
    // Start is called before the first frame update
    private static AudioManagerNepal _instance;
    public AudioSource audioSource;
    public AudioClip nepalJump;
    public AudioClip nepalLoop;
    public AudioClip newScore;
    public AudioClip resurection;
    public AudioClip youDied;

    public static AudioManagerNepal Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<AudioManagerNepal>();
            }

            return _instance;
        }
    }

public void PlayResurection()
{
audioSource.PlayOneShot(resurection,1f);
}
   public void PlayJump(){
     audioSource.PlayOneShot(nepalJump,1f);
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
