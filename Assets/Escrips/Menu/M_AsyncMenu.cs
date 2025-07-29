using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class M_AsyncMenu : MonoBehaviour
{
    public GameObject PantallaDeCarga;
    public ResourceManager ResourceManager;
    public PopupConfirmacion popupConfirmacion;

    public void Awake()
    {
        ResourceManager = FindObjectOfType<ResourceManager>();
    }

    private void Start()
    {
        if (GameSessionSummary.mostrarResumen)
        {
            MostrarPopupResumen();
            GameSessionSummary.Reset();
        }
    }

    public void CargarNivel(int NumeroDeEscena)
    {
        Time.timeScale = 1;
        StartCoroutine(CargarAsync(NumeroDeEscena));

        if (NumeroDeEscena == 1)
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

    public void VolverAlMenuConConfirmacion()
    {
        popupConfirmacion.Mostrar("¿Querés volver al menú principal?", () =>
        {
            CargarNivel(0); 
        });
    }

    public void MostrarPopupResumen()
    {
        var (monedas, tuercas, nafta) = GameSessionSummary.ObtenerDiferencias();

        string mensaje = $"Resumen del nivel:\n" +
                         $"Monedas ganadas: {monedas}\n" +
                         $"Tuercas ganadas: {tuercas}\n" +
                         $"Nafta obtenida: {nafta}";

        popupConfirmacion.botonNo.gameObject.SetActive(false);
        popupConfirmacion.Mostrar(mensaje, () =>
        {
            GameSessionSummary.Reset();
            SceneManager.LoadScene(0); 
        });
    }
}