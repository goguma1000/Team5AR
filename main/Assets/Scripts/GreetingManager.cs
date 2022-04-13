using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreetingManager : MonoBehaviour
{
    private GameObject pet;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        pet = GameObject.FindGameObjectWithTag("Player");
        animator = pet.GetComponent<Animator>();
        UseTriggerGesture();
    }
    private void UseTriggerGesture()
    {
        HandInfo detectedHand = ManomotionManager.Instance.Hand_infos[0].hand_info;

        if (detectedHand.gesture_info.mano_gesture_trigger == ManoGestureTrigger.SWIPE_LEFT || detectedHand.gesture_info.mano_gesture_trigger == ManoGestureTrigger.SWIPE_RIGHT)
        {

            StartCoroutine("ChangeAnimation");
        }
        //animator.SetInteger("animation", 1);
    }

    IEnumerator ChangeAnimation()
    {
        GameManager.Instance.Love += 5;
        GameManager.Instance.Cleanliness -= 10;
        GameManager.Instance.Fullness -= 10;
        for (float ft = 1f; ft >= 0; ft -= 0.1f)
        {
            animator.SetInteger("animation", 3);
            yield return new WaitForSecondsRealtime(0.5f);
        }
        animator.SetInteger("animation", 1);
    }

} 
