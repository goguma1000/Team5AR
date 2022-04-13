using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetLookAtCamera : MonoBehaviour
{
    private GameObject pet;
    private Camera camera;
    private Vector3 targetPosition;

    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindGameObjectWithTag("Player"))
        {
            pet = GameObject.FindGameObjectWithTag("Player");
        }
        camera = Camera.main;
        targetPosition = new Vector3(camera.transform.position.x, pet.transform.position.y, camera.transform.position.z);
        pet.transform.LookAt(targetPosition);
    }
}
