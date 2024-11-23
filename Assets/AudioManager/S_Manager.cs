using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;


public class S_Manager : MonoBehaviour
{
    [Header("Audio Mixers")]
    public AudioMixer audioMixer;

    [Header("Sliders")]
    public Slider sliderMusica;
    public Slider sliderSonidos;

    private void Start()
    {
        // Cargar valores guardados o asignar valores predeterminados
        sliderMusica.value = PlayerPrefs.GetFloat("VolumenMusica", 0.75f);
        sliderSonidos.value = PlayerPrefs.GetFloat("VolumenSonidos", 0.75f);

        // Aplicar volúmenes iniciales
        CambiarVolumenMusica(sliderMusica.value);
        CambiarVolumenSonidos(sliderSonidos.value);

        // Suscribir los eventos de cambio de los sliders
        sliderMusica.onValueChanged.AddListener(CambiarVolumenMusica);
        sliderSonidos.onValueChanged.AddListener(CambiarVolumenSonidos);
    }

    public void CambiarVolumenMusica(float valor)
    {
        if (valor > 0.001f)
        {
            audioMixer.SetFloat("VolumenMusica", Mathf.Log10(valor) * 20); // Convierte a decibelios
        }
        else
        {
            audioMixer.SetFloat("VolumenMusica", -80f); // Silencia cuando el valor es 0
        }
        PlayerPrefs.SetFloat("VolumenMusica", valor);
    }

    public void CambiarVolumenSonidos(float valor)
    {
        if (valor > 0.001f)
        {
            audioMixer.SetFloat("VolumenSonidos", Mathf.Log10(valor) * 20); // Convierte a decibelios
        }
        else
        {
            audioMixer.SetFloat("VolumenSonidos", -80f); // Silencia cuando el valor es 0
        }
        PlayerPrefs.SetFloat("VolumenSonidos", valor);
    }
}