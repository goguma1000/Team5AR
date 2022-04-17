using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandCollider : MonoBehaviour
{
    #region Singleton
    private static HandCollider _instance;
    public static HandCollider Instance
    {
        get
        {
            return _instance;
        }

        set
        {
            _instance = value;
        }
    }
    #endregion

    private GestureInfo gesture;
    private HandInfo currentlyDetectedHand;
    private Vector3 currentPosition;
    private Vector3 defaultsize;
    private TrackingInfo tracking;
    // Start is called before the first frame update

    // Update is called once per frame
    private void Start()
    {
        gameObject.tag = "Item";
        defaultsize = transform.localScale;
    }

    void Update()
    {
        currentlyDetectedHand = ManomotionManager.Instance.Hand_infos[0].hand_info;
        gesture = currentlyDetectedHand.gesture_info;
        UserTriggerGesture(gesture, currentlyDetectedHand);
    }


    private void UserTriggerGesture(GestureInfo gesture, HandInfo currentlyDetectedHand)
    {
        if (gesture.mano_gesture_continuous == ManoGestureContinuous.HOLD_GESTURE)
        {
            tracking = currentlyDetectedHand.tracking_info;
            currentPosition = Camera.main.ViewportToWorldPoint(new Vector3(tracking.skeleton.joints[4].x, tracking.skeleton.joints[4].y, tracking.depth_estimation));
            float distance = Vector3.Distance(Camera.main.transform.position, currentPosition);

            if (distance > 0.7 && (0.7f - distance) / 5f > -defaultsize.x) transform.localScale = defaultsize + (new Vector3(0.7f - distance, 0.7f - distance, 0.7f - distance) / 5f);
            else if (0.3 < distance && distance < 0.7) transform.localScale = defaultsize + (new Vector3(0.7f - distance, 0.7f - distance, 0.7f - distance) / 2);

            transform.position = currentPosition;
        }
    }
}