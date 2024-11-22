using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class M_Menu : MonoBehaviour
{
    [Header("Paneles del Menú")]
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
        ResourceManager.ActualCurrency -= 10;
        ResourceManager.AddStamina(1);
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

    
}
