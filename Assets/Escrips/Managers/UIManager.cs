using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject _victoryScreen;
    [SerializeField] private GameObject _defeatScreen;

    void Awake()
    {
        EventManager.SubscribeToEvent(EventsType.Event_Victory, ShowVictoryScreen);
        EventManager.SubscribeToEvent(EventsType.Event_Defeat, ShowDefeatScreen);
    }

    public void ShowVictoryScreen(object[] p)
    {
        _victoryScreen.SetActive(true);
    }
    public void ShowDefeatScreen(object[] p)
    {
        _defeatScreen.SetActive(true);
    }
}
