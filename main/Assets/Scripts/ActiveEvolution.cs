using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActiveEvolution : MonoBehaviour
{
    public GameObject evoUI;
    private bool isevo = false;
    // Start is called before the first frame update
    void Start()
    {
        evoUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        int stomachSum = 0;

        for (int i = 0; i < 7; i++)
        {
            stomachSum += GameManager.Instance.petStomach[i];
        }

        if (GameManager.Instance.Love >= 100 && stomachSum >= 10 && GameManager.Instance.petNum == 0)
        {
            evoUI.SetActive(true);
        }

        else
            evoUI.SetActive(false);
    }
}