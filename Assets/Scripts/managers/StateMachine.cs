using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HexasphereGrid;
public interface IState
{
    void PreCalculate();
    void Enter();
	void Execute();
    void Exit();
}
 
public class StateMachine
{
    public IState currentState;
 
    public void ChangeState(IState newState)
    {
        if (currentState != null)
            currentState.Exit();

        
        currentState = newState;
        currentState.PreCalculate();
        //Debug.Log("Current state: " + currentState.ToString());
    }
   
    public void Update()
    {
        if (currentState != null) currentState.Execute();
    }
}



 


