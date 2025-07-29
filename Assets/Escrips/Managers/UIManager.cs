using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject _defeatScreen;

    private void Awake()
    {


        EventManager.SubscribeToEvent(EventsType.Event_Victory, ShowVictoryScreen);
        EventManager.SubscribeToEvent(EventsType.Event_Defeat, ShowDefeatScreen);
    }

    public void ShowVictoryScreen(object[] p)
    {
        UnsubscribeEvents();

    }

    public void ShowDefeatScreen(object[] p)
    {
        UnsubscribeEvents();

        if (_defeatScreen != null)
        {
            _defeatScreen.SetActive(true);
        }
        else
        {
            Debug.LogWarning("La pantalla de derrota (_defeatScreen) fue destruida o no está asignada.");
        }
    }

    private void UnsubscribeEvents()
    {
        EventManager.UnsubscribeToEvent(EventsType.Event_Victory, ShowVictoryScreen);
        EventManager.UnsubscribeToEvent(EventsType.Event_Defeat, ShowDefeatScreen);
    }
}
