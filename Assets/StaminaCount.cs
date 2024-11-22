using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaCount : MonoBehaviour
{
    public ResourceManager ResourceManager;
    public Image staminaBar;

    public void Start()
    {
        ResourceManager = FindObjectOfType<ResourceManager>();
    }

    public void Update()
    {
        ActualizarStamina();
    }

    public void ActualizarStamina()
    {
        staminaBar.fillAmount = Mathf.Clamp01(ResourceManager.ActualNafta / 5f);
    }
}
