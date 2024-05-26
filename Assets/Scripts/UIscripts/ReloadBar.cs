using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReloadBar : MonoBehaviour
{
    public Image ReloadSlider;
    public MyGunManager GunManager;

    void Start()
    {
        ReloadSlider = GetComponent<Image>();
        GunManager = GameObject.Find("Arm").GetComponent<MyGunManager>();
    }

    void Update()
    {
        if (GunManager.Reloading == true)
        {
            ReloadSlider.fillAmount = (Time.time - GunManager.reloadTimer) / GunManager.reloadTime;
        } else
        {
            ReloadSlider.fillAmount = 0;
        }
    }
}
