using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateFood : MonoBehaviour
{
    public GameObject[] food;
    private GameObject pet;


    private List<GameObject> foodList;

    public void GameStart()
    {
        foodList = new List<GameObject>();
        pet = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine("createFood");
    }

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

    public void walkingEnd()
    {
        StopCoroutine("createFood");
        destroyFood();
    }

    private void destroyFood()
    {
        foreach(GameObject go in foodList)
        {
            Destroy(go);
        }
        foodList.Clear();
    }
}
