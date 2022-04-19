using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateFood : MonoBehaviour
{
    // Script for Walking

    public GameObject[] food;

    private GameObject pet;
    private GameObject Plane;


    private List<GameObject> foodList;

    public void GameStart(GameObject plane)
    {
        foodList = new List<GameObject>();
        pet = GameObject.FindGameObjectWithTag("Player");
        Plane = plane;
        StartCoroutine("createFood");
    }

    // Create food for walking
    IEnumerator createFood()
    {
        MeshCollider planeColl =  Plane.GetComponent<MeshCollider>();

        while (true)
        {
            if (foodList.Count < 50)
            {
                for (int i = 0; i < 5; i++)
                {
                    float randomX = pet.transform.position.x + Random.Range(-2f, 2f);
                    float randomZ = pet.transform.position.z + Random.Range(-2f, 2f);

                    float rangeX_min = Plane.transform.position.x - (planeColl.bounds.size.x / 2);
                    float rangeX_max = Plane.transform.position.x + (planeColl.bounds.size.x / 2);

                    float rangeZ_min = Plane.transform.position.z - (planeColl.bounds.size.z / 2);
                    float rangeZ_max = Plane.transform.position.z + (planeColl.bounds.size.z / 2);

                    if ((randomX < rangeX_max && randomX > rangeX_min) &&
                        (randomZ < rangeZ_max && randomZ > rangeZ_min))
                    {
                        Vector3 pos = new Vector3(randomX, pet.transform.position.y + 0.1f, randomZ);
                        foodList.Add(Instantiate(food[WalkingManager.ranFood], pos, Quaternion.identity));
                    }
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
        if (foodList != null)
        {
            foreach (GameObject go in foodList)
            {
                Destroy(go);
            }
            foodList.Clear();
        }
    }
}
