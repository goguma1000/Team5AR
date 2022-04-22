using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EvolutionSystem : MonoBehaviour
{
    [SerializeField]
    private GameObject ArSection;
    private GameObject spawnedObject;
    private GameObject pet;
    private Animator animator;

    public GameObject effect_1;
    public GameObject effect_2;
    public GameObject effect_3;
    BoxCollider rangeCollider;

    [SerializeField]
    private GameObject[] objectToInstantiate;

    public GameObject evoUI;

    private float spinSpeed = 3.0f;
    bool isSelect = false;
    int isEvo = 0;
    int count = 0;
    Vector3 petPos;

    // Start is called before the first frame update

    private void OnEnable()
    {
        pet = GameObject.FindGameObjectWithTag("Player");

        Debug.Log("start");
        ArSection.GetComponent<PetLookAtCamera>().enabled = false;

        isEvo = 0;
        count = 0;
        isSelect = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (isEvo == 0)
        {
            evoUI.SetActive(false);
            StartCoroutine("EvolutionAnimation");
        }
        else if (isEvo == 1)
        {
            ArSection.GetComponent<PetLookAtCamera>().enabled = true;

            InvokeRepeating("RandomSpawn", 0f, 0.2f);
            animator.SetInteger("animation", 3);

            isEvo = 2;

            //Status Reset
            GameManager.Instance.Love = 0;
            GameManager.Instance.Cleanliness = 50;
            GameManager.Instance.Fullness = 50;
        }

        if (count >= 10) 
        { 
            CancelInvoke("RandomSpawn");
            animator.SetInteger("animation", 1);
            this.gameObject.SetActive(false);
        }
    }

    IEnumerator EvolutionAnimation()
    {
        //Rotation Animation
        for (int i = 0; i < 3; i++)
        {
            pet.transform.Rotate(0, spinSpeed * 100 * Time.deltaTime * i, 0);
            yield return new WaitForSecondsRealtime(0.6f);
        }
        petPos = pet.transform.position;

        if(!isSelect)
            SelectType();

        isEvo = 1;
    }



    void SelectType()
    {
        int stomachSum = 0;

        for (int i = 0; i < 7; i++)
        {
            stomachSum += GameManager.Instance.petStomach[i];
        }

        int type = Random.Range(0, 5);
        Debug.Log("type : " + type);

        if (GameManager.Instance.petStomach[5] == stomachSum)
        {
            spawnedObject = Instantiate(objectToInstantiate[6], petPos, Quaternion.Euler(0, 180, 0));
            GameManager.Instance.petNum = 6;
        }
        else if (GameManager.Instance.petStomach[6] == stomachSum)
        { 
            spawnedObject = Instantiate(objectToInstantiate[7], petPos, Quaternion.Euler(0, 180, 0));
            GameManager.Instance.petNum = 7;
        }
        else
        {
            switch (type)
            {
                case 0:
                    spawnedObject = Instantiate(objectToInstantiate[1], petPos, Quaternion.Euler(0, 180, 0));
                    GameManager.Instance.petNum = 1;
                    break;
                case 1:
                    spawnedObject = Instantiate(objectToInstantiate[2], petPos, Quaternion.Euler(0, 180, 0));
                    GameManager.Instance.petNum = 2;
                    break;
                case 2:
                    spawnedObject = Instantiate(objectToInstantiate[3], petPos, Quaternion.Euler(0, 180, 0));
                    GameManager.Instance.petNum = 3;
                    break;
                case 3:
                    spawnedObject = Instantiate(objectToInstantiate[4], petPos, Quaternion.Euler(0, 180, 0));
                    GameManager.Instance.petNum = 4;
                    break;
                case 4:
                    spawnedObject = Instantiate(objectToInstantiate[5], petPos, Quaternion.Euler(0, 180, 0));
                    GameManager.Instance.petNum = 5;
                    break;
            }
        }
        isSelect = true;
        rangeCollider = spawnedObject.GetComponent<BoxCollider>();
        animator = spawnedObject.GetComponent<Animator>();
    }

    private void RandomSpawn()
    {  
        float rangeX = rangeCollider.bounds.size.x;
        float rangeY = rangeCollider.bounds.size.y;
        float rangeZ = rangeCollider.bounds.size.z;

        // Spawn range
        rangeX = Random.Range(rangeX *2 * -1, rangeX * 2);
        rangeY = Random.Range(rangeY *2 * -1, rangeY * 2);
        rangeZ = Random.Range(rangeZ *2 * -1, rangeZ * 2);

        Vector3 randomPos = new Vector3(rangeX, rangeY, rangeZ);
        Vector3 spawnPos = petPos + randomPos;

        int type = Random.Range(0, 3);

        switch (type)
        {
            case 0:
                Instantiate(effect_1, spawnPos, Quaternion.identity);
                break;
            case 1:
                Instantiate(effect_2, spawnPos, Quaternion.identity);
                break;
            case 2:
                Instantiate(effect_3, spawnPos, Quaternion.identity);
                break;
        }

        count++;
    }

}
