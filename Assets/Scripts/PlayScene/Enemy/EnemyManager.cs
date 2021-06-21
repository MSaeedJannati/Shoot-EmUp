using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.AI;
using BaseEnemyStates;

public class EnemyManager : MonoBehaviour, IDammageable
{
    #region Variables
    [SerializeField] EnemyState state;
    [SerializeField] NavMeshAgent mAgent;
    [SerializeField] Transform mTransform;
    [SerializeField] Transform muzzleTransform;
    [SerializeField] float speed;
    [SerializeField] float minDistanceToFight = 5.0f;
    [SerializeField] float distanceToStartPresueAgain = 10.0f;
    float reloadTime = 2.0f;
    float lastShootTime;
    [SerializeField] float maxHealth=100.0f;
    float currentHealth;
    IHealthViewer mHealthViewer;
    bool isRotatingTowardPlayer;
    bool isChangingPos;
    float lastMovedTime=0.0f;
    [SerializeField] float changePosPeriod = 10.0f;

    WaitForSeconds delay = new WaitForSeconds(.1f);
    State currentState;



    #region common fields
    #region States
   

    #endregion
    #endregion
    #region Properties
    public float MinDistanceToFight => minDistanceToFight;
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
    void CreateStates()
    {
        
    }
    void Initialize()
    {
        state = EnemyState.PRESUING;
        mAgent.speed = speed;
        mAgent.stoppingDistance = minDistanceToFight;
        lastShootTime = Time.time - reloadTime;
    }
    float calcSqrDistance()
    {
        return (mTransform.position - PlayerManager.PlayerTransform.position).sqrMagnitude;
    }
    public void shootAtPlayer()
    {
        if (Time.time > lastShootTime + reloadTime)
        {
            ObjectPool.Instantiate(EnemyFlyWeight.BulletPrefab, muzzleTransform.position, muzzleTransform.rotation);
            lastShootTime = Time.time;
        }
    }
    void rotateTowardPlayer()
    {
        if (!isRotatingTowardPlayer)
        {
            StartCoroutine(rotateTowardPlayerCoroutine());
        }
    }
    void changePosAfterAWhile()
    {
        if (isChangingPos)
            return;
        if (Time.time > lastMovedTime + changePosPeriod)
        {
            lastMovedTime = Time.time;
            mAgent.stoppingDistance = .5f;
            Vector3 destPos = getRandomPos();
            mAgent.SetDestination(destPos);
            StartCoroutine(changePosCoroutine(destPos));
               
        }
    }
    Vector3 getRandomPos()
    {
        Vector3 outPut = new Vector3(UnityEngine.Random.Range(-2.00f, 2.00f), 0, UnityEngine.Random.Range(-2.00f, 2.00f));
        outPut += mTransform.position;
        return outPut;
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
    public void SetState(State state)
    {
        currentState.OnExit();
        currentState = state;
        currentState.OnEnter();
    }
    #endregion
    #region Coroutines
    IEnumerator changePosCoroutine(Vector3 dest)
    {
        isChangingPos = true;
        while ((mTransform.position - dest).sqrMagnitude > mAgent.stoppingDistance * mAgent.stoppingDistance)
        {
            yield return delay;
        }
        isChangingPos = false;
    }
    IEnumerator agentAiCouroutine()
    {
        yield return null;
        if (isChangingPos)
            yield break;
        while (true)
        {
            yield return EnemyFlyWeight.DelayBetweenAIStateCheck;
            if (state == EnemyState.PRESUING)
            {
                if (calcSqrDistance() > minDistanceToFight * minDistanceToFight)
                    mAgent.SetDestination(PlayerManager.PlayerTransform.position);

                else
                    state = EnemyState.FIGHTING;
            }
            else if (state == EnemyState.FIGHTING)
            {
                if (calcSqrDistance() > distanceToStartPresueAgain * distanceToStartPresueAgain)
                {
                    state = EnemyState.PRESUING;
                    mAgent.stoppingDistance = minDistanceToFight;
                }
                else
                {
                    shootAtPlayer();
                    changePosAfterAWhile();
                    rotateTowardPlayer();
                }
            }
        }
    }

    #endregion
    #region coroutines
    IEnumerator rotateTowardPlayerCoroutine()
    {
        float t = 0.0f;
        float period = .3f;
        isRotatingTowardPlayer = true;
        while (t < period)
        {
            t += Time.deltaTime;
            mTransform.rotation = Quaternion.Slerp(mTransform.rotation,Quaternion.LookRotation(-mTransform.position+ PlayerManager.PlayerTransform.position,mTransform.up),t/period);
          
            yield return null;
        }
        isRotatingTowardPlayer = false;
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
