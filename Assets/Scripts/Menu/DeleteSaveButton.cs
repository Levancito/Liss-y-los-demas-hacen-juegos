using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeleteSaveButton : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private PopupConfirmacion popupConfirmacion;

    void Start()
    {
        _button.onClick.RemoveAllListeners();
        _button.onClick.AddListener(MostrarPopupDeConfirmacion);
    }

    public void MostrarPopupDeConfirmacion()
    {
        popupConfirmacion.Mostrar("�Est�s seguro de que quer�s borrar todos los datos guardados?", ConfirmarBorrado);
    }

    void ConfirmarBorrado()
    {
        CloudSaveData.Instance.DeleteData();
    }
}