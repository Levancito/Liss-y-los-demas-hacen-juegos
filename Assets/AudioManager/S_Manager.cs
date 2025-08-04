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
        sliderMusica.value = PlayerPrefs.GetFloat("VolumenMusica", 0.75f);
        sliderSonidos.value = PlayerPrefs.GetFloat("VolumenSonidos", 0.75f);

        Debug.Log("Volumen música inicial: " + sliderMusica.value);
        Debug.Log("Volumen sonidos inicial: " + sliderSonidos.value);

        CambiarVolumenMusica(sliderMusica.value);
        CambiarVolumenSonidos(sliderSonidos.value);

        sliderMusica.onValueChanged.AddListener(CambiarVolumenMusica);
        sliderSonidos.onValueChanged.AddListener(CambiarVolumenSonidos);
    }

    public void CambiarVolumenMusica(float valor)
    {
        if (valor > 0.001f)
        {
            audioMixer.SetFloat("VolumenMusica", Mathf.Log10(valor) * 20); 
        }
        else
        {
            audioMixer.SetFloat("VolumenMusica", -80f);
        }
        PlayerPrefs.SetFloat("VolumenMusica", valor);
    }

    public void CambiarVolumenSonidos(float valor)
    {
        if (valor > 0.001f)
        {
            audioMixer.SetFloat("VolumenSonidos", Mathf.Log10(valor) * 20); 
        }
        else
        {
            audioMixer.SetFloat("VolumenSonidos", -80f);
        }
        PlayerPrefs.SetFloat("VolumenSonidos", valor);
    }
}