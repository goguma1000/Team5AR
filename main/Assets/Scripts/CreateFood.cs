using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateFood : MonoBehaviour
{
    // Script for Walking

    public GameObject[] food;
    private GameObject pet;


    private List<GameObject> foodList;

    public void GameStart()
    {
        foodList = new List<GameObject>();
        pet = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine("createFood");
    }

    // Create food for walking
    IEnumerator createFood()
    {
        while (true)
        {
            if (foodList.Count < 50)
            {
                for (int i = 0; i < 5; i++)
                {
                    float randomX = pet.transform.position.x + Random.Range(-2f, 2f);
                    float randomZ = pet.transform.position.z + Random.Range(-2f, 2f);


                    Vector3 pos = new Vector3(randomX, pet.transform.position.y + 0.1f, randomZ);
                    foodList.Add(Instantiate(food[WalkingManager.ranFood], pos, Quaternion.identity));
                }
            }

            yield return new WaitForSecondsRealtime(3f);
        }
    }

    // when walking is end, stop create food coroutine
    public void walkingEnd()
    {
        StopCoroutine("createFood");
        destroyFood();
    }

    // When walking is end, destroy all food object.
    private void destroyFood()
    {
        foreach(GameObject go in foodList)
        {
            Destroy(go);
        }
        foodList.Clear();
    }
}
