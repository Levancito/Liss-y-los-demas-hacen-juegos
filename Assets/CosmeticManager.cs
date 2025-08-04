using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CosmeticManager : MonoBehaviour
{
    //[SerializeField] private GameObject[] cosmetics;

    //public bool Cos1 = false;
    //public bool Cos2 = false;
    //public bool Cos3 = false;
    
    public static CosmeticManager Instance { get; private set; }
    public static SaveFile SaveFile { get; private set; }

    public Dictionary<string, bool> cosmeticsDictionary = new Dictionary<string, bool>() {
        { "cosmetic1", false },
        { "cosmetic2", false },
        { "cosmetic3", false } };

    public Dictionary<string, GameObject> cosmeticGameObjects = new Dictionary<string, GameObject>() {
        { "cosmetic1", null },
        { "cosmetic2", null },
        { "cosmetic3", null} };

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);

        if (SaveFile == null)
        {
            SaveFile = FindObjectOfType<CloudSaveData>().saveFile;
        }
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        FindCosmeticObjects();
        ApplyCosmetics();
    }

    private void Start()
    {
        cosmeticsDictionary["cosmetic1"] = SaveFile.customization1;
        cosmeticsDictionary["cosmetic2"] = SaveFile.customization2;
        cosmeticsDictionary["cosmetic3"] = SaveFile.customization3;
        //foreach (var cosmetic in cosmetics)
        //{
        //    if (cosmetic != null)
        //    {
        //        cosmetic.SetActive(false);
        //    }
        //}
    }

    public void UpdateCloud()
    {
        SaveFile.customization1 = cosmeticsDictionary["cosmetic1"];
        SaveFile.customization2 = cosmeticsDictionary["cosmetic2"];
        SaveFile.customization3 = cosmeticsDictionary["cosmetic3"];
    }
    public void ResetCosmetics()
    {
        cosmeticsDictionary["cosmetic1"] = false;
        cosmeticsDictionary["cosmetic2"] = false;
        cosmeticsDictionary["cosmetic3"] = false;

        SaveFile.customization1 = false;
        SaveFile.customization2 = false;
        SaveFile.customization3 = false;

        ApplyCosmetics(); 
    }
    public void FindCosmeticObjects()
    {
        for (int i = 0; i < 3; i++) 
        {
            string key = "cosmetic" + ((i + 1).ToString());
            //GameObject foundObject = GameObject.Find(key);
            //cosmeticGameObjects[key] = foundObject;
            cosmeticGameObjects[key] = GameObject.Find(key);
        }
    }

    public void ApplyCosmetics()
    {
        for (int i = 0; i < 3; i++)
        {
            string key = "cosmetic" + ((i + 1).ToString());
            bool value = cosmeticsDictionary[key];

            if (cosmeticGameObjects[key] != null)
            {
                cosmeticGameObjects[key].SetActive(value);
            }
            else
            {
                Debug.LogWarning($"No: {key}");
            }
        }
    }

    public void EnableCosmetic(int cosmetic)
    {
        string key = "cosmetic" + (cosmetic.ToString());
        cosmeticsDictionary[key] = true;
        UpdateCloud();
    }

}