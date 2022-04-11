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
    public Vector3 currentPosition, cusorPosition;
    private Vector3 defaultsize;
    private TrackingInfo tracking;
    private GameObject target = null;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        currentlyDetectedHand = ManomotionManager.Instance.Hand_infos[0].hand_info;
        cusorPosition = Camera.main.ViewportToWorldPoint(new Vector3(currentlyDetectedHand.tracking_info.skeleton.joints[4].x, currentlyDetectedHand.tracking_info.skeleton.joints[4].y, currentlyDetectedHand.tracking_info.depth_estimation));
        transform.position = cusorPosition;

        if (target != null)
        {
            gesture = currentlyDetectedHand.gesture_info;
            UserTriggerGesture(gesture, currentlyDetectedHand, target);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Food"))
        {
            target = other.gameObject;
            defaultsize = target.transform.localScale;

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Food"))
        {
            target = null;

        }
    }
    private void UserTriggerGesture(GestureInfo gesture, HandInfo currentlyDetectedHand, GameObject target)
    {
        if (gesture.mano_gesture_continuous == ManoGestureContinuous.HOLD_GESTURE)
        {
            tracking = currentlyDetectedHand.tracking_info;
            currentPosition = Camera.main.ViewportToWorldPoint(new Vector3(tracking.skeleton.joints[4].x, tracking.skeleton.joints[4].y, tracking.depth_estimation));
            target.transform.position = currentPosition;
            float distance = Vector3.Distance(Camera.main.transform.position, target.transform.position);

            if (distance > 0.7 && (0.7f - distance) * 10 > -defaultsize.x) target.transform.localScale = defaultsize + (new Vector3(0.7f - distance, 0.7f - distance, 0.7f - distance) * 10);
            else if (Mathf.Epsilon < distance && distance < 0.7) target.transform.localScale = defaultsize + (new Vector3(0.7f - distance, 0.7f - distance, 0.7f - distance) * 10);
        }
    }
}