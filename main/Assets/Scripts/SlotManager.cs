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
    Notice_UI notice;
    [SerializeField]
    private GameObject interactionBar, foodInventory;
    private GameObject spawned;
    void Start()
    {
        notice = FindObjectOfType<Notice_UI>();
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
            spawned = Instantiate(prefab, Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, Camera.main.nearClipPlane + 0.5f)), Quaternion.identity);
            spawned.name = prefab.name;
            spawned.tag = "Food";
            GameManager.Instance.isFoodSpawn = true;
            GameManager.Instance.foodStock[slotnum] -= 1;
            numtext[slotnum].text = "" + GameManager.Instance.foodStock[slotnum];

        }
        else if(GameManager.Instance.isFoodSpawn == true)
        {
            notice.SUB("Other food is spawned already.");
        }
        else if(GameManager.Instance.foodStock[slotnum] == 0)
        {
            notice.SUB("You don't have this food.");
            foodInventory.SetActive(false);
            interactionBar.SetActive(true);
        }
        else if(GameManager.Instance.Fullness >= 100)
        {
            notice.SUB("The Pet is full ");
            foodInventory.SetActive(false);
            interactionBar.SetActive(true);
        }

    }

    public void backMain()
    {
        if (GameManager.Instance.isFoodSpawn)
        {
            Destroy(spawned);
            GameManager.Instance.isFoodSpawn = false;
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
