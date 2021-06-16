using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using ENUMS;

public class PlayerHealthVIew : MonoBehaviour, IHealthViewer
{
    #region Variables
    [SerializeField] Image foreGroundImage;
    [SerializeField] TMP_Text healthTxt;
    [SerializeField] TMP_Text ammoText;
    [SerializeField] Button shootButton;
    [SerializeField] Button reloadBtn;
    [SerializeField] Image reloadImageFill;
    #endregion
    #region monobahaviour callbacks
    #endregion
    #region Funcitons
    public void SetHealthValue(float currentAmount, float maxAmount)
    {
        foreGroundImage.fillAmount = currentAmount / maxAmount;
        healthTxt.text = $"{currentAmount}/{maxAmount}";
    }
    public void SetAmmoValue(float currentAmount, float maxAmount, WeaponType type)
    {
       ammoText.text = $"{currentAmount}/{maxAmount}";
    }
    public void reload(int ammoInMag, int ammoInPool, IWeapon weapon)
    {
        StartCoroutine(reloadVisualCoroutine(ammoInMag, ammoInPool, weapon));
    }
    void ChangeButtonsApearenceForReload(bool reloadStarting)
    {
        shootButton.interactable = !reloadStarting;
        reloadBtn.interactable = !reloadStarting;
        reloadImageFill.gameObject.SetActive(reloadStarting);
    }
    #endregion
    #region Coroutines
    IEnumerator reloadVisualCoroutine(int ammoInMag, int ammoInPool, IWeapon weapon)
    {
        float t = Time.time;
        float endTime = t + weapon.ReloadTime;
        ChangeButtonsApearenceForReload(true);
        while (t < endTime)
        {

            reloadImageFill.fillAmount = (-endTime + t+ weapon.ReloadTime) / weapon.ReloadTime;
            yield return null;
            t += Time.deltaTime;
        }
        ChangeButtonsApearenceForReload(false);
    }
    #endregion

}
