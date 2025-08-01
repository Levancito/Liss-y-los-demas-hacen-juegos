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

    private AsyncOperation operacionActual = null;
    public void CargarNivel(int NumeroDeEscena)
    {
        if (operacionActual != null && !operacionActual.isDone) return;

        Time.timeScale = 1;
        operacionActual = SceneManager.LoadSceneAsync(NumeroDeEscena);
        StartCoroutine(ManejarCarga(operacionActual));

        if (NumeroDeEscena == 1)
        {
            ResourceManager.TryPlay();
        }
    }

    private IEnumerator ManejarCarga(AsyncOperation operacion)
    {
        PantallaDeCarga.SetActive(true);

        while (!operacion.isDone)
        {
            float progreso = Mathf.Clamp01(operacion.progress / .9f);
            Debug.Log(progreso);
            yield return null;
        }

        operacionActual = null;
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