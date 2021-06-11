using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFlyWeight : MonoBehaviour
{
    #region Variables
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] float delayBetweenAIStateCheckInSec = .5f;
    static Transform targetTransform;
    static GameObject bullet;
    static WaitForSeconds delayBetweenAIStateCheck;
    #endregion
    #region monobehaviour callback
    private void Awake()
    {
        if (bullet == null)
            bullet = bulletPrefab;
        if (targetTransform == null)
            targetTransform = PlayerManager.PlayerTransform;
        if (delayBetweenAIStateCheck == null)
            delayBetweenAIStateCheck = new WaitForSeconds(delayBetweenAIStateCheckInSec);
    }
    #endregion
    #region Properties
    public static GameObject BulletPrefab => bullet;
    public static Transform TargetTransform => targetTransform;
    public static WaitForSeconds DelayBetweenAIStateCheck => delayBetweenAIStateCheck;
    #endregion
}
