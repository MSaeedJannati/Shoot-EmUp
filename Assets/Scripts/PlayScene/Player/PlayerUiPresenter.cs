using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ENUMS;

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
        PlayerWeapon.OnReload += OnRelaod;
        PlayerWeapon.OnShoot += OnShoot;
        PlayerWeapon.OnAmmoChange += UpdateAmmoView;
        Inventory.OnAidPicked += OnHealed;
    }
    private void OnDisable()
    {
        PlayerWeapon.OnReload -= OnRelaod;
        PlayerWeapon.OnShoot -= OnShoot;
        PlayerWeapon.OnAmmoChange -= UpdateAmmoView;
        Inventory.OnAidPicked -= OnHealed;
    }

    #endregion
    #region Functions
    #endregion
    public void OnHealed(int aid)
    {
        uiModel.CurretnHealth += aid;
        if (uiModel.CurretnHealth > uiModel.MaxHealth)
            uiModel.CurretnHealth = uiModel.MaxHealth;
        uiView.SetHealthValue(uiModel.CurretnHealth, uiModel.MaxHealth);
    }
    public void OnDammaged(float damage)
    {
        uiModel.CurretnHealth -= damage;
        if (uiModel.CurretnHealth <= 0)
            OnDie();
        uiView.SetHealthValue(uiModel.CurretnHealth, uiModel.MaxHealth);
    }
    public void OnShoot(int ammoInMag,int ammoInPool,WeaponType type)
    {
        uiView.SetAmmoValue(ammoInMag, ammoInPool, type);
    }
    public void UpdateAmmoView(int ammoInMag, int ammoInPool, WeaponType type)
    {
        uiView.SetAmmoValue(ammoInMag, ammoInPool, type);
    }
    public void OnRelaod(int ammoInMag, int ammoInPool, IWeapon type)
    {
        uiView.reload(ammoInMag, ammoInPool, type);
    }
    public void OnDie()
    {
        gameObject.SetActive(false);
    }
}
