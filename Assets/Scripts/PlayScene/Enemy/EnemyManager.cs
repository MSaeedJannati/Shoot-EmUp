using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.AI;

public class EnemyManager : MonoBehaviour
{
    #region Variables
    EnemyState state;
    [SerializeField] NavMeshAgent mAgent;
    #region common fields
    #endregion
    #endregion
    #region Monobehaciour callbacks
    private void Start()
    {

    }
    #endregion
    #region Functions
    void Initialize()
    {
        state = EnemyState.PRESUING;
      
    }

    #endregion
    #region Enums
    [Serializable]
    public enum EnemyState
    {
        PATROLING,
        PRESUING,
        FIGHTING,
        FLIEENG
    }
    #endregion
}
