using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; 

public class DistanceTracker : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI distanceText; // Campo para mostrar la distancia en UI usando TextMeshPro

    private float timeElapsed = 0f; // Tiempo jugado en segundos
    private double distanceCovered = 0f; // Distancia total en metros (usando double para grandes valores)

    private bool isGameActive = false; // Indica si el juego está activo

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

    private void Update()
    {
        if (!isGameActive) return; // Solo calcula si el juego está activo

        // Incrementa el tiempo jugado
        timeElapsed += Time.deltaTime;

        // Calcula la distancia (1 segundo = 1 metro)
        distanceCovered = timeElapsed;

        // Actualiza el texto mostrado
        UpdateDistanceText();
    }

    private void UpdateDistanceText()
    {
        string unit = "m";
        double displayValue = distanceCovered;

        // Determina la unidad apropiada
        for (int i = 0; i < unitThresholds.Length; i++)
        {
            if (distanceCovered < unitThresholds[i])
            {
                break;
            }
            unit = units[i];
            displayValue = distanceCovered / unitThresholds[i];
        }

        // Muestra la distancia con 2 decimales
        distanceText.text = $"{displayValue:F2} {unit}";
    }

    // Método para iniciar el conteo
    public void StartGame()
    {
        isGameActive = true;
        timeElapsed = 0f; // Reinicia el tiempo al inicio del juego
        distanceCovered = 0f; // Reinicia la distancia
    }

    // Método para pausar o detener el conteo
    public void StopGame()
    {
        isGameActive = false;
    }
}

