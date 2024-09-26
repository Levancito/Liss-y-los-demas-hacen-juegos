
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

    public float BulletDamage = 5;
    public int EnemyDamage = 10;
    public float EnemyLife = 50;
    public float PlayerSpeed = 0.5f;
    public float ShootInterval = 0.4f;

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

        BulletDamage = RemoteConfigService.Instance.appConfig.GetFloat("BulletDamage");
        EnemyDamage = RemoteConfigService.Instance.appConfig.GetInt("EnemyDamage");
        EnemyLife = RemoteConfigService.Instance.appConfig.GetFloat("EnemyLife");
        PlayerSpeed = RemoteConfigService.Instance.appConfig.GetFloat("PlayerSpeed");
        ShootInterval = RemoteConfigService.Instance.appConfig.GetFloat("ShootInterval");
        NotifyControllers(); 
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
        // Bala Del Jugador
        Bullet[] bullets = FindObjectsOfType<Bullet>();
        foreach (var bullet in bullets)
        {
            bullet.UpdateDamage(BulletDamage);
        }
    }
}