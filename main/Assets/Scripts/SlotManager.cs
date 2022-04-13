using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class SlotManager : MonoBehaviour
{
    private int slotnum;
    [SerializeField]
    private TextMeshProUGUI[] numtext;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    // Update is called once per frame

    public void setSlotnum(int num)
    {
        slotnum = num;
    }
    public void instantiatePrefab(GameObject prefab)
    {
        if (GameManager.Instance.isFoodSpawn == false && GameManager.Instance.foodStock[slotnum] != 0 && GameManager.Instance.Fullness < 100)
        {
            GameObject spawned = Instantiate(prefab, Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, Camera.main.nearClipPlane + 0.5f)), Quaternion.identity);
            spawned.name = prefab.name;
            spawned.tag = "Food";
            GameManager.Instance.isFoodSpawn = true;
            GameManager.Instance.foodStock[slotnum] -= 1;
            numtext[slotnum].text = "" + GameManager.Instance.foodStock[slotnum];

        }
        else if(GameManager.Instance.isFoodSpawn == true)
        {
            Debug.Log("Food already exist");
        }
        else if(GameManager.Instance.foodStock[slotnum] == 0)
        {
            Debug.Log("the food empty");
        }
        else if(GameManager.Instance.Fullness >= 100)
        {
            Debug.Log("I'm full");
        }

    }
    public void InitslotText()
    {
        for (int i = 0; i < GameManager.Instance.foodStock.Length; i++)
        {
            numtext[i].text = "" + GameManager.Instance.foodStock[i];
        }
    }
}
