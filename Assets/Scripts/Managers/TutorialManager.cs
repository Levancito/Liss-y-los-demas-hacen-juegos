using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject tutorialUI;
    [SerializeField] private TMP_Text tutorialText;
    [SerializeField] private Button nextButton;
    [SerializeField] private Button finishButton;
    [SerializeField] private GameObject joystickUI;

    [Header("Spawns")]
    [SerializeField] private GameObject enemy1Prefab;
    [SerializeField] private GameObject enemy2Prefab;
    [SerializeField] private GameObject itemTuerca;
    [SerializeField] private GameObject itemNafta;
    [SerializeField] private GameObject itemCoin;
    [SerializeField] private GameObject itemMejora;

    [Header("Player Control")]
    [SerializeField] private P_Crontrol movimientoScript;
    [SerializeField] private P_ShootController disparoScript;

    [Header("Spawn Point")]
    [SerializeField] private Transform spawnPoint;


    private int tutorialStep = 0;
    private GameObject currentSpawned;
    private Coroutine autoDestroyCoroutine;

    private readonly string[] tutorialSteps = new string[]
    {
        "¡Bienvenido piloto!\nHaz clic para continuar.",
        "", // ← Este se modifica dinámicamente según el tipo de control
        "¡Y mientras te mueves también disparas!",
        "Ese enemigo no dispara, pero hace daño al tocarte.",
        "Este enemigo sí dispara, ¡ten cuidado!",
        "¿Ves esa tuerca? Sirve para curarte y mejorarte.",
        "Eso es Nafta. Te permite volver a jugar.",
        "Esta moneda es valiosa. Sirve para mejoras y cosméticos.",
        "La mejora te da tres disparos temporales.",
        "Eso es todo, ahora te veré volar...\n¡Nos veremos, Space Cowboy!"
    };

    void Start()
    {
        movimientoScript.enabled = false;
        disparoScript.enabled = false;
        joystickUI.SetActive(false);
        finishButton.gameObject.SetActive(false);
        nextButton.onClick.AddListener(OnNextButtonPressed);
        finishButton.onClick.AddListener(OnFinishButtonPressed);

        tutorialUI.SetActive(true);
        ShowStep();
    }

    void ShowStep()
    {
        if (tutorialStep == 1)
        {
            string controlText = movimientoScript.tipoDeControl == P_Crontrol.ControlType.Joystick
                ? "Con el joystick de abajo puedes moverte."
                : "Inclina tu dispositivo para moverte.";
            tutorialText.text = controlText;
        }
        else
        {
            tutorialText.text = tutorialSteps[tutorialStep];
        }

        nextButton.gameObject.SetActive(true);
        finishButton.gameObject.SetActive(false);

        if (currentSpawned != null)
        {
            Destroy(currentSpawned);
            currentSpawned = null;
        }

        if (autoDestroyCoroutine != null)
        {
            StopCoroutine(autoDestroyCoroutine);
            autoDestroyCoroutine = null;
        }

        switch (tutorialStep)
        {
            case 0:
                Time.timeScale = 0f;
                joystickUI.SetActive(false);
                movimientoScript.enabled = false;
                disparoScript.enabled = false;
                break;

            case 1:
                joystickUI.SetActive(true);
                movimientoScript.enabled = true;
                disparoScript.enabled = false;
                Time.timeScale = 1f;
                break;

            case 2:
                disparoScript.enabled = true;
                Time.timeScale = 1f;
                break;

            case 3:
                SpawnInteractable(enemy1Prefab);
                break;

            case 4:
                SpawnInteractable(enemy2Prefab);
                break;

            case 5:
                SpawnInteractable(itemTuerca);
                break;

            case 6:
                SpawnInteractable(itemNafta);
                break;

            case 7:
                SpawnInteractable(itemCoin);
                break;

            case 8:
                SpawnInteractable(itemMejora);
                break;

            case 9:
                Time.timeScale = 0f;
                joystickUI.SetActive(false);
                movimientoScript.enabled = false;
                disparoScript.enabled = false;
                nextButton.gameObject.SetActive(false);
                finishButton.gameObject.SetActive(true);
                break;
        }
    }

    void SpawnInteractable(GameObject prefab)
    {
        currentSpawned = Instantiate(prefab, spawnPoint.position, Quaternion.identity);

        if (currentSpawned.GetComponent<InteractableObject>() == null)
        {
            currentSpawned.AddComponent<InteractableObject>().Initialize(this);
        }

        Time.timeScale = 1f;
        autoDestroyCoroutine = StartCoroutine(AutoDestroyAfterDelay(5f));
    }

    IEnumerator AutoDestroyAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (currentSpawned != null)
        {
            Destroy(currentSpawned);
            currentSpawned = null;
        }
    }

    public void OnNextButtonPressed()
    {
        Time.timeScale = 0f;

        if (autoDestroyCoroutine != null)
        {
            StopCoroutine(autoDestroyCoroutine);
            autoDestroyCoroutine = null;
        }

        AdvanceStep();
    }

    public void OnInteractableTouched()
    {
        if (autoDestroyCoroutine != null)
        {
            StopCoroutine(autoDestroyCoroutine);
            autoDestroyCoroutine = null;
        }

        if (currentSpawned != null)
        {
            Destroy(currentSpawned);
            currentSpawned = null;
        }
    }

    void AdvanceStep()
    {
        tutorialStep++;

        if (tutorialStep >= tutorialSteps.Length)
        {
            FinishTutorial();
            return;
        }

        ShowStep();
    }

    void OnFinishButtonPressed()
    {
        FinishTutorial();
    }

    void FinishTutorial()
    {
        Time.timeScale = 1f;
        tutorialUI.SetActive(false);
        joystickUI.SetActive(false);
        // SceneManager.LoadScene("JuegoPrincipal");
    }

    private class InteractableObject : MonoBehaviour
    {
        private TutorialManager tutorialManager;
        private bool touched = false;

        public void Initialize(TutorialManager manager)
        {
            tutorialManager = manager;

            var col = GetComponent<Collider>();
            if (col == null)
            {
                col = gameObject.AddComponent<BoxCollider>();
                col.isTrigger = true;
            }
            else
            {
                col.isTrigger = true;
            }

            var rb = GetComponent<Rigidbody>();
            if (rb == null)
            {
                rb = gameObject.AddComponent<Rigidbody>();
                rb.isKinematic = true;
            }
            else
            {
                rb.isKinematic = true;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (touched) return;
            if (other.CompareTag("Player"))
            {
                touched = true;
                tutorialManager.OnInteractableTouched();
                Destroy(gameObject);
            }
        }
    }
}

