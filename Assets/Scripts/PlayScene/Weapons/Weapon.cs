using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : IWeapon
{
    public override void shoot()
    {
        ObjectPool.Instantiate(bulletPrefab, muzzlePoint.position, muzzlePoint.rotation);
        lastShootTime = Time.time;
    }
    public override void reload(PlayerWeapon weapon)
    {

        int totalAmmo = weapon.AmmoInMag + weapon.AmmoInInv;
        totalAmmo -= ClipSize;
        weapon.setPlayerAmmoAfterReload(ClipSize, totalAmmo);
    }
    public override bool canShoot()
    {
        return Time.time > lastShootTime + RecoilTime;
    }
}
