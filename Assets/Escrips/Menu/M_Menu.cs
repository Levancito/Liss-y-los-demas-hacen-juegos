using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_Menu : MonoBehaviour
{
    [Header("Paneles del Menú")]
    public GameObject panelPrincipal;
    public GameObject panelTienda;
    public GameObject panelOpciones;

    private void Start()
    {
        MostrarPanel(panelPrincipal);
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
}
