using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    #region Variables

    [SerializeField] Transform mTransform;
    [SerializeField] Rigidbody mRigidBody;
    [SerializeReference] float lifeTime = 3.0f;
    float velocity = 20.0f;
    WaitForSeconds delay;
    [SerializeField] float damage = 20.0f;
    [SerializeField] bool canDammageEnemies;
    #endregion
    #region monobehaviour callbacks
    private void OnEnable()
    {
        if (delay == null)
        {
            delay = new WaitForSeconds(lifeTime);
        }
        mRigidBody.velocity = mTransform.forward * velocity;
        StartCoroutine(disableAfterLifeTime());
    }
    private void OnDisable()
    {
        mRigidBody.Sleep();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (!gameObject.activeInHierarchy)
            return;
        if (collision.collider.gameObject.TryGetComponent<IDammageable>(out var dammageAble))
        {
            bool shouldApply = false;
            if (collision.collider.gameObject.tag != "Player")
            {
                if (canDammageEnemies)
                {

                    shouldApply = true;

                }
            }
            else
            {
                shouldApply = true;


            }
            if (shouldApply)
                dammageAble.OnDammaged(damage);
        }
        gameObject.SetActive(false);
    }
    #endregion
    #region Functions
    #endregion
    #region Coroutines
    IEnumerator disableAfterLifeTime()
    {
        yield return delay;
        gameObject.SetActive(false);
    }
    #endregion
}
