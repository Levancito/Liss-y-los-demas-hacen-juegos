using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class M_AsyncMenu : MonoBehaviour
{
    public GameObject PantallaDeCarga;

    public void CargarNivel(int NumeroDeEscena)
    {
        StartCoroutine(CargarAsync(NumeroDeEscena));
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
