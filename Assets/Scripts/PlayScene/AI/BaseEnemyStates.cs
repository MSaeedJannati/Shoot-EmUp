using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace BaseEnemyStates 
{
    [Serializable]
    public class AIStateMachine:StateMachine
    {
        State currentState;
        EnemyManager manager;
        #region States
        Idle idleState;
        Fight fightState;
        Chase chaseState;
        RotateTowardPlayer rotateTowardState;
        ChnagePos changePosState;
        Patrol patrolState;
        #endregion
        #region Properties
        Idle IdleState=> idleState;
        Fight FightState=> fightState;
        Chase ChaseState=> chaseState;
        RotateTowardPlayer RotateTowardState=> rotateTowardState;
        ChnagePos ChangePosState=> changePosState;
        Patrol PatrolState=> patrolState;
        EnemyManager Manager => manager;
        #endregion
        #region Constructor
        public  AIStateMachine(EnemyManager manager_)
        {
            manager = manager_;
            idleState = new Idle(this);
            fightState=new Fight(this);
            chaseState = new Chase( this);
            rotateTowardState=new RotateTowardPlayer( this);
            changePosState=new ChnagePos(this);
            patrolState=new Patrol( this);
            currentState = idleState;
        }
        #endregion
        #region Variables
        #endregion
        #region Functions
        public void ChangeState(State state)
        {
            currentState?.OnExit();
            currentState = null;
            currentState = state;
            currentState?.OnEnter();
        }
        public void Update_()
        {
            currentState.Update_();
        }
        #endregion
    }
    [Serializable]
    public class Patrol : State
    {
        public Patrol( AIStateMachine stateMachine)
        {
            Fsm = stateMachine;
        }
        public override void OnEnter()
        {
        }

        public override void OnExit()
        {
        }

        public override void Update_()
        {
        }
    }
    [Serializable]
    public class Idle : State
    {
        public Idle(AIStateMachine stateMachine)
        {
            Fsm = stateMachine;
        }
        public override void OnEnter()
        {
        }

        public override void OnExit()
        {
        }

        public override void Update_()
        {
        }
    }
    [Serializable]
    public class Fight : State
    {
        float lastTimeShoot;
        float coolDownTime;
        float changePosCoolDown;

        public Fight( AIStateMachine stateMachine, float cooldownTime=2.5f)
        {
            coolDownTime = cooldownTime;
            lastTimeShoot = Time.time - coolDownTime;
            Fsm = stateMachine;
        }
        public override void OnEnter()
        {
        }

        public override void OnExit()
        {
        }

        public override void Update_()
        {
        }
    }
    [Serializable]
    public class Chase : State
    {
        public Chase( AIStateMachine stateMachine)
        {
            Fsm = stateMachine;
        }
        public override void OnEnter()
        {
        }

        public override void OnExit()
        {
        }

        public override void Update_()
        {
        }
    }
    [Serializable]
    public class RotateTowardPlayer : State
    {
        public RotateTowardPlayer( AIStateMachine stateMachine)
        {
            Fsm = stateMachine;
        }
        public override void OnEnter()
        {
        }

        public override void OnExit()
        {
        }

        public override void Update_()
        {
        }
    }
    [Serializable]
    public class ChnagePos : State
    {
        float rotateSpeed = 60.0f;
        Transform mTransform;
        Quaternion destRot;
        public ChnagePos( AIStateMachine stateMachine)
        {
            Fsm = stateMachine;
        }
        public override void OnEnter()
        {
        }

        public override void OnExit()
        {
        }

        public override void Update_()
        {
            destRot = Quaternion.LookRotation(-mTransform.position + PlayerManager.PlayerTransform.position, mTransform.up);
            mTransform.rotation = Quaternion.RotateTowards(mTransform.rotation, destRot, rotateSpeed * Time.deltaTime);
            if (Quaternion.Angle(mTransform.rotation, destRot) < 10)
            {
                //mManager.SetState()
            }
                //Quaternion.Slerp(mTransform.rotation, Quaternion.LookRotation(-mTransform.position + PlayerManager.PlayerTransform.position, mTransform.up), t / period);
        }
    }
}
