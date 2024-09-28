using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WinConditionManager : MonoBehaviour
{
    private int _enemyAmountIndex = 0;
    private int _requiredKills = 15;
    [SerializeField] private TextMeshProUGUI enemiesRemainingText;


    private void Awake()
    {
        EventManager.SubscribeToEvent(EventsType.Event_EnemyDestroyed, onEnemyDestroyed);
        EventManager.SubscribeToEvent(EventsType.Event_Victory, Victory);
        EventManager.SubscribeToEvent(EventsType.Event_Defeat, Defeat);

        UpdateEnemiesRemainingText();

    }

    void onEnemyDestroyed(object[] p)
    {
        _enemyAmountIndex += p.Length;
        UpdateEnemiesRemainingText();

        if (_enemyAmountIndex >= _requiredKills)
        {
            EventManager.TriggerEvent(EventsType.Event_Victory, this);
        }
    }

    void UpdateEnemiesRemainingText()
    {
        int enemiesRemaining = _requiredKills - _enemyAmountIndex;
        enemiesRemainingText.text = $"Enemigos restantes: {enemiesRemaining}";
    }

    void Victory(object[] p)
    {
        //aca van las acciones que se triggerean cuando se gana
        EventManager.UnsubscribeToEvent(EventsType.Event_EnemyDestroyed, onEnemyDestroyed);
        EventManager.UnsubscribeToEvent(EventsType.Event_Victory, Victory);
        EventManager.UnsubscribeToEvent(EventsType.Event_Defeat, Defeat);
        Time.timeScale = 0;
    }
    void Defeat(object[] p)
    {
        EventManager.UnsubscribeToEvent(EventsType.Event_EnemyDestroyed, onEnemyDestroyed);
        EventManager.UnsubscribeToEvent(EventsType.Event_Victory, Victory);
        EventManager.UnsubscribeToEvent(EventsType.Event_Defeat, Defeat);
        Time.timeScale = 0;
    }
}