using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public TMPro.TMP_InputField petNameField;
    private bool isFirst = true;

    void Start()
    {
        if(!PlayerPrefs.HasKey("petName"))
        {
            isFirst = true;
            petNameField.gameObject.SetActive(true);
        }
        else
        {
            isFirst = false;
            Load();
        }
    }

    void Update()
    {
        if(!isFirst)
        {
            if(GameManager.Instance.petNum != PlayerPrefs.GetInt("petNum") ||
                GameManager.Instance.Love != PlayerPrefs.GetInt("Love") ||
                GameManager.Instance.Cleanliness != PlayerPrefs.GetInt("Cleanliness") ||
                GameManager.Instance.Fullness != PlayerPrefs.GetInt("Fullness") ||
                GameManager.Instance.petName != PlayerPrefs.GetString("petName"))
            {
                Save();
            }
            else
            {
                for (int i = 0; i < GameManager.Instance.foodStock.Length; i++)
                {
                    if(GameManager.Instance.foodStock[i] != PlayerPrefs.GetInt("foodStock" + i))
                    {
                        Save();
                    }
                }

                for (int i = 0; i < GameManager.Instance.petStomach.Length; i++)
                {
                    if(GameManager.Instance.petStomach[i] != PlayerPrefs.GetInt("petStomach" + i))
                    {
                        Save();
                    }
                }
            }
        }
    }

    public static void Save()
    {
        PlayerPrefs.SetInt("petNum", GameManager.Instance.petNum);
        PlayerPrefs.SetInt("Love", GameManager.Instance.Love);
        PlayerPrefs.SetInt("Cleanliness", GameManager.Instance.Cleanliness);
        PlayerPrefs.SetInt("Fullness", GameManager.Instance.Fullness);

        for(int i = 0; i < GameManager.Instance.foodStock.Length; i++)
        {
            PlayerPrefs.SetInt("foodStock" + i, GameManager.Instance.foodStock[i]);
        }

        for (int i = 0; i < GameManager.Instance.petStomach.Length; i++)
        {
            PlayerPrefs.SetInt("petStomach" + i, GameManager.Instance.petStomach[i]);
        }

        PlayerPrefs.SetString("petName", GameManager.Instance.petName);
    }

    private void Load()
    {
        GameManager.Instance.petNum = PlayerPrefs.GetInt("petNum");
        GameManager.Instance.Love = PlayerPrefs.GetInt("Love");
        GameManager.Instance.Cleanliness = PlayerPrefs.GetInt("Cleanliness");
        GameManager.Instance.Fullness = PlayerPrefs.GetInt("Fullness");

        for (int i = 0; i < GameManager.Instance.foodStock.Length; i++)
        {
            GameManager.Instance.foodStock[i] = PlayerPrefs.GetInt("foodStock" + i);
        }

        for (int i = 0; i < GameManager.Instance.petStomach.Length; i++)
        {
            GameManager.Instance.petStomach[i] = PlayerPrefs.GetInt("petStomach" + i);
        }

        GameManager.Instance.petName = PlayerPrefs.GetString("petName");
    }

    public void editPetName()
    {
        GameManager.Instance.petName = petNameField.text;
        Save();
        isFirst = false;
        petNameField.gameObject.SetActive(false);
    }
}
