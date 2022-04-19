using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Notice_UI : MonoBehaviour
{
    public GameObject subbox;
    public Text subintext;
    public Animator subani;

    private WaitForSeconds popDelay = new WaitForSeconds(1.0f);
    private WaitForSeconds fadeDelay = new WaitForSeconds(0.3f);
    
    void Start()
    {
        subbox.SetActive(false);   
    }

    public void SUB(string message)
    {
        subintext.text = message;
        subbox.SetActive(false);
        StopAllCoroutines();
        StartCoroutine(SUBDelay());

    }

    IEnumerator SUBDelay()
    {
        subbox.SetActive(true);
        subani.SetBool("isOn", true);
        yield return popDelay;
        subani.SetBool("isOn", false);
        yield return fadeDelay;
        subbox.SetActive(false);
    }

    
}
