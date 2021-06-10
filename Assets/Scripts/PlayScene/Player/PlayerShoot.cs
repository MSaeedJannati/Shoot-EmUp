 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    #region Variables
    [SerializeField] Transform muzlleTransform;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] bool isMouseDown;
    [SerializeField] float recoilTime = .2f;
    float lastShootTime;
    #endregion
    #region monobehaviour callbacks
    private void Start()
    {
        lastShootTime = Time.time - recoilTime;
    }
    #endregion
    #region functions
   
    public void OnPointerDown()
    {
        isMouseDown = true;
        StartCoroutine(continousShootCoroutine());
    }
    public void OnPointerUp()
    {
        isMouseDown = false;
    }
    public void OnPointerExit()
    {
        isMouseDown = false;
    }
    public void ContinuesShoot()
    {
        if (!isMouseDown)
            return;
        if (Time.time < lastShootTime + recoilTime)
            return;
        lastShootTime = Time.time;
        CreateBullet();
    }

    public void CreateBullet()
    {
        ObjectPool.Instantiate(bulletPrefab, muzlleTransform.position, muzlleTransform.rotation);
    }
    #endregion
    #region coroutines
    IEnumerator continousShootCoroutine()
    {
        while (isMouseDown)
        {
            ContinuesShoot();
            yield return null;
        }
    }
    #endregion
}
