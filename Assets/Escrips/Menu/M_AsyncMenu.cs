using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class M_AsyncMenu : MonoBehaviour
{
    public GameObject PantallaDeCarga;
    public ResourceManager ResourceManager;

    public void Awake()
    {
        ResourceManager = FindObjectOfType<ResourceManager>();
    }
    public void CargarNivel(int NumeroDeEscena)
    {
        
        Time.timeScale = 1;
        StartCoroutine(CargarAsync(NumeroDeEscena));

        if(NumeroDeEscena == 1)
        {
            ResourceManager.TryPlay();
        }
    }
    IEnumerator CargarAsync(int NumeroDeEscena)
    {
        AsyncOperation Operacion = SceneManager.LoadSceneAsync(NumeroDeEscena);
        PantallaDeCarga.SetActive(true);

        while (!Operacion.isDone)
        {
            float Progreso = Mathf.Clamp01(Operacion.progress / .9f);
            Debug.Log(Progreso);
            yield return null;
        }

    }
    public void Exit()
    {
        Debug.Log("Salir");
        Application.Quit();
    }
}
