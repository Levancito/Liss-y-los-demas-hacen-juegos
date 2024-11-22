using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    [SerializeField] private Bullet _Bullet;
    [SerializeField] private Stats _Stats;
    [SerializeField] private P_Crontrol _Crontrol;
    //[SerializeField] private float damageMultiplier = 0.2f;
    //[SerializeField] private float velocityMultiplier = 0.2f;
    //[SerializeField] private float lifeMultiplier = 0.2f;

    public void UpgradeDamage(int amount)
    {
        Bullet bullet = FindObjectOfType<Bullet>();
        if (_Bullet == null)
        {
            Debug.LogError("No se encontró una referencia al script Bullet.");
            return;
        }

        _Bullet.damage = _Bullet.damage * amount;
        var Dmg = _Bullet.damage;
        Debug.Log($"Daño mejorado. Nuevo valor: {Dmg}");

    }

    public void UpgradeVelocity(int amount)
    {
        if (_Crontrol == null)
        {
            Debug.LogError("No se encontró una referencia al script Bullet.");
            return;
        }

        _Crontrol._speed = _Crontrol._speed * amount;
        var Sp = _Crontrol._speed;
        Debug.Log($"Daño mejorado. Nuevo valor: {Sp}");
    }

    public void UpgradeLife(int amount)
    {
        if (_Stats == null)
        {
            Debug.LogError("No se encontró una referencia al script Bullet.");
            return;
        }

        _Stats.MaxHP = _Stats.MaxHP * amount;
        var HP = _Stats.MaxHP;
        Debug.Log($"Daño mejorado. Nuevo valor: {HP}");
    }

    public float GetDamage() => _Bullet.damage;
    public float GetVelocity() => _Crontrol._speed;
    public float GetLife() => _Stats.MaxHP;
}
