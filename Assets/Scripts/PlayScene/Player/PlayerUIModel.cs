using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUIModel : MonoBehaviour
{
    #region Variables
    [SerializeField]float maxHealth=100.0f;
    float currentHealth;
    #endregion
    #region Properties
    public float MaxHealth { get => maxHealth; set => maxHealth = value; }
    public float CurretnHealth { get => currentHealth; set => currentHealth = value; }

    #endregion
}
