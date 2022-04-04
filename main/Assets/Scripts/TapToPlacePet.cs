using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycastManager))]
public class TapToPlacePet : MonoBehaviour
{

    private ARRaycastManager raycastManager;

    [SerializeField]
    private GameObject objectToInstantiate;

    // raycast hits
    private List<ARRaycastHit> hits = new List<ARRaycastHit>();

    private Vector2 touchPosition;

    private GameObject spawnedObject;

    void Awake()
    {
        raycastManager = GetComponent<ARRaycastManager>();
    }

    // get input in this method
    private bool TryGetTouchPosition(out Vector2 touchPosition)
    {
        // touch and drag
        if (Input.touchCount > 0)
        {
            touchPosition = Input.GetTouch(0).position;
            return true;
        }
        touchPosition = default;
        return false;

    }

    // Update is called once per frame
    void Update()
    {
        // check input
        if (TryGetTouchPosition(out touchPosition))
        {
            raycastAndCreateAndUpdate();
        }
    }

    // instantiate new gameobject based on raycast
    private void raycastAndCreateAndUpdate()
    {
        Ray ray = Camera.main.ScreenPointToRay(touchPosition);

        // do raycast
        if (raycastManager.Raycast(ray, hits, TrackableType.PlaneWithinPolygon))
        {
            // get position and rotation of the first plane hit and instantiate a gameobject
            Pose hitPose = hits[0].pose;

            // instantiate only one time
            if (spawnedObject == null)
            {
                spawnedObject = Instantiate(objectToInstantiate, hitPose.position, Quaternion.Euler(0, 180, 0));
            }
            else
            {
                // update position
                spawnedObject.transform.position = hitPose.position;
            }


        }
    }
}
