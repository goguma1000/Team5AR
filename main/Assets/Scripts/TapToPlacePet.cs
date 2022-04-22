using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.EventSystems;

[RequireComponent(typeof(ARRaycastManager))]
public class TapToPlacePet : MonoBehaviour
{

    private ARRaycastManager raycastManager;

    [SerializeField]
    private GameObject[] objectToInstantiate;
    [SerializeField]
    private GameObject interactionUI;
    // raycast hits
    private List<ARRaycastHit> hits = new List<ARRaycastHit>();

    private Vector2 touchPosition;

    private GameObject spawnedObject;
    private bool uiOver = false;

    void Awake()
    {
        raycastManager = GetComponent<ARRaycastManager>();
    }

    // get input in this method
    private bool TryGetTouchPosition(out Vector2 touchPosition)
    {
        // touch and drag
        if (Input.touchCount > 0)
        {   if(Input.GetTouch(0).phase == TouchPhase.Began)
            {
                touchPosition = Input.GetTouch(0).position;
                return true;
            }
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
            if (!isOverUI(touchPosition))
            {
                raycastAndCreateAndUpdate();
            }
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
                interactionUI.SetActive(true);
                spawnedObject = Instantiate(objectToInstantiate[GameManager.Instance.petNum], hitPose.position, Quaternion.Euler(0, 180, 0));
            }
            else
            {
                // update position
                spawnedObject.transform.position = hitPose.position;
            }
        }
    }
    private bool isOverUI(Vector2 position)
    {   
        // if the pointer is over a gameobject, it is not over a UI element.
        if (EventSystem.current.IsPointerOverGameObject())
            return false;
        // Check if "position" over one or more UI elements.
        // EventData contains information about mouse/touch events.
        // EventSystem.current is the active event system that is handling all UI events.
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = new Vector2(position.x, position.y);
        // EventSystem's raycast methods do raycasts against UI elements (they don't have colliders
        // so we cannot use Physics.Raycast()).
        // RaycastAll() returns all hit information of UI elements that were hit.
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);
        // if hit count > 0 then we hit some UI elements
        return results.Count > 0;
    }
}
