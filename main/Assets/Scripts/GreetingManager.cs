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
            swipeCount = 0;
            ChangeStatus();
            StartCoroutine("ChangeAnimation");
        }
    }

    private void GreetingOver()
    {
        //GameManager Timer initialize to check status
        GameManager.Instance.Timer = 0.0f;
        exitWindow.SetActive(true);
    }

    private void ChangeStatus()
    {
        if (GameManager.Instance.Love + 5 >= 100)
        {
            GameManager.Instance.Love = 100;
            Debug.Log("100 애정도 오름 " + GameManager.Instance.Love);
        }
        else
        {
            GameManager.Instance.Love += 5;
            Debug.Log("애정도 오름 " + GameManager.Instance.Love);
        }
        if (GameManager.Instance.Cleanliness - 10 < 0)
        {
            GameManager.Instance.Cleanliness = 0;
            Debug.Log("0 청결도 내림 " + GameManager.Instance.Cleanliness);
        }
        else
        {
            GameManager.Instance.Cleanliness -= 10;
            Debug.Log("청결도 내림 " + GameManager.Instance.Cleanliness);
        }
        if (GameManager.Instance.Fullness - 10 < 0)
        {
            GameManager.Instance.Fullness = 0;
            Debug.Log("0 배부름 내림 " + GameManager.Instance.Cleanliness);
        }
        else
        {
            GameManager.Instance.Fullness -= 10;
            Debug.Log("0 배부름 내림 " + GameManager.Instance.Cleanliness);
        }
    }

    IEnumerator ChangeAnimation()
    {
        animator.SetInteger("animation", 3);
        yield return new WaitForSecondsRealtime(2f);
        animator.SetInteger("animation", 1);
        yield return new WaitForSecondsRealtime(0.5f);
        GreetingOver();
    }

} 
