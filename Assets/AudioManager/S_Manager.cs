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

        CambiarVolumenMusica(sliderMusica.value);
        CambiarVolumenSonidos(sliderSonidos.value);
    }


    public void CambiarVolumenMusica(float valor)
    {
        if (audioMixer.SetFloat("VolumenMusica", Mathf.Log10(valor) * 20))
        {
        }
        else
        {
        }
        PlayerPrefs.SetFloat("VolumenMusica", valor);
    }

    public void CambiarVolumenSonidos(float valor)
    {
        if (audioMixer.SetFloat("VolumenSonidos", Mathf.Log10(valor) * 20))
        {
        }
        else
        {
        }
        PlayerPrefs.SetFloat("VolumenSonidos", valor);
    }
}
