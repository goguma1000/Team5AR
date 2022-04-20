using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FoodManager : MonoBehaviour
{
    private GameObject target;
    private int foodIndex, hitcount = 0;
    private float distance;
    private AudioSource audio;
    private GestureInfo gesture;
    private HandInfo currentlyDetectedHand;

    private void Start()
    {
        audio = GetComponent<AudioSource>();
    }
    private void Update()
    {
        currentlyDetectedHand = ManomotionManager.Instance.Hand_infos[0].hand_info;
        gesture = currentlyDetectedHand.gesture_info;

        target = GameObject.FindGameObjectWithTag("Player");
        if (target != null)
        {
            distance = Mathf.Abs(Vector3.Distance(target.transform.position, transform.position));
            if(distance <= 0.7f && gesture.mano_gesture_continuous == ManoGestureContinuous.HOLD_GESTURE)
            {
                Ray ray = new Ray(transform.position, target.transform.position - transform.position);
               
                RaycastHit hit;
                if(Physics.Raycast(ray, out hit, distance))
                {
                    if (hit.collider.gameObject.CompareTag("Player"))
                    {
                        if(hitcount == 0)
                        {
                            audio.Play();
                            hitcount++;
                        }
                        StartCoroutine("eat");
                    }
                }
            }
        }
    }
    IEnumerator eat()
    {   
        target.gameObject.GetComponent<Animator>().SetInteger("animation",5);
        yield return new WaitForSeconds(audio.clip.length);
        GameManager.Instance.isFoodSpawn = false;
        swithchNameToIndex(this.name);

        GameManager.Instance.petStomach[foodIndex] += 1;

        if (GameManager.Instance.Fullness + 20 > 100)
        {
            GameManager.Instance.Fullness = 100;
        }
        else GameManager.Instance.Fullness += 20;
        
        if (GameManager.Instance.Cleanliness - 10 < 0)
        {
            GameManager.Instance.Cleanliness = 0;
        }
        else GameManager.Instance.Cleanliness -= 10;

        if (foodIndex <= 4)
        {
            GameManager.Instance.Love += 5;
        }
        else
        {
            GameManager.Instance.Love += 10;
        }
        target.gameObject.GetComponent<Animator>().SetInteger("animation", 1);
        GameObject.Find("InvantoryCanvas").SetActive(false);
        GameObject.Find("MainGUI").transform.GetChild(1).gameObject.SetActive(true);
        GameObject.Find("MainGUI").transform.GetChild(0).gameObject.SetActive(true);
        Destroy(this.gameObject);
    }
    
    private int swithchNameToIndex(string name)
    {
        switch (name)
        {
            case "Cherry":
                foodIndex = 0;
                break;
            case "Watermelon":
                foodIndex = 1;
                break;
            case "Olive":
                foodIndex = 2;
                break;
            case "Cheese":
                foodIndex = 3;
                break;
            case "Banana":
                foodIndex = 4;
                break;
            case "Hotdog":
                foodIndex = 5;
                break;
            case "Hamburger":
                foodIndex = 6;
                break;
            default:
                break;
        }
        return foodIndex;
    }
}
