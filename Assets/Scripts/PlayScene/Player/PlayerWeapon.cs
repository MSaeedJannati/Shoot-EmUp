using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ENUMS;
using System;

public class PlayerWeapon : MonoBehaviour
{
    #region Variables
    [SerializeField] IWeapon currentWaepon;
    Dictionary<WeaponType, int> playerAmmo;
    int ammoInMagazine;
    int ammoInInventory;
    public static event Action<int, int, WeaponType> OnShoot;
    public static event Action<int, int, IWeapon> OnReload;
    public static event Action<int, int, WeaponType> OnAmmoChange;

    bool isReloading;
    #endregion
    #region Properties
    public int AmmoInMag => ammoInMagazine;
    public int AmmoInInv => ammoInInventory;
    #endregion
    #region Monobehaviour callBacks
    private void OnEnable()
    {
        Inventory.OnAmmoPicked += AmmoPicked;
    }
    private void OnDisable()
    {
        Inventory.OnAmmoPicked -= AmmoPicked;
    }
    private void Awake()
    {
        playerAmmo = new Dictionary<WeaponType, int>();
        playerAmmo.Add(currentWaepon.Type, 200);
        calcAmmo();
        currentWaepon.reload(this);
        setAmmoIndict();
       
    }
    private void Start()
    {
        OnAmmoChange?.Invoke(ammoInMagazine, ammoInInventory, currentWaepon.Type);
    }
    public void reload()
    {
        StartCoroutine(reloadCoroutine());
        OnReload?.Invoke(ammoInMagazine, ammoInInventory, currentWaepon);
    }
    void calcAmmo()
    {
        ammoInInventory = playerAmmo[currentWaepon.Type];
    }
    void setAmmoIndict()
    {
        playerAmmo[currentWaepon.Type] = ammoInInventory;
    }
    public void shoot()
    {
        if (ammoInMagazine == 0)
            return;
        if (!currentWaepon.canShoot())
            return;
        currentWaepon.shoot();
        ammoInMagazine--;
        OnShoot?.Invoke(ammoInMagazine, ammoInInventory, currentWaepon.Type);


    }
    public void setPlayerAmmoAfterReload(int magAmmo, int poolAmmo)
    {
        ammoInMagazine = magAmmo;
        ammoInInventory = poolAmmo;
    }
    public void AmmoPicked(int ammo, WeaponType type)
    {
        if (playerAmmo.ContainsKey(type))
        {
            playerAmmo[type] += ammo;
        }
        else
        {
            playerAmmo.Add(type, ammo);
        }
        if (type == currentWaepon.Type)
        {
            calcAmmo();
            OnAmmoChange?.Invoke(ammoInMagazine, ammoInInventory, currentWaepon.Type);
        }
    }
    #endregion
    #region Functions
    #endregion
    #region coroutines
    IEnumerator reloadCoroutine()
    {
        yield return new WaitForSeconds(currentWaepon.ReloadTime);
        calcAmmo();
        currentWaepon.reload(this);
        setAmmoIndict();
        OnAmmoChange?.Invoke(ammoInMagazine, ammoInInventory, currentWaepon.Type);
    }
    #endregion
}
