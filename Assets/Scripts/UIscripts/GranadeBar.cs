using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GranadeBar : MonoBehaviour
{
    public Image GranadeChargeSlider;

    void Start()
    {
        GranadeChargeSlider = GetComponent<Image>();
    }

    public void UpdateGranadeChargeBar(float ChargeAmount, float ChargeLimit) 
    {
        GranadeChargeSlider.fillAmount = ChargeAmount / ChargeLimit;
    }

    void Update()
    {
        
    }
}
