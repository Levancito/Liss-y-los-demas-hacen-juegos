using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject _defeatScreen;
    [SerializeField] private GameObject _continuePanel;

    private bool hasWatchedAd = false;

    private void Awake()
    {
        EventManager.SubscribeToEvent(EventsType.Event_Victory, ShowVictoryScreen);
        EventManager.SubscribeToEvent(EventsType.Event_Defeat, ShowDefeatScreen);

        if (_continuePanel != null)
            _continuePanel.SetActive(false);
    }

    public void ShowVictoryScreen(object[] p)
    {
        UnsubscribeEvents();
        // Podés activar la pantalla de victoria si querés
    }

    public void ShowDefeatScreen(object[] p)
    {
        UnsubscribeEvents();

        Time.timeScale = 0;

        if (!hasWatchedAd && _continuePanel != null)
        {
            _continuePanel.SetActive(true); 
        }
        else if (_defeatScreen != null)
        {
            _defeatScreen.SetActive(true); 
        }
        else
        {
            Debug.LogWarning("La pantalla de derrota (_defeatScreen) no está asignada.");
        }
    }

    public void OnContinueButtonPressed()
    {
        AdsManager.instance.ShowRewardedAd(() =>
        {
            hasWatchedAd = true;

            if (_continuePanel != null) _continuePanel.SetActive(false);
            if (_defeatScreen != null) _defeatScreen.SetActive(false);

            Time.timeScale = 1;

            Stats playerStats = GameObject.FindObjectOfType<Stats>();

            if (playerStats == null)
            {
            }
            else
            {
                playerStats.Heal(50);

                var shooting = playerStats.GetComponent<P_ShootController>();
                if (shooting != null)
                {
                    shooting.enabled = true;
                }
                

                var control = playerStats.GetComponent<P_Crontrol>();
                if (control != null)
                {
                    control.enabled = true;
                }
                
            }



            WinConditionManager win = GameObject.FindObjectOfType<WinConditionManager>();

            if (win != null)
            {
                EventManager.SubscribeToEvent(EventsType.Event_EnemyDestroyed, win.OnEnemyDestroyed);
                EventManager.SubscribeToEvent(EventsType.Event_Victory, win.OnVictory);
                EventManager.SubscribeToEvent(EventsType.Event_Defeat, win.OnDefeat);
            }

        });
    }

    private void UnsubscribeEvents()
    {
        EventManager.UnsubscribeToEvent(EventsType.Event_Victory, ShowVictoryScreen);
        EventManager.UnsubscribeToEvent(EventsType.Event_Defeat, ShowDefeatScreen);
    }
}
