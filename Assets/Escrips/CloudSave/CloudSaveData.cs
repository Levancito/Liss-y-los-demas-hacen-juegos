using System.Collections.Generic;
using UnityEngine;
using Unity.Services.CloudSave;
//using Unity.Services.Authentication;
using Unity.Services.Core;
using System.Threading.Tasks;
//using Unity.VisualScripting;

public class CloudSaveData : MonoBehaviour
{
    public static CloudSaveData Instance { get; private set; }

    [SerializeField] public int number;
    [SerializeField] public SaveFile saveFile = new SaveFile();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

    private void Start()
    {
        MyUserAuth.Instance.OnAuthenticationComplete += AuthenticationCompleted;
        //SaveData(12345, "TestPlayer", 2, 10, 50, true, false, true, false, true, false);
    }

    private async void AuthenticationCompleted()
    {
        await Instance.LoadData();
    }

    [ContextMenu("Load Data")]
    public async Task LoadData()
    {
        Debug.Log("------------------------Cloud Loading Data------------------------");

        var playerData = await CloudSaveService.Instance.Data.Player.LoadAsync(new HashSet<string> { "MyNumber", "MyDataToSave" });

        if (playerData.TryGetValue("MyNumber", out var firstKey))
        {
            Debug.Log($"MyNumber value: {firstKey.Value.GetAs<string>()}");
            number = firstKey.Value.GetAs<int>();
        }

        if (playerData.TryGetValue("MyDataToSave", out var secondKey))
        {
            Debug.Log($"MyDataToSave value: {secondKey.Value.GetAs<string>()}");
            JsonUtility.FromJsonOverwrite(secondKey.Value.GetAs<string>(), saveFile);
        }
    }

    [ContextMenu("Save Data")]
    public async void SaveData()
    {
        Debug.Log("------------------------Cloud Saving Data------------------------");

        var playerData = new Dictionary<string, object> {
        { "MyNumber", number },
        { "MyDataToSave", JsonUtility.ToJson(saveFile) }};

        var result = await CloudSaveService.Instance.Data.Player.SaveAsync(playerData);
        Debug.Log($"Saved data {string.Join(',', playerData)}");

        Debug.Log("------------------------Cloud Saved Data------------------------");
    }

    private async void OnApplicationQuit()
    {
        await SaveData();
    }
  
    public async void DeleteData()
    {
        Debug.Log("------------------------Cloud Deleting Data------------------------");

        var keys = await CloudSaveService.Instance.Data.Player.ListAllKeysAsync();
        for (int i =0; i< keys.Count; i++)
        {
            Debug.Log(keys[i].Key);
            await CloudSaveService.Instance.Data.Player.DeleteAsync(keys[i].Key);
        }
        Debug.Log("------------------------Cloud Data Deleted------------------------");
    }

    #region old
    //private async void SaveData(int score, string playerName, int fuel, int coins, int bolts,
    //    bool upgrade1, bool upgrade2, bool upgrade3,bool custom1, bool custom2, bool custom3)
    //{
    //    var data = new Dictionary<string, object>
    //    {
    //        { "highScore", score },
    //        { "playerName", playerName },
    //        { "fuel", fuel },
    //        { "coins", coins },
    //        { "bolts", bolts },
    //        { "upgrade1", upgrade1 },
    //        { "upgrade2", upgrade2 },
    //        { "upgrade3", upgrade3 },
    //        { "customization1", custom1 },
    //        { "customization2", custom2 },
    //        { "customization3", custom3 }
    //    };

    //    try
    //    {
    //        await CloudSaveService.Instance.Data.Player.SaveAsync(data);
    //        Debug.Log("Data saved to the cloud");
    //    }
    //    catch (System.Exception ex)
    //    {
    //        Debug.LogError($"Failed to save data: {ex.Message}");
    //    }
    //}

    //private async void LoadData(string valueName)
    //{
    //    try
    //    {
    //        var savedData = await CloudSaveService.Instance.Data.Player.LoadAllAsync();

    //        if (savedData.TryGetValue(valueName, out var value))
    //        {
    //            Debug.Log($"{valueName}: {value.Value.GetAs<string>()}");
    //        }
    //    }
    //    catch(System.Exception ex)
    //    {
    //        Debug.LogError($"Failed to load data: {ex.Message}");
    //    }
    //}

    //private async void DeleteData()
    //{
    //    var keys = await CloudSaveService.Instance.Data.Player.ListAllKeysAsync();
    //    for(int i =0; i > keys.Count; i++)
    //    {
    //        Debug.Log(keys[i].Key);
    //        await CloudSaveService.Instance.Data.Player.DeleteAsync(keys[i].Key);
    //    }
    //}
    #endregion
}