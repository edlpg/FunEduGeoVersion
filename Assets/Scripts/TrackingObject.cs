using UnityEngine;
using Vuforia;
using System.Collections;
//using NUnit.Framework;

public class TrackingObject : MonoBehaviour,ITrackableEventHandler {
   private TrackableBehaviour mTrackableBehaviour;
    private static TrackingObject _instance;
    public bool isDetected;

    public static TrackingObject Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<TrackingObject>();
            }

            return _instance;
        }
    }
    void Start()
    {
      mTrackableBehaviour = GetComponent<TrackableBehaviour>();
      if (mTrackableBehaviour) {
        mTrackableBehaviour.RegisterTrackableEventHandler(this);
      }
    }

    public void OnTrackableStateChanged(
    TrackableBehaviour.Status previousStatus,
    TrackableBehaviour.Status newStatus)
  {
    if (newStatus == TrackableBehaviour.Status.DETECTED ||
        newStatus == TrackableBehaviour.Status.TRACKED ||
        newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
    {
            // Play audio when target is found
            isDetected = true;
		 TrackedObjectEvent.Instance.HasBeenTracked(false);
        }
        else
        {
            isDetected = false;
			    TrackedObjectEvent.Instance.HasBeenTracked(true);
        }
    }
    // Update is called once per frame

}
