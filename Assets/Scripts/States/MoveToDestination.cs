using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HexasphereGrid;

public class MoveToDestination : IState
{
    Unit owner;   
    public MoveToDestination(Unit owner) { this.owner = owner; }
 
    public void PreCalculate()
    {
        Enter();
    }
    public void Enter()
    {
        //changes color to visually show state change
        //Color color = Color.red;
		//owner.selfcolor.SetColor(color);
        owner.selfMove.startMove(owner.selfMove.destination);
               
        //Debug.Log("entering test state");
    }
 
    public void Execute()
    {
        
    }
 
    public void Exit()
    {
        owner.selfMove.stopMove();
        //Debug.Log("exiting test state");
    }



    
}
