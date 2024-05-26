using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarManager : MonoBehaviour
{
    public Image healthSlider;

    void Start()
    {
        healthSlider = GetComponent<Image>();
    }

    public void UpdateHealthBar(float currentHealth, float maxHealth)
    {
        healthSlider.fillAmount = currentHealth / maxHealth;
    }

    void Update()
    {
        
    }
}
