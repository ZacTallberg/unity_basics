using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HexasphereGrid;

public class Idle : IState {

	Unit owner;
	public Idle(Unit owner) {this.owner = owner; }

	public void PreCalculate()
	{
		Enter();
	}
	public void Enter()
	{
		//changes color to visually show state change
		//Color color = Color.white;
		//owner.selfcolor.SetColor(color);
		
	}
	public void Execute()
	{

	}
	public void Exit()
	{
		
		
	}
}
