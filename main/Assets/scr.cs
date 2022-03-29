using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr : MonoBehaviour
{
    // Start is called before the first frame update
  
    void Update()
    {
        transform.Rotate(new Vector3(20*Time.deltaTime, 30*Time.deltaTime, 40* Time.deltaTime));
    }
}
