using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MagazineBar : MonoBehaviour
{
    public Image magazineSlider;

    void Start()
    {
        magazineSlider = GetComponent<Image>();
    }

    public void UpdateReloadBar(float remainBullets, float magazineSize)
    {
        magazineSlider.fillAmount = remainBullets / magazineSize;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
