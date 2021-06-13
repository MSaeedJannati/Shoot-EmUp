using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUiPresenter : MonoBehaviour, IDammageable
{
    #region Variabels
    PlayerHealthVIew uiView;
    PlayerUIModel uiModel;

    #endregion
    #region monobahaviour callbacks
    private void OnEnable()
    {
        TryGetComponent(out uiView);
        TryGetComponent(out uiModel);
        uiModel.CurretnHealth = uiModel.MaxHealth;
        uiView.SetHealthValue(uiModel.CurretnHealth, uiModel.MaxHealth);
    }
    #endregion
    #region Functions
    #endregion
    public void OnDammaged(float damage)
    {
        uiModel.CurretnHealth -= damage;
        if (uiModel.CurretnHealth <= 0)
            OnDie();
        uiView.SetHealthValue(uiModel.CurretnHealth, uiModel.MaxHealth);
    }

    public void OnDie()
    {
        gameObject.SetActive(false);
    }
}
