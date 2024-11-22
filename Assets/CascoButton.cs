using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CascoButton : MonoBehaviour
{
    [SerializeField] private ShopScript Shop;
    [SerializeField] private Button _button;
    [SerializeField] private GameObject Inactivo;

    void Start()
    {
        Inactivo.SetActive(false);
        Shop = ShopScript.FindObjectOfType<ShopScript>();

        _button.onClick.RemoveAllListeners();
        _button.onClick.AddListener(() => Shop.tryBuy(10, 1));
    }
}
