using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveButton : MonoBehaviour
{
    [SerializeField] private Button _button;
    void Start()
    {
        _button.onClick.RemoveAllListeners();
        _button.onClick.AddListener(CloudSaveData.Instance.SaveData);
    }
}
