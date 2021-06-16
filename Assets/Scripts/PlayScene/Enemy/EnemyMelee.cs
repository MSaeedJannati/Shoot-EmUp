using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMelee : MonoBehaviour, IDammageable
{
    #region Variables
    [SerializeField] EnemyManager.EnemyState state;
    [SerializeField] NavMeshAgent mAgent;
    [SerializeField] Transform mTransform;
    [SerializeField] float speed;
    [SerializeField] float minDistanceToFight = .5f;
    [SerializeField] Animator mAnimator;
    [SerializeField] float damage;
    float attackCooldown = 2.0f;
    float lastAttackTIme;
    [SerializeField] float maxHealth = 100.0f;
    float currentHealth;
    IHealthViewer mHealthViewer;

    WaitForSeconds delay = new WaitForSeconds(.1f);
    int attackHash = Animator.StringToHash("Attack");
   static IDammageable playerDammageAble;
    #region common fields
    #endregion
    #endregion
    #region Monobehaciour callbacks

    private void OnEnable()
    {
        Initialize();
        StartCoroutine(agentAiCouroutine());
        TryGetComponent(out mHealthViewer);
        currentHealth = maxHealth;
        mHealthViewer?.SetHealthValue(currentHealth, maxHealth);
    }
    #endregion
    #region Functions
    void Initialize()
    {
        state = EnemyManager.EnemyState.PRESUING;
        mAgent.speed = speed;
        mAgent.stoppingDistance = minDistanceToFight;
        lastAttackTIme = Time.time - attackCooldown;
    }
    float calcSqrDistance()
    {
        return (mTransform.position - PlayerManager.PlayerTransform.position).sqrMagnitude;
    }
    public void shootAtPlayer()
    {
        if (Time.time > lastAttackTIme + attackCooldown)
        {
            mAnimator.SetTrigger(attackHash);
            lastAttackTIme = Time.time;
        }
    }

    public void DammagePlayer()
    {
        if (playerDammageAble == null)
        {
            PlayerManager.PlayerTransform.TryGetComponent(out playerDammageAble);
        }
        if (playerDammageAble != null)
        {
            playerDammageAble.OnDammaged(damage);
        }
    }
    public void OnDammaged(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            OnDie();
        }
        mHealthViewer?.SetHealthValue(currentHealth, maxHealth);

    }
    public void OnDie()
    {
        gameObject.SetActive(false);
    }
    #endregion
    #region Coroutines

    IEnumerator agentAiCouroutine()
    {
        yield return null;
        while (true)
        {
            yield return EnemyFlyWeight.DelayBetweenAIStateCheck;
            if (state == EnemyManager.EnemyState.PRESUING)
            {
                if (calcSqrDistance() > minDistanceToFight * minDistanceToFight)
                    mAgent.SetDestination(PlayerManager.PlayerTransform.position);

                else
                    state = EnemyManager.EnemyState.FIGHTING;
            }
            else if (state == EnemyManager.EnemyState.FIGHTING)
            {
                if (calcSqrDistance() > minDistanceToFight * minDistanceToFight)
                {
                    state = EnemyManager.EnemyState.PRESUING;
                    mAgent.stoppingDistance = minDistanceToFight;
                }
                else
                {
                    shootAtPlayer();
                }
            }
        }
    }

    #endregion
    #region coroutines
    #endregion
}
