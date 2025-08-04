using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Services.Authentication;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
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
    public void ResetResources()
    {
        ActualCurrency = 0;
        ActualTuerca = 0;
        ActualNafta = 5;
        updateResource();
    }
    public void LoadFromSaveFile()
    {
        ActualCurrency = SaveFile.coins;
        ActualTuerca = SaveFile.bolts;
        ActualNafta = SaveFile.fuel;
    }

    public void updateResource()
    {
        SaveFile.coins = ActualCurrency;
        SaveFile.bolts = ActualTuerca;
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

            GameSessionSummary.GuardarValoresIniciales();


            FindObjectOfType<M_AsyncMenu>().CargarNivel(1); 
        }
        
    }
}