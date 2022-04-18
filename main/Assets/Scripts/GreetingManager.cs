using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreetingManager : MonoBehaviour
{
    private GameObject pet;
    private Animator animator;
    private int swipeCount = 0;
    
    [SerializeField]
    private GameObject interactionUI;
    [SerializeField]
    private GameObject greeting;
    [SerializeField]
    private GameObject exitWindow;
    // Update is called once per frame
    void Update()
    {
        pet = GameObject.FindGameObjectWithTag("Player");
        animator = pet.GetComponent<Animator>();
        UseTriggerGesture();
    }
    private void OnEnable()
    {
        exitWindow.SetActive(false);
        interactionUI.SetActive(false);
    }
    private void UseTriggerGesture()
    {
        HandInfo detectedHand = ManomotionManager.Instance.Hand_infos[0].hand_info;

        if (detectedHand.gesture_info.mano_gesture_trigger == ManoGestureTrigger.SWIPE_LEFT || detectedHand.gesture_info.mano_gesture_trigger == ManoGestureTrigger.SWIPE_RIGHT)
        {
            swipeCount++;
        }
        //If player swipe hands LEFT or RIGHT 3 times, Greeting is over!
        if(swipeCount == 3)
        {
            GameManager.Instance.Love += 5;
            GameManager.Instance.Cleanliness -= 10;
            GameManager.Instance.Fullness -= 10;
            StartCoroutine("ChangeAnimation");
            swipeCount = 0;
            GreetingOver();
        }
    }

    private void GreetingOver()
    {
        exitWindow.SetActive(true);
    }

    IEnumerator ChangeAnimation()
    {
        for (float ft = 1f; ft >= 0; ft -= 0.1f)
        {
            animator.SetInteger("animation", 3);
            yield return new WaitForSecondsRealtime(0.5f);
        }
        animator.SetInteger("animation", 1);
    }

} 
