using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CosmeticManager : MonoBehaviour
{
    [SerializeField] private GameObject[] cosmetics;

    private void Start()
    {
        foreach (var cosmetic in cosmetics)
        {
            if (cosmetic != null)
            {
                cosmetic.SetActive(false);
            }
        }
    }

    public void ActivateCosmetic(GameObject prefab)
    {
        foreach (var cosmetic in cosmetics)
        {
            if (cosmetic == prefab)
            {
                prefab.SetActive(true);
                return;
            }
        }

        Debug.LogWarning("El prefab dado no está en la lista de cosmetics.");
    }

    public void DeactivateCosmetic(GameObject prefab)
    {
        foreach (var cosmetic in cosmetics)
        {
            if (cosmetic == prefab)
            {
                prefab.SetActive(false);
                return;
            }
        }

        Debug.LogWarning("El prefab dado no está en la lista de cosmetics.");
    }
}

public class PrefabManager : MonoBehaviour
{
    [SerializeField] private GameObject[] prefabs;
    [SerializeField] public CosmeticManager cosmeticManager;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            SpawnRandomPrefab();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            ExecuteAnotherMethod();
        }
    }

    // Método para generar un prefab aleatorio.
    private void SpawnRandomPrefab()
    {
        if (prefabs.Length == 0)
        {
            Debug.LogWarning("No hay prefabs asignados en la lista.");
            return;
        }

        int randomIndex = Random.Range(0, prefabs.Length); // Obtiene un índice aleatorio.
        GameObject prefabToSpawn = prefabs[randomIndex];

        if (prefabToSpawn != null)
        {
            cosmeticManager.ActivateCosmetic(prefabToSpawn);
        }
        else
        {
            Debug.LogWarning($"El prefab en el índice {randomIndex} es nulo.");
        }
    }

    // Método a llamar cuando se presiona la tecla Q.
    private void ExecuteAnotherMethod()
    {
        if (prefabs.Length == 0)
        {
            Debug.LogWarning("No hay prefabs asignados en la lista.");
            return;
        }

        int randomIndex = Random.Range(0, prefabs.Length); // Obtiene un índice aleatorio.
        GameObject prefabToSpawn = prefabs[randomIndex];

        if (prefabToSpawn != null)
        {
            cosmeticManager.DeactivateCosmetic(prefabToSpawn);
        }
        else
        {
            Debug.LogWarning($"El prefab en el índice {randomIndex} es nulo.");
        }
    }
}
