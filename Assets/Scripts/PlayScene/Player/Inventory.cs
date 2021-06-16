using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using ENUMS;
public class Inventory : MonoBehaviour
{
    #region Variables
    int gold;
    PlayerWeapon weaponReference;
    public static event Action<int> OnGoldChange;
    public static event Action<int> OnAidPicked;
    public static event Action<int, WeaponType> OnAmmoPicked;
    #endregion
    #region Properties
    public int Gold => gold;
    #endregion
    #region MonobehaviourCallBacks
    private void OnEnable()
    {
        TryGetComponent(out weaponReference);
    }
    #endregion
    #region Functions
    public void changeGOld(int amount)
    {
        gold += amount;
        if (gold < 0)
            gold = 0;
        OnGoldChange?.Invoke(gold);
    }
    public void FirstAidAbsorbed(int amount)
    {
        OnAidPicked?.Invoke(amount);


    }
    public void AmmoPicked(int amount,WeaponType type)
    {
        OnAmmoPicked?.Invoke(amount,type);
    }
    #endregion
}
