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
        
        _pooledBullets = GetComponents<Bullet>();

        foreach (Bullet bullet in _pooledBullets)
        {
            
            bullet.damage += 10;
        }
    }

    public void UpgradeVelocity()
    {
        saveFile.upgrade2 = true;
        if (_Crontrol == null)
        {
            return;
        }

        _Crontrol._speed += 0.3f;
    }

    public void UpgradeLife()
    {
        saveFile.upgrade3 = true;
        if (_Stats == null)
        {
            return;
        }

        _Stats.MaxHP += 100;
        _Stats.Heal(100);
    }

    public float GetDamage() => _Bullet.damage;
    public float GetVelocity() => _Crontrol._speed;
    public float GetLife() => _Stats.MaxHP;
}
