using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ResourceSpawner : MonoBehaviour
{
    [Header("Currency Settings")]
    [SerializeField] private GameObject currencyPrefab;
    [SerializeField] private float currencySpawnRate = 2f; 
    [SerializeField] private int currencySpawnCount = 3;   

    [Header("Nafta Settings")]
    [SerializeField] private GameObject naftaPrefab;
    [SerializeField] private float naftaSpawnRate = 3f;    
    [SerializeField] private int naftaSpawnCount = 2;      

    [Header("Tuercas Settings")]
    [SerializeField] private GameObject tuercasPrefab;

    public GameObject zPosition;

    private void Start()
    {
        InvokeRepeating(nameof(SpawnCurrency), 0f, currencySpawnRate);
        InvokeRepeating(nameof(SpawnNafta), 0f, naftaSpawnRate);
    }

    private void SpawnCurrency()
    {
        for (int i = 0; i < currencySpawnCount; i++)
        {
            Vector3 randomPosition = GetRandomPosition();
            Instantiate(currencyPrefab, randomPosition, Quaternion.identity);
            Debug.Log($"Spawneado Currency #{i + 1} en: {randomPosition}");
        }
    }

    private void SpawnNafta()
    {
        for (int i = 0; i < naftaSpawnCount; i++)
        {
            Vector3 randomPosition = GetRandomPosition();
            Instantiate(naftaPrefab, randomPosition, Quaternion.Euler(0, 0, Random.Range(0, 360)));
            Debug.Log($"Spawneado Nafta #{i + 1} en: {randomPosition}");
        }
    }

    public void SpawnTuerca(Transform nave)
    {
        Instantiate(tuercasPrefab, nave.position, Quaternion.identity);
    }

    private Vector3 GetRandomPosition()
    {
        float x = Random.Range(-40f, 40f);
        return new Vector3(x, 0f, zPosition.transform.position.z);
    }
}
