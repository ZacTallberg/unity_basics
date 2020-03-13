using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HexasphereGrid;

public class harvestGrass : MonoBehaviour , IState {

	Unit owner;
	MoveUnit ownerMove;
	
	public harvestGrass(Unit owner) {this.owner = owner; }

	
	public void PreCalculate()
	{
		if (ownerMove == null) ownerMove = owner.GetComponent<MoveUnit>();
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
