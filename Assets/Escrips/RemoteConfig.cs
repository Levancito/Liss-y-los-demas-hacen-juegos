using System.Threading.Tasks;
using Unity.Services.RemoteConfig;
using Unity.Services.Authentication;
using Unity.Services.Core;
using UnityEngine;
using Builder;

public class RemoteConfig : MonoBehaviour
{
    public struct userAttributes { }
    public struct appAttributes { }

    public int BulletDamage = 5;// Hecho
    public int EnemyDamage = 10;// Hecho
    public int EnemyLife = 15;// Hecho
    public float EnemySpeed = 2;// Hecho
    public float BulletSpeed = 10;// Hecho
    public float SpawnIntervalCurrency = 2;
    public float SpawnIntervalNafta = 3;
    public float SpawnIntervalUpdate = 2;
    public float PlayerSpeed = 0.5f;// Hecho
    public float ShootInterval = 0.4f;// Hecho
    public string Comentario = "";// Hecho
    public string Update = "";

    public static RemoteConfig Instance;


    public int EnemyDamageBase = 10;
    async Task InitializeRemoteConfigAsync()
    {
        await UnityServices.InitializeAsync();

        if (!AuthenticationService.Instance.IsSignedIn)
        {
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
        }
    }

    async Task Start()
    {
     
        if (Utilities.CheckForInternetConnection())
        {
            await InitializeRemoteConfigAsync();
        }

        RemoteConfigService.Instance.FetchCompleted += ApplyRemoteSettings;
        RemoteConfigService.Instance.FetchConfigs(new userAttributes(), new appAttributes());
    }

    void ApplyRemoteSettings(ConfigResponse configResponse)
    {
        Debug.Log("RemoteConfigService.Instance.appConfig fetched: " + RemoteConfigService.Instance.appConfig.config.ToString());

        BulletDamage = RemoteConfigService.Instance.appConfig.GetInt("BulletDamage"); 
        EnemyDamage = RemoteConfigService.Instance.appConfig.GetInt("EnemyDamage");
        EnemyLife = RemoteConfigService.Instance.appConfig.GetInt("EnemyLife");
        EnemySpeed = RemoteConfigService.Instance.appConfig.GetFloat("EnemySpeed");
        SpawnIntervalCurrency = RemoteConfigService.Instance.appConfig.GetFloat("SpawnIntervalCurrency");
        SpawnIntervalNafta = RemoteConfigService.Instance.appConfig.GetFloat("SpawnIntervalNafta");
        SpawnIntervalUpdate = RemoteConfigService.Instance.appConfig.GetFloat("SpawnIntervalUpdate");
        BulletSpeed = RemoteConfigService.Instance.appConfig.GetFloat("BulletSpeed");
        PlayerSpeed = RemoteConfigService.Instance.appConfig.GetFloat("PlayerSpeed");
        ShootInterval = RemoteConfigService.Instance.appConfig.GetFloat("ShootInterval");
        Comentario = RemoteConfigService.Instance.appConfig.GetString("Comentario");
        Update = RemoteConfigService.Instance.appConfig.GetString("Update");
        NotifyControllers(); 
    }
    public int GetScaledEnemyDamage(float distanceCovered)
    {
        return Mathf.FloorToInt(EnemyDamageBase + distanceCovered / 10f);

    }
    private void NotifyControllers()
    {
        // Velicidad de Disparo
        P_ShootController[] shootControllers = FindObjectsOfType<P_ShootController>();
        foreach (var controller in shootControllers)
        {
            controller.UpdateShootInterval(ShootInterval);
        }
        // Velocidad del Jugador
        P_Crontrol[] playerControls = FindObjectsOfType<P_Crontrol>();
        foreach (var control in playerControls)
        {
            control.UpdateSpeed(PlayerSpeed);
        }
        // Bala Del Enemigo
        EnemyBullet[] enemyBullets = FindObjectsOfType<EnemyBullet>();
        foreach (var bullet in enemyBullets)
        {
            bullet.UpdateDamage(EnemyDamage);
        }
        //Vida Enemigo
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        foreach (var enemy in enemies)
        {
            enemy.UpdateMaxHealth(EnemyLife);
       
        }
        foreach (var enemy in enemies)
        {
            enemy.UpdateSpeed(EnemySpeed);

        }

        // Bala Del Jugador
        Bullet[] bullets = FindObjectsOfType<Bullet>();
        foreach (var bullet in bullets)
        {
            bullet.UpdateDamage(BulletDamage, BulletSpeed);
        }
        // Comentario del menu
        MenuManager[] menu = FindObjectsOfType<MenuManager>();
        foreach (var menuManager in menu)
        {
            menuManager.UpdateComentary(Comentario, Update);
            Debug.Log(Comentario);
        }
        // Configurar ResourceSpawner
        ResourceSpawner[] spawners = FindObjectsOfType<ResourceSpawner>();
        foreach (var spawner in spawners)
        {
            spawner.ConfigureFromRemote(SpawnIntervalCurrency, SpawnIntervalNafta);
        }
    }


}