using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class TapToPlacePetForWalking : MonoBehaviour
{
    private ARRaycastManager raycastManager;

    // raycast hits
    private List<ARRaycastHit> hits = new List<ARRaycastHit>();

    private Vector2 touchPosition;

    public LayerMask objectLayer;

    private GameObject spawnedObject;
    private PetForWalking walking;

    void Awake()
    {
        raycastManager = GetComponent<ARRaycastManager>();
    }

    private void OnEnable()
    {
        spawnedObject = GameObject.FindGameObjectWithTag("Player");
        walking = spawnedObject.GetComponent<PetForWalking>();
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

        RaycastHit hit;

        // do raycast
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, objectLayer))
        {
            Vector3 pos = hit.point + new Vector3(0, 0.08f, 0);

            // update position
            walking.movePet(pos);
        }
    }

    // reference: https://www.youtube.com/watch?v=NdrvihZhVqs
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
