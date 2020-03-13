using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HexasphereGrid;

public class template : IState {

	Unit owner;
	public template(Unit owner) {this.owner = owner; }

	public void PreCalculate()
	{
		Enter();
	}
	public void Enter()
	{

	}
	public void Execute()
	{

	}
	public void Exit()
	{

	}
}
