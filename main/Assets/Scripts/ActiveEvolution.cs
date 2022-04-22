using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActiveEvolution : MonoBehaviour
{
    public GameObject evoUI;
    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        int stomachSum = 0;

        //For TapToPlace after evolution
        GameObject[] player = GameObject.FindGameObjectsWithTag("Player");

        if (player.Length > 1 && GameManager.Instance.petNum != 0)
            Destroy(player[0]);

        for (int i = 0; i < 7; i++)
        {
            stomachSum += GameManager.Instance.petStomach[i];
        }

        if (GameManager.Instance.Love >= 100 && stomachSum >= 10)
        {
            evoUI.SetActive(true);
        }

        else
            evoUI.SetActive(false);
    }
}