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
    public int petNum = 0;
    public bool isFoodSpawn = false;
    public float Timer = 0.0f;
    [HideInInspector]
    public string petName = null;
    private bool ispetSpwaned = false;
    [SerializeField]
    private float eventTimer = 0.0f;
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
        if (ispetSpwaned == false)
        {
            if (GameObject.FindGameObjectWithTag("Player"))
            {
                ispetSpwaned = true;
            }
        }
        else
        {
            Timer += Time.deltaTime;

            if (Timer > 180)
            {
                if (Cleanliness - 5 < 0)
                {
                    Cleanliness = 0;
                }
                else Cleanliness -= 5;

                if (Fullness - 5 < 0)
                {
                    Fullness = 0;
                }
                else Fullness -= 5;

                Timer = 0.0f;
            }

            if ((Cleanliness <= 20 || Fullness <= 20) && Love > 0)
            {
                eventTimer += Time.deltaTime;
                if (eventTimer > 60)
                {
                    if (Love - 5 < 0) Love = 0;
                    else Love -= 5;
                    eventTimer = 0.0f;
                }
            }
            else eventTimer = 0.0f;
        }
    }
    
}

