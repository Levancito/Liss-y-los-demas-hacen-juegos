using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class OjosBoton : MonoBehaviour
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
        popupConfirmacion.Mostrar("¿Querés comprar los ojos?", BuyCosmetic);
    }

    void BuyCosmetic()
    {
        bool comprado = Shop.tryBuy(10, 3);
        if (comprado)
        {
            _button.interactable = false;
            Inactivo.SetActive(false);
        }
    }
}

