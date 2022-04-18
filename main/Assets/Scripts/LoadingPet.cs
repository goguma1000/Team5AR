using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingPet : MonoBehaviour
{
    [SerializeField]
    private GameObject[] petPrefabs;

    void OnEnable()
    {
        if(PlayerPrefs.HasKey("petNum"))
        {
            GameObject go = Instantiate(petPrefabs[PlayerPrefs.GetInt("petNum")], transform.position, Quaternion.identity);
            go.transform.parent = transform;
            go.tag = null;
        }
        else 
        {
            GameObject go = Instantiate(petPrefabs[0], transform.position, Quaternion.identity);
            go.transform.parent = transform;
            go.tag = null;
        }
    }
}
