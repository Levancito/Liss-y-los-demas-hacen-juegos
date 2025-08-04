using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class SaveButton : MonoBehaviour
{
    [SerializeField] private Button _button;
    void Start()
    {
        _button.onClick.RemoveAllListeners();
        _button.onClick.AddListener(() => Save());
    }

    private async void Save()
    {
        await CloudSaveData.Instance.SaveData();
        Debug.Log("Data saved successfully");

    }   
}
