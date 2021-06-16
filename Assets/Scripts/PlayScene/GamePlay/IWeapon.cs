using UnityEngine;
using  ENUMS;

public abstract class IWeapon:MonoBehaviour
{
    #region Variables
    [SerializeField] int clipSize;
    [SerializeField] float reloadTime;
    [SerializeField] float recoilTime;
    [SerializeField] float damageMultiplier;
    [SerializeField] WeaponType type;
    [SerializeField] protected Transform muzzlePoint;
    [SerializeField] protected GameObject bulletPrefab;
   protected float lastShootTime;
    #endregion
    #region Properties
    public int ClipSize { get => clipSize; set => clipSize = value; }
    public float ReloadTime { get => reloadTime; set => reloadTime = value; }
    public float RecoilTime { get => recoilTime; set => recoilTime = value; }
    public float DamageMultiplier { get => damageMultiplier; set => damageMultiplier = value; }
    public WeaponType Type { get => type; set => type = value; }
    public abstract void shoot();
    public abstract void reload(PlayerWeapon weapon);
    public abstract  bool canShoot();
    #endregion

}
