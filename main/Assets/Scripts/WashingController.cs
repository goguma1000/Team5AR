using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WashingController : MonoBehaviour
{
    public GameObject bubble;
    private GameObject pet;
    public GameObject clearUI;
    CapsuleCollider rangeCollider;

    public Slider washingGauge;
    private bool isClear;

    void Start()
    {
        clearUI.SetActive(false);
        washingGauge.value = 0;
        isClear = false;
    }

    void Update()
    {
        // Manage status
        if (washingGauge.value >= 100 && !isClear)
        {
            clearUI.SetActive(true);
            isClear = true;

            Debug.Log("Stop");

            GameManager.Instance.Cleanliness += 20;
            GameManager.Instance.Love += 5;
            GameManager.Instance.Fullness -= 10;

            clearUI.SetActive(true);
        }

        // AR
        DetectHandGestureGrab();
        DetectMouseClick();
    }

    private void FixedUpdate()
    {
        pet = GameObject.FindGameObjectWithTag("Player");
        rangeCollider = pet.GetComponent<CapsuleCollider>();
    }

    // AR version
    void DetectHandGestureGrab()
    {
        //Information of the hand
        HandInfo detectedHand = ManomotionManager.Instance.Hand_infos[0].hand_info;

        // When I perform the Grab Gesture
        if (detectedHand.gesture_info.mano_gesture_trigger == ManoGestureTrigger.GRAB_GESTURE)
        {
            if(washingGauge.value < 100)
               washingGauge.value += 10;
            RandomSpawn();
        }

    }

    // Click version
    void DetectMouseClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            washingGauge.value += 10;
            RandomSpawn();
            Debug.Log("게이지 : " + washingGauge.value);
            Debug.Log("청결 : " + GameManager.Instance.Cleanliness);
        }

    }

    // Bubble spawn
    private void RandomSpawn()
    {
        Vector3 petPos = pet.transform.position;

        float rangeX = rangeCollider.bounds.size.x;
        float rangeY = rangeCollider.bounds.size.y;
        float rangeZ = rangeCollider.bounds.size.z;

        // Spawn range
        rangeX = Random.Range((rangeX/2) * -1, rangeX/2 );
        rangeY = Random.Range((rangeY/2) * -1, rangeY/2 );
        rangeZ = Random.Range((rangeZ/2) * -1, rangeZ/2 );

        Vector3 randomPos = new Vector3(rangeX, rangeY, rangeZ);
        Vector3 spawnPos = petPos + randomPos;

        Instantiate(bubble, spawnPos, Quaternion.identity);
    }
}
