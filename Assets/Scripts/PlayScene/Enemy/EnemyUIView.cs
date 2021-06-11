using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnemyUIView : MonoBehaviour,IHealthViewer
{
    #region Variables
    [SerializeField] Image foreGroundImage;
    #endregion
    #region Monobehaviour callbacks
    #endregion
    #region Functions
    public void SetHealthValue(float currentAmount, float maxAmount)
    {
        foreGroundImage.fillAmount = (currentAmount / maxAmount);
    }

    #endregion
}
