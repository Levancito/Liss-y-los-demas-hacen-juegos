using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopScript : MonoBehaviour
{
    public ResourceManager resourceManager;

    public void Start()
    {
        resourceManager = FindObjectOfType<ResourceManager>();
    }

    public bool tryBuy(int Precio, int Custom)
    {
        if (resourceManager.ActualCurrency >= Precio)
        {
            resourceManager.ActualCurrency -= Precio;
            CosmeticManager.Instance.EnableCosmetic(Custom);
            resourceManager.updateResource();
            return true;
        }
        return false;
    }
}
