using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class M_Menu : MonoBehaviour
{
    [Header("Paneles del Men�")]
    public GameObject panelPrincipal;
    public GameObject panelTienda;
    public GameObject panelOpciones;

    public TextMeshProUGUI Plata;
    public TextMeshProUGUI Plata2;

    public ResourceManager ResourceManager;

    
    private void Start()
    {
        ActualizarData();
        ResourceManager = ResourceManager.FindObjectOfType<ResourceManager>();
        MostrarPanel(panelPrincipal);
    }

    public void Update()
    {
        ActualizarPlata();
    }

    public void MostrarPanel(GameObject panelActivo)
    {
        panelPrincipal.SetActive(panelActivo == panelPrincipal);
        panelTienda.SetActive(panelActivo == panelTienda);
        panelOpciones.SetActive(panelActivo == panelOpciones);
    }

    public void AbrirTienda()
    {
        MostrarPanel(panelTienda);
    }

    public void AbrirOpciones()
    {
        MostrarPanel(panelOpciones);
    }

    public void VolverAlMenuPrincipal()
    {
        MostrarPanel(panelPrincipal);
    }


    public void ComprarStamina()
    {
        int costo = 10;

        if (ResourceManager.ActualCurrency >= costo)
        {
            ResourceManager.ActualCurrency -= costo;
            ResourceManager.AddStamina(1);
        }
        else
        {
            Debug.LogWarning("No ten�s suficiente plata para comprar stamina.");
        }
    }

    public void ActualizarPlata()
    {
        Plata.text = $"{ResourceManager.ActualCurrency}"; ;
        Plata2.text = $"{ResourceManager.ActualCurrency}"; ;
    }

    public async void ActualizarData()
    {
        await CloudSaveData.Instance.LoadData();
    }

    public void OnDropdownChanged(int index)
    {
        PlayerPrefs.SetInt("ControlType", index);
        PlayerPrefs.Save();
    }

}
