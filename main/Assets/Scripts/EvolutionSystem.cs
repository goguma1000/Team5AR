using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EvolutionSystem : MonoBehaviour
{
    public GameObject evoUI;
    // Start is called before the first frame update
    void Start()
    {
        evoUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //먹이 준 횟수 조건에 추가하기
        if (GameManager.Instance.Love >= 100)
        {
            evoUI.SetActive(true);
        }

        else
            evoUI.SetActive(false);
    }
}
