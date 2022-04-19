using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PetStatusManager : MonoBehaviour
{
    public Slider loveSlider;
    public Slider cleanlinessSlider;
    public Slider fullnessSlider;
    public Text petName;
    // Update is called once per frame
    void Update()
    {
        petName.text = GameManager.Instance.petName;
        loveSlider.value = GameManager.Instance.Love;
        cleanlinessSlider.value = GameManager.Instance.Cleanliness;
        fullnessSlider.value = GameManager.Instance.Fullness;
    }
}
