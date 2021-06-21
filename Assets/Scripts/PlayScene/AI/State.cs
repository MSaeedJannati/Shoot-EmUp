using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
    protected StateMachine Fsm;
    abstract public void Update_();
    abstract public void OnExit();
    abstract public void OnEnter();

}
public abstract class StateMachine
{
    
}
