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

    private bool isGameActive = false; // Indica si el juego est� activo

    // Unidades m�tricas y sus equivalencias
    private readonly string[] units = { "m", "dam", "hm", "km", "Mm", "Gm", "Tm", "Pm", "Em", "Zm", "Ym", "Rm", "Qm" };
    private readonly double[] unitThresholds = {
        1,             // metros
        10,            // dec�metros
        100,           // hect�metros
        1_000,         // kil�metros
        1_000_000,     // meg�metros
        1_000_000_000, // gig�metros
        1e12,          // ter�metros
        1e15,          // pet�metros
        1e18,          // ex�metros
        1e21,          // zett�metros
        1e24,          // yott�metros
        1e27,          // ronn�metros
        1e30           // quett�metros
    };

    private void Update()
    {
        if (!isGameActive) return; // Solo calcula si el juego est� activo

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

    // M�todo para iniciar el conteo
    public void StartGame()
    {
        isGameActive = true;
        timeElapsed = 0f; // Reinicia el tiempo al inicio del juego
        distanceCovered = 0f; // Reinicia la distancia
    }

    // M�todo para pausar o detener el conteo
    public void StopGame()
    {
        isGameActive = false;
    }
}

