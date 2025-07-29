using System.Collections.Generic;
using UnityEngine;
using Unity.Services.CloudSave;
//using Unity.Services.Authentication;
using Unity.Services.Core;
using System.Threading.Tasks;
//using Unity.VisualScripting;

using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Unity.Services.CloudSave;
using Unity.Services.Core;
using Unity.Services.Authentication;

public class CloudSaveData : MonoBehaviour
{
    public static CloudSaveData Instance { get; private set; }

    [SerializeField] public int number;
    [SerializeField] public SaveFile saveFile = new SaveFile();

    private async void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            await InitializeUnityServices(); 
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        MyUserAuth.Instance.OnAuthenticationComplete += AuthenticationCompleted;
    }

    private async void AuthenticationCompleted()
    {
        await LoadData();
    }

    private async Task InitializeUnityServices()
    {
        try
        {
            if (UnityServices.State != ServicesInitializationState.Initialized)
            {
                await UnityServices.InitializeAsync();
                Debug.Log("Unity Services Initialized.");
            }

            if (!AuthenticationService.Instance.IsSignedIn)
            {
                await AuthenticationService.Instance.SignInAnonymouslyAsync();
                Debug.Log("Signed in anonymously.");
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError("Unity Services Initialization Error: " + e.Message);
        }
    }

    [ContextMenu("Load Data")]
    public async Task LoadData()
    {
        Debug.Log("------------------------Cloud Loading Data------------------------");

        try
        {
            var playerData = await CloudSaveService.Instance.Data.Player.LoadAsync(new HashSet<string> { "MyNumber", "MyDataToSave" });

            if (playerData.TryGetValue("MyNumber", out var firstKey))
            {
                number = firstKey.Value.GetAs<int>();
                Debug.Log($"MyNumber value: {number}");
            }

            if (playerData.TryGetValue("MyDataToSave", out var secondKey))
            {
                Debug.Log($"MyDataToSave value: {secondKey.Value.GetAs<string>()}");
                JsonUtility.FromJsonOverwrite(secondKey.Value.GetAs<string>(), saveFile);
            }
        }
        catch (System.Exception ex)
        {
            Debug.LogError("Error loading data: " + ex.Message);
        }
    }

    [ContextMenu("Save Data")]
    public async Task SaveData()
    {
        Debug.Log("------------------------Cloud Saving Data------------------------");

        try
        {
            var playerData = new Dictionary<string, object> {
                { "MyNumber", number },
                { "MyDataToSave", JsonUtility.ToJson(saveFile) }
            };

            await CloudSaveService.Instance.Data.Player.SaveAsync(playerData);
            Debug.Log($"Saved data: {string.Join(", ", playerData)}");
        }
        catch (System.Exception ex)
        {
            Debug.LogError("Error saving data: " + ex.Message);
        }
    }

    private async void OnApplicationQuit()
    {
        await SaveData();
    }

    public async void Save()
    {
        await SaveData();
    }

    public async void DeleteData()
    {
        Debug.Log("------------------------Cloud Deleting Data------------------------");

        try
        {
            var keys = await CloudSaveService.Instance.Data.Player.ListAllKeysAsync();

            foreach (var key in keys)
            {
                Debug.Log("Deleting key: " + key.Key);
                await CloudSaveService.Instance.Data.Player.DeleteAsync(key.Key);
            }

            Debug.Log("------------------------Cloud Data Deleted------------------------");
        }
        catch (System.Exception ex)
        {
            Debug.LogError("Error deleting data: " + ex.Message);
        }
    }
}
