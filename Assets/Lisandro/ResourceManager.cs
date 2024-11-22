using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    //public static ResourceManager instance;
    public static ResourceManager Instance { get; private set; }
    public static SaveFile SaveFile { get; private set; }

    public int ActualCurrency;
    public int ActualTuerca;
    public int ActualNafta;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);

        if (SaveFile == null)
        {
            SaveFile = FindObjectOfType<CloudSaveData>().saveFile;
        }
        ActualCurrency = SaveFile.coins;
        ActualTuerca = SaveFile.bolts;
        ActualNafta = SaveFile.fuel;
    }

    public void AddCurrency(int cantidad)
    {
        ActualCurrency += cantidad;
        SaveFile.coins = ActualCurrency;
    }

    public void AddTuerca(int cantidad)
    {
        ActualTuerca += cantidad;
        SaveFile.bolts = ActualTuerca;
    }

    public void AddStamina(int cantidad)
    {
        ActualNafta += cantidad;
        SaveFile.fuel = ActualNafta;
        SaveFile.fuel = Mathf.Clamp(SaveFile.fuel, 0, 5);
    }

    public void TryPlay()
    {
        if (ActualNafta >= 1)
        {
            ActualNafta--;
            SaveFile.fuel = ActualNafta;
            SaveFile.fuel = Mathf.Clamp(SaveFile.fuel, 0, 5);
            // para el boton de play de la UI deberia llamar a esto, si le da la nafta, llama a otro script al metodo
            // que cambie de escena. eso o que el script de cambio de escena se fije aca
            //cambio de escena capaz?
        }
        else
        {
            //capaz un cartel de UI que te avise que no podes arrancar?
            Debug.LogWarning("No tenes nafta para arrancar la nave");
        }
    }
}
