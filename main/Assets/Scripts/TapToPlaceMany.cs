using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycastManager))]
public class TapToPlaceMany : MonoBehaviour
{

    private ARRaycastManager raycastManager;

    // this is for demo only, not really used for the features.
    private ARPlaneManager planeManager;

    [SerializeField]
    private GameObject objectToInstantiate;

    // raycast hits
    private List<ARRaycastHit> hits = new List<ARRaycastHit>();

    private Vector2 touchPosition;

    void Awake()
    {
        raycastManager = GetComponent<ARRaycastManager>();
        planeManager = GetComponent<ARPlaneManager>();
    }

    // subscribe
    private void OnEnable()
    {
        planeManager.planesChanged += planesChanged;
    }


    // unsubscribe
    private void OnDisable()
    {
        planeManager.planesChanged -= planesChanged;
    }
    private void planesChanged(ARPlanesChangedEventArgs args)
    {
        // execute here any code that you want to execute when planes have been detected or changed
        foreach (ARPlane plane in args.added)
        {
            Debug.Log("Plane added: " + plane.transform.position);
        }
    }


    // get input in this method
    private bool TryGetTouchPosition(out Vector2 touchPosition)
    {
        // get only one touch (holding down the finger will be ignored)
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
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
            raycastAndCreate();
        }
    }

    // instantiate new gameobject based on raycast
    private void raycastAndCreate()
    {
        Ray ray = Camera.main.ScreenPointToRay(touchPosition);

        // do raycast
        if (raycastManager.Raycast(ray, hits, TrackableType.PlaneWithinPolygon))
        {
            // get position and rotation of the first plane hit and instantiate a gameobject
            Pose hitPose = hits[0].pose;
            Instantiate(objectToInstantiate, hitPose.position, hitPose.rotation);

        }
    }
}
