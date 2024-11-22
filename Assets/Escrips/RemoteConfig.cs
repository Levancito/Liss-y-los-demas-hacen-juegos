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

    public int BulletDamage = 5;
    public int EnemyDamage = 10;
    public int EnemyLife = 15;
    public float PlayerSpeed = 0.5f;
    public float ShootInterval = 0.4f;
    public string Comentario = "";

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
        PlayerSpeed = RemoteConfigService.Instance.appConfig.GetFloat("PlayerSpeed");
        ShootInterval = RemoteConfigService.Instance.appConfig.GetFloat("ShootInterval");
        Comentario = RemoteConfigService.Instance.appConfig.GetString("Comentario");
        NotifyControllers(); 
    }

    private void NotifyControllers()
    {
        // Velicidad de Disparo - no anda
        P_ShootController[] shootControllers = FindObjectsOfType<P_ShootController>();
        foreach (var controller in shootControllers)
        {
            controller.UpdateShootInterval(ShootInterval);
        }
        // Velocidad del Jugador - no anda
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
        // Bala Del Jugador
        Bullet[] bullets = FindObjectsOfType<Bullet>();
        foreach (var bullet in bullets)
        {
            bullet.UpdateDamage(BulletDamage);
        }
        MenuManager[] menu = FindObjectsOfType<MenuManager>();
        foreach (var menuManager in menu)
        {
            menuManager.UpdateComentary(Comentario);
            Debug.Log(Comentario);
        }
    }
}