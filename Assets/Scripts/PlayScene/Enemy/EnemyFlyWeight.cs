using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFlyWeight : MonoBehaviour
{
    #region Variables
    [SerializeField] GameObject bulletPrefab;
    static Transform targetTransform;
    static GameObject bullet;
    #endregion
    #region monobehaviour callback
    private void Awake()
    {
        if (bullet == null)
            bullet = bulletPrefab;
        if (targetTransform == null)
            targetTransform = PlayerManager.PlayerTransform;
    }
    #endregion
    #region Properties
    public static GameObject BulletPrefab => bullet;
    public static Transform TargetTransform => targetTransform;
    #endregion
}
