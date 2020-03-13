using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HexasphereGrid;

public class sendToDestination : IState
{
    Unit owner;   
    public sendToDestination(Unit owner) { this.owner = owner; }
 
    public void PreCalculate()
    {
        Enter();
    }
    public void Enter()
    {
        //changes color to visually show state change
        //Color color = Color.red;
		//owner.selfcolor.SetColor(color);
        owner.selfSend.startSend(owner.selfSend.destination);
               
        //Debug.Log("entering test state");
    }
 
    public void Execute()
    {
        
    }
 
    public void Exit()
    {
        owner.selfSend.stopMove();
        owner.destroyMe();
        //Debug.Log("exiting test state");
    }



    
}
