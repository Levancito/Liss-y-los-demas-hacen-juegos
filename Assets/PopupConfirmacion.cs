using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class PopupConfirmacion : MonoBehaviour
{
    public TextMeshProUGUI mensajeTexto;
    public Button botonSi;
    public Button botonNo;

    private Action onConfirmar;

    public void Mostrar(string mensaje, Action onConfirmar)
    {
        this.onConfirmar = onConfirmar;
        mensajeTexto.text = mensaje;
        gameObject.SetActive(true);
    }

    private void Awake()
    {
        botonSi.onClick.AddListener(() =>
        {
            onConfirmar?.Invoke();
            gameObject.SetActive(false);
        });

        botonNo.onClick.AddListener(() =>
        {
            gameObject.SetActive(false);
        });

        gameObject.SetActive(false);
    }
}
