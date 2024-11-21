using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public int ActualCurrency;
    public int ActualTuerca;
    public int ActualNafta;


    public void AddCurrency(int cantidad)
    {
        ActualCurrency += cantidad;
    }

    public void AddTuerca(int cantidad)
    {
        ActualTuerca += cantidad;
    }

    public void AddStamina(int cantidad)
    {
        ActualNafta += cantidad;
    }
}
