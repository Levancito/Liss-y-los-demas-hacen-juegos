using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CascoButton : MonoBehaviour
{
    [SerializeField] private ShopScript Shop;
    [SerializeField] private Button _button;
    [SerializeField] private GameObject Inactivo;
    [SerializeField] private PopupConfirmacion popupConfirmacion;


    void Start()
    {
        Shop = FindObjectOfType<ShopScript>();

        _button.onClick.RemoveAllListeners();
        _button.onClick.AddListener(ConfirmarCompra);
    }

    void ConfirmarCompra()
    {
        popupConfirmacion.Mostrar("¿Querés comprar el casco?", BuyCosmetic);
    }

    void BuyCosmetic()
    {
        bool comprado = Shop.tryBuy(10, 1);
        if (comprado)
        {
            _button.interactable = false;      
            Inactivo.SetActive(false);           
        }
    }
}