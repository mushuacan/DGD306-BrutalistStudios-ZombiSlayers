using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniMapUI : MonoBehaviour
{
    public Slider slider;
    public void UpdateSlider(float yüzde)
    {
        slider.value = 1f - yüzde;
    }
}
