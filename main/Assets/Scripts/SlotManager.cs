using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class SlotManager : MonoBehaviour
{
    private bool isSpawn = false;
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
        if (GameManager.Instance.isFoodSpawn == false && GameManager.Instance.foodStock[slotnum] != 0)
        {
            GameObject spawned = Instantiate(prefab, new Vector3(0, 0, 0.5f), Quaternion.identity);
            spawned.transform.localScale = new Vector3(5f, 5f, 5f);
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
    }
}
