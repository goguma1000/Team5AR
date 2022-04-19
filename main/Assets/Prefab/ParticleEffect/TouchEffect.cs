using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchEffect : MonoBehaviour
{
    public GameObject touchEffect;

    float spawnTime;
    public float defaultTime = 0.05f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) && spawnTime >= defaultTime)
        {
            EffectSpawn();
            spawnTime = 0;
        }

        spawnTime += Time.deltaTime;
    }

    void EffectSpawn()
    {
        Vector3 touchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        touchPos.z = 2;
        Instantiate(touchEffect, touchPos, Quaternion.identity);
    }
}
