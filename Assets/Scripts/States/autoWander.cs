using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HexasphereGrid;

public class autoWander : IState {

	Unit owner;
	autoMovement ownerAuto;
	public autoWander(Unit owner) {this.owner = owner; }

	public void PreCalculate()
	{
		
		Enter();
	}
	
	public void Enter()
	{
		

		//Color color = Color.green;
		//owner.selfcolor.SetColor(color);
		//owner.selfAuto.startAutoMove();
		//Debug.Log("starting wandering");

	}
	public void Execute()
	{
		if (owner.selfAuto.stillMoving == false)
		{
			owner.selfAuto.startAutoMove();
		}
	}
	public void Exit()
	{
		//Color color = Color.white;
		//owner.selfcolor.SetColor(color);
		//owner.selfAuto.stopMove();
		//Debug.Log("stopping wandering");
	}
}
