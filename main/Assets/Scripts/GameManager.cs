using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;
    public int Love = 0;
    public int Cleanliness = 50;
    public int Fullness = 50;
    public int[] foodStock = { 5, 0, 0, 0, 0, 0, 0 }; // 먹이 인벤토리 순서대로
    public int[] petStomach = { 0, 0, 0, 0, 0, 0, 0 };
    public bool isFoodSpawn = false;
    public string petName = "Test Name";
   
    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   
}

