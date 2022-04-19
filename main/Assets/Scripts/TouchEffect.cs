using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchEffect : MonoBehaviour
{
    public GameObject touchEffect;

    float spawnTime;
    public float defaultTime = 0.05f;

    Vector2 touchPos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0 && spawnTime >= defaultTime)
        {
            EffectSpawn();
            spawnTime = 0;
        }

        spawnTime += Time.deltaTime;
    }

    void EffectSpawn()
    {
        touchPos = Input.GetTouch(0).position;
        Vector3 pos = new Vector3(touchPos.x, touchPos.y, 1);
        pos = Camera.main.ScreenToWorldPoint(pos);
        Debug.Log("À§Ä¡ " + pos);
        Instantiate(touchEffect, pos, Quaternion.identity);
    }
}
