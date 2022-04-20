using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

public class WalkingManager : MonoBehaviour
{
    // Food num pet got
    public static int food = 0;
    // What food get
    public static int ranFood = 0;

    // Food Sprites for UI
    public Sprite[] foodSprites;
    public Image foodImage;
    public TMPro.TMP_Text foodScore;

    // End Walking UI
    [SerializeField]
    private GameObject clearWindow;

    // Create Food and plane
    private CreateFood createFood;
    [SerializeField]
    private GameObject planePrefab;
    private GameObject plane;

    // Turn on or off UI and Component
    [SerializeField]
    private GameObject InteractionUI;
    [SerializeField]
    private GameObject arSO;

    private GameObject pet;

    private const int normalProbConst = 80;
    private const int specialProbConst = 20;

    private const int normalFoodNum = 5;
    private const int specialFoodNum = 2;

    void Awake()
    {
        createFood = GetComponent<CreateFood>();
    }

    // Start is called before the first frame update
    void OnEnable()
    {
        pet = GameObject.FindGameObjectWithTag("Player");
        GameStart();
    }

    // Update is called once per frame
    void Update()
    {
        if(food >= 10)
        {
            createFood.walkingEnd();
            clearWindow.SetActive(true);
        }

        foodScore.text = "X" + food;
    }

    public void GameStart()
    {
        pet.GetComponent<PetForWalking>().enabled = true;
        arSO.GetComponent<TapToPlacePet>().enabled = false;
        foreach (var go in arSO.GetComponent<ARPlaneManager>().trackables)
        {
            go.gameObject.SetActive(false);
        }
        arSO.GetComponent<ARPlaneManager>().enabled = false;
        arSO.GetComponent<TapToPlacePetForWalking>().enabled = true;
        InteractionUI.SetActive(false);

        ranFood = RandomFood();
        foodImage.sprite = foodSprites[ranFood];

        plane = Instantiate(planePrefab, pet.transform.position - new Vector3(0, 0.145f, 0), Quaternion.identity);
        food = 0;
        foodScore.text = "X" + food;

        createFood.GameStart(plane);
    }

    public void GameClear()
    {
        GameManager.Instance.foodStock[ranFood]++;
        GameManager.Instance.Cleanliness -= 20;
        GameManager.Instance.Fullness -= 20;
        GameManager.Instance.Love += 10;

        if(GameManager.Instance.Cleanliness < 0)
        {
            GameManager.Instance.Cleanliness = 0;
        }
        if (GameManager.Instance.Fullness < 0)
        {
            GameManager.Instance.Fullness = 0;
        }
        if (GameManager.Instance.Love > 100)
        {
            GameManager.Instance.Love = 100;
        }
        GameOver();
    }

    public void GameOver()
    {
        GameManager.Instance.Timer = 0f;

        createFood.walkingEnd();
        pet.GetComponent<PetForWalking>().enabled = false;
        arSO.GetComponent<ARPlaneManager>().enabled = true;
        foreach (var go in arSO.GetComponent<ARPlaneManager>().trackables)
        {
            go.gameObject.SetActive(true);
        }
        arSO.GetComponent<TapToPlacePet>().enabled = true;
        arSO.GetComponent<TapToPlacePetForWalking>().enabled = false;
        InteractionUI.SetActive(true);

        Destroy(plane);
        clearWindow.SetActive(false);
        transform.gameObject.SetActive(false);
    }

    // Select Food what is get Randomly
    private int RandomFood()
    {
        int choose = Random.Range(0, normalProbConst + specialProbConst);

        for (int i = 0; i < normalFoodNum; i++)
        {
            if (choose < (normalProbConst / normalFoodNum) * (i + 1)
                && choose >= (normalProbConst / normalFoodNum) * (i))
            {
                return i;
            }
        }
        for (int i = normalFoodNum; i < normalFoodNum + specialFoodNum; i++)
        {
            if (choose < normalProbConst + (specialProbConst / specialFoodNum) * (i - normalFoodNum + 1)
                && choose >= normalProbConst + (specialProbConst / specialFoodNum) * (i - normalFoodNum))
            {
                return i;
            }
        }
        return 0;
    }
}
