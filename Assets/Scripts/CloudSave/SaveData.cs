using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveData : MonoBehaviour
{

    private void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
        {
            CloudSaveData.Instance.SaveData();
        }
    }

    private void OnApplicationQuit()
    {
        CloudSaveData.Instance.SaveData();
    }
}
