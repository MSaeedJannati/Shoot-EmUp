using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ENUMS;

public class AmmoCart : ICollectable
{
    [SerializeField]WeaponType ammoType;
    public override void OnCollect(Inventory inv)
    {
        inv.AmmoPicked(amount, ammoType);
        gameObject.SetActive(false);
    }
}
