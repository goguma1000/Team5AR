using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WashingController : MonoBehaviour
{
    public GameObject bubble;
    private GameObject pet;
    private Animator normalAnimator;
    public GameObject clearUI;
    public AudioSource bubbleSound;
    BoxCollider rangeCollider;
    public AudioSource clearSound;

    public Slider washingGauge;
    public bool isClear;

    private void OnEnable()
    {
        clearUI.SetActive(false);
        washingGauge.value = 0;
        isClear = false;
    }

    private void OnDisable()
    {
        GameManager.Instance.Timer = 0.0f;
        normalAnimator.SetInteger("animation", 1);
    }

    void Update()
    {
        pet = GameObject.FindGameObjectWithTag("Player");
        normalAnimator = pet.GetComponent<Animator>();

        // Manage status
        if (washingGauge.value >= 100 && !isClear)
        {
            clearUI.SetActive(true);
            isClear = true;

            Debug.Log("Stop");

            GameManager.Instance.Cleanliness += 20;
            GameManager.Instance.Love += 5;
            GameManager.Instance.Fullness -= 10;

            clearSound.Play();
        }

        if (!isClear)
        {
            // AR
            DetectHandGestureGrab();
          //  DetectMouseClick();
        }
    }

    private void FixedUpdate()
    {
  
        rangeCollider = pet.GetComponent<BoxCollider>();
    }

    // AR version
    void DetectHandGestureGrab()
    {
        //Information of the hand
        HandInfo detectedHand = ManomotionManager.Instance.Hand_infos[0].hand_info;
        // When I perform the Grab Gesture
        if (detectedHand.gesture_info.mano_gesture_trigger == ManoGestureTrigger.GRAB_GESTURE)
        {
            // Washing animation
            StartCoroutine("WashingAnimation");

            if(washingGauge.value < 100)
               washingGauge.value += 20;

            RandomSpawn();
        }

    }

    /*
    // Click version
    void DetectMouseClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("washing!!");
            // Washing animation
            StartCoroutine("WashingAnimation");
            
            washingGauge.value += 20;
            RandomSpawn();
            Debug.Log("게이지 : " + washingGauge.value);
            Debug.Log("청결 : " + GameManager.Instance.Cleanliness);
            // Set Idle animation
            //normalAnimator.SetInteger("animation", 1);
        }

    }*/

    // Spawn bubble particle
    private void RandomSpawn()
    {
        bubbleSound.Play();
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

    IEnumerator WashingAnimation()
    {
        normalAnimator.SetInteger("animation", 7);
        yield return new WaitForSecondsRealtime(0.5f);
      
        normalAnimator.SetInteger("animation", 1);
    }
}
