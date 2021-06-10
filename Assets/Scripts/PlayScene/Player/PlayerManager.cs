using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerManager : MonoBehaviour
{
    #region Variables
    public static Action update;
    [SerializeField] Transform mTransform;
    static Transform playerTransform;
    #endregion
    #region Properties
    public static Transform PlayerTransform => playerTransform;
    #endregion
    #region Monobehaviour callbacks
    private void OnEnable()
    {
        playerTransform = mTransform;
    }
    private void Update()
    {
        update?.Invoke();
    }
    #endregion
    #region Functions
    #endregion
}
