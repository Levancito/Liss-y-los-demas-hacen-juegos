using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameSessionSummary
{
    public static int monedasAntes;
    public static int tuercasAntes;
    public static int naftaAntes;

    public static bool mostrarResumen = false;

    public static void GuardarValoresIniciales()
    {
        monedasAntes = ResourceManager.Instance.ActualCurrency;
        tuercasAntes = ResourceManager.Instance.ActualTuerca;
        naftaAntes = ResourceManager.Instance.ActualNafta;
        mostrarResumen = true;
    }

    public static (int monedas, int tuercas, int nafta) ObtenerDiferencias()
    {
        return (
            ResourceManager.Instance.ActualCurrency - monedasAntes,
            ResourceManager.Instance.ActualTuerca - tuercasAntes,
            ResourceManager.Instance.ActualNafta - naftaAntes
        );
    }

    public static void Reset()
    {
        mostrarResumen = false;
    }
}