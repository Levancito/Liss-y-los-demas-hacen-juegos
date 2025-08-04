using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_Animations : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private float tiempoCorregir = 0.5f;
    [SerializeField] private float umbralMovimiento = 0.01f;
    [SerializeField] private float tiempoBloqueoPostGiro = 2f;

    private bool corrigiendo = false;
    private float tiempoCorriendo = 10f;
    private float tiempoBloqueado = 0f;

    public void ReportarMovimiento(Vector3 input)
    {
        if (corrigiendo || tiempoBloqueado > 0f) return;

        if (input.x > umbralMovimiento)
        {
            animator.ResetTrigger("GirandoIzquierda");
            animator.SetTrigger("Girando");
            IniciarCorregir();
        }
        else if (input.x < -umbralMovimiento)
        {
            animator.ResetTrigger("Girando");
            animator.SetTrigger("GirandoIzquierda");
            IniciarCorregir();
        }
    }

    private void IniciarCorregir()
    {
        corrigiendo = true;
        tiempoCorriendo = 0f;
        tiempoBloqueado = tiempoBloqueoPostGiro;
    }

    private void Update()
    {
        if (corrigiendo)
        {
            tiempoCorriendo += Time.deltaTime;
            if (tiempoCorriendo >= tiempoCorregir)
            {
                animator.SetTrigger("CorregirMovimiento");
                corrigiendo = false;
            }
        }

        // Contador de tiempo bloqueado
        if (tiempoBloqueado > 0f)
        {
            tiempoBloqueado -= Time.deltaTime;
        }
    }
}

