using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthVIew : MonoBehaviour, IHealthViewer
{
    #region Variables
    [SerializeField] Image foreGroundImage;
    [SerializeField] TMPro.TMP_Text healthTxt;
    #endregion
    #region monobahaviour callbacks
    #endregion
    #region Funcitons
    public void SetHealthValue(float currentAmount, float maxAmount)
    {
        foreGroundImage.fillAmount = currentAmount / maxAmount;
        healthTxt.text = $"{currentAmount}/{maxAmount}";
    }
    #endregion

}
