using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WashingController : MonoBehaviour
{
    public GameObject bubble;
    public GameObject pet;
    SphereCollider rangeCollider;

    public Slider washingGauge;
    // Start is called before the first frame update
    void Start()
    {
        rangeCollider = pet.GetComponent<SphereCollider>();
      //  washingGauge = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        DetectHandGestureGrab();
        DetectMouseClick();
        if (washingGauge.value == 100)
        {
            washingGauge.value = 0;
        }
    }

    void DetectHandGestureGrab()
    {
        //Information of the hand
        HandInfo detectedHand = ManomotionManager.Instance.Hand_infos[0].hand_info;

        // When I perform the Grab Gesture
        if (detectedHand.gesture_info.mano_gesture_trigger == ManoGestureTrigger.GRAB_GESTURE)
        {
            washingGauge.value += 10;
            RandomSpawn();
        }

    }

    void DetectMouseClick()
    {
        // When I perform the Grab Gesture
        if (Input.GetMouseButtonDown(0))
        {
            washingGauge.value += 10;
            RandomSpawn();
        }

    }

    private void RandomSpawn()
    {
        Vector3 petPos = pet.transform.position;

        float rangeX = rangeCollider.bounds.size.x;
        float rangeY = rangeCollider.bounds.size.y;
        float rangeZ = rangeCollider.bounds.size.z;

        rangeX = Random.Range((rangeX / 2) * -1, rangeX / 2);
        rangeY = Random.Range((rangeY / 2) * -1, rangeY / 2);
        rangeZ = Random.Range((rangeZ / 2) * -1, rangeZ / 2);

        Vector3 randomPos = new Vector3(rangeX, rangeY, rangeZ);
        Vector3 spawnPos = petPos + randomPos;

        Instantiate(bubble, spawnPos, Quaternion.identity);
    }
}
