using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DistanceTracker : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI distanceText; 

    private float timeElapsed = 0f; 
    private double distanceCovered = 0f;

    private bool isGameActive = false;

    public SaveFile saveFile;
    public static double DistanceCovered { get; private set; }

    // Unidades métricas y sus equivalencias
    private readonly string[] units = { "m", "dam", "hm", "km", "Mm", "Gm", "Tm", "Pm", "Em", "Zm", "Ym", "Rm", "Qm" };
    private readonly double[] unitThresholds = {
        1,             // metros
        10,            // decámetros
        100,           // hectómetros
        1_000,         // kilómetros
        1_000_000,     // megámetros
        1_000_000_000, // gigámetros
        1e12,          // terámetros
        1e15,          // petámetros
        1e18,          // exámetros
        1e21,          // zettámetros
        1e24,          // yottámetros
        1e27,          // ronnámetros
        1e30           // quettámetros
    };

    void Awake()
    {
        saveFile = CloudSaveData.Instance.saveFile;
    }

    private void Start()
    {
        distanceText.text = "0.00 m";
        StartGame();
    }

    private void Update()
    {
        if (!isGameActive) return;
        timeElapsed += Time.deltaTime;
        DistanceCovered = timeElapsed; // Guardamos la distancia
        distanceCovered = DistanceCovered;

        UpdateDistanceText();
        UpdateHighscore();
    }

    private void UpdateDistanceText()
    {
        string unit = "m";
        double displayValue = distanceCovered;

        for (int i = 0; i < unitThresholds.Length; i++)
        {
            if (distanceCovered < unitThresholds[i])
            {
                break;
            }
            unit = units[i];
            displayValue = distanceCovered / unitThresholds[i];
        }

        distanceText.text = $"{displayValue:F2} {unit}";
    }

    public void StartGame()
    {
        isGameActive = true;
        timeElapsed = 0f; 
        distanceCovered = 0f;
    }

    public void UpdateHighscore()
    {
        var currentHighScore = saveFile.highScore;
        if (currentHighScore < timeElapsed)
        {
            saveFile.highScore = timeElapsed;
        }
    }

    public void StopGame()
    {
        //aca se podria mandar a savefile
    }
}

