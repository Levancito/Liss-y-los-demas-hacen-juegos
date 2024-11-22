using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager instance;

    public int ActualCurrency;
    public int ActualTuerca;
    public int ActualNafta;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

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
