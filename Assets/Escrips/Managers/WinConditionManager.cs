using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinConditionManager : MonoBehaviour
{
    private int _enemyAmountIndex = 0;
    private int _requiredKills = 1;

    private void Awake()
    {
        EventManager.SubscribeToEvent(EventsType.Event_EnemyDestroyed, onEnemyDestroyed);
        EventManager.SubscribeToEvent(EventsType.Event_Victory, Victory);
        EventManager.SubscribeToEvent(EventsType.Event_Defeat, Defeat);
    }

    void onEnemyDestroyed(object[] p)
    {
        _enemyAmountIndex += p.Length;
        if (_enemyAmountIndex >= _requiredKills)
        {
            EventManager.TriggerEvent(EventsType.Event_Victory, this);
        }
    }
    void Victory(object[] p)
    {
        //aca van las acciones que se triggerean cuando se gana
        Time.timeScale = 0;
    }
    void Defeat(object[] p)
    {

        Time.timeScale = 0;
    }
}