using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WinConditionManager : MonoBehaviour
{
    private int _enemyAmountIndex = 0;
    private int _requiredKills = 15;

    [SerializeField] private TextMeshProUGUI enemiesRemainingText;
    [SerializeField] private GameObject continueUI;

    private bool hasWatchedAd = false;



    private void Awake()
    {
        SubscribeAll();
        UpdateEnemiesRemainingText();
    }

    public void OnEnemyDestroyed(object[] p)
    {
        _enemyAmountIndex += p.Length;
        UpdateEnemiesRemainingText();

        if (_enemyAmountIndex >= _requiredKills)
        {
            EventManager.TriggerEvent(EventsType.Event_Victory, this);
        }
    }

    public void OnVictory(object[] p)
    {
        UnsubscribeAll();
        Time.timeScale = 0;
        Debug.Log("Victoria!");
    }

    public void OnDefeat(object[] p)
    {
        UnsubscribeAll();

        if (!hasWatchedAd)
        {
            Time.timeScale = 0;
            continueUI.SetActive(true);
            Debug.Log("Derrota - se ofrece continuar con ad.");
        }
        else
        {
            Time.timeScale = 0;
            Debug.Log("Derrota definitiva.");
        }
    }

    public void OnContinueButtonPressed()
    {
        AdsManager.instance.ShowRewardedAd(() =>
        {
            hasWatchedAd = true;
            continueUI.SetActive(false);
            Time.timeScale = 1;

            SubscribeAll();

            Debug.Log("Jugador continuó después del anuncio.");
        });
    }

    private void UpdateEnemiesRemainingText()
    {
        int enemiesRemaining = _requiredKills - _enemyAmountIndex;
        enemiesRemainingText.text = $"Enemigos restantes: {enemiesRemaining}";
    }

    private void SubscribeAll()
    {
        EventManager.SubscribeToEvent(EventsType.Event_EnemyDestroyed, OnEnemyDestroyed);
        EventManager.SubscribeToEvent(EventsType.Event_Victory, OnVictory);
        EventManager.SubscribeToEvent(EventsType.Event_Defeat, OnDefeat);
    }

    private void UnsubscribeAll()
    {
        EventManager.UnsubscribeToEvent(EventsType.Event_EnemyDestroyed, OnEnemyDestroyed);
        EventManager.UnsubscribeToEvent(EventsType.Event_Victory, OnVictory);
        EventManager.UnsubscribeToEvent(EventsType.Event_Defeat, OnDefeat);
    }
}
