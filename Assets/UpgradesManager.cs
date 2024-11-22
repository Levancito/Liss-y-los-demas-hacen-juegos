using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    [SerializeField] private Bullet _Bullet;
    [SerializeField] private Stats _Stats;
    [SerializeField] private P_Crontrol _Crontrol;

    public SaveFile saveFile;

    public Bullet[] _pooledBullets;

    [ContextMenu("UpgradeDamage")]
    [ContextMenu("UpgradeVelocity")]
    [ContextMenu("UpgradeLife")]

    //[SerializeField] private float damageMultiplier = 0.2f;
    //[SerializeField] private float velocityMultiplier = 0.2f;
    //[SerializeField] private float lifeMultiplier = 0.2f;

    private void Awake()
    {
        saveFile = FindObjectOfType<CloudSaveData>().saveFile;
    }

    private void Start()
    {
        if (saveFile.upgrade1)
        {
            UpgradeDamage();
        }
        if (saveFile.upgrade2)
        {
            UpgradeVelocity();
        }
        if (saveFile.upgrade3)
        {
            UpgradeLife();
        }
    }

    public void UpgradeDamage()
    {
        saveFile.upgrade1 = true;
        //Bullet bullet = FindObjectOfType<Bullet>();
        //if (_Bullet != null)
        //{
        //Debug.LogError("No se encontró una referencia al script Bullet.");
        //return;
        //}
        _pooledBullets = GetComponents<Bullet>();
        //_Bullet.damage = _Bullet.damage * amount;

        foreach (Bullet bullet in _pooledBullets)
        {
            //var Dmg = _Bullet.damage;
            //bullet.damage = (int)(((float)bullet.damage) * 1.2f);
            bullet.damage += 10;
            Debug.Log($"Daño mejorado. Nuevo valor: {bullet.damage}");
        }
    }

    public void UpgradeVelocity()
    {
        saveFile.upgrade2 = true;
        if (_Crontrol == null)
        {
            Debug.LogError("No se encontró una referencia al script Control.");
            return;
        }

        _Crontrol._speed += 0.3f;
        Debug.Log($"Velocidad mejorada, nuevo valor: {_Crontrol._speed}");
    }

    public void UpgradeLife()
    {
        saveFile.upgrade3 = true;
        if (_Stats == null)
        {
            Debug.LogError("No se encontró una referencia al script Stats.");
            return;
        }

        _Stats.MaxHP += 100;
        _Stats.Heal(100);
        Debug.Log($"Vida mejorada, nuevo valor: {_Stats.MaxHP}");
    }

    public float GetDamage() => _Bullet.damage;
    public float GetVelocity() => _Crontrol._speed;
    public float GetLife() => _Stats.MaxHP;
}
