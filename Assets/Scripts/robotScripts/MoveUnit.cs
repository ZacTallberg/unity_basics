﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HexasphereGrid;

public class MoveUnit : MonoBehaviour
{
    [Tooltip("Bool that shows if the unit is currently moving")]
    public bool moving;
    [Tooltip("The current destination of our unit")]
    public int destinationIndex;
    public Unit thisUnit;
    [Tooltip("The instance of the Hexasphere which this unit resides on")]
    public Hexasphere hexa;
    [Tooltip("How quickly this unit moves between tiles")]
	public float moveSpeed;
    [Tooltip("The reference to the tile on which this unit currently resides")]
	public int currentTile;
    autoMovement autoMove;
    private Coroutine moveCoroutine;
    public int destination;
    public Vector3 destinationPos;
    public Vector3 currentPos;
    public List<int> path;
    public float t;
    public int tileIndex;
    
    // Start is called before the first frame update
    void Start()
    {
       //Debug.Log("starting at moveUnit");
       if(hexa == null) hexa = Hexasphere.GetInstance ("Hexasphere");
       if(thisUnit == null) thisUnit = gameObject.GetComponent<Unit>();
       if(autoMove == null) autoMove = gameObject.GetComponent<autoMovement>();
       //currentTile = thisUnit.currentTile;

    }

    void OnEnable()
    {
        //hexa.OnTileClick += tileClicked;
        touchManager.moveRobot += tileClicked;
    }

    void OnDisable()
    {
        //hexa.OnTileClick -= tileClicked;
        touchManager.moveRobot -= tileClicked;
    }

    public void startMove(int destinationInt)
    {
        Debug.Log("we're moving from here");
        currentTile = thisUnit.currentTile;
       // destinationIndex = destinationInt;
        path = findPath(destinationInt);
       // Debug.Log("destination index: " + destinationIndex.ToString());
        moveCoroutine = StartCoroutine(move(path));
    }
    public void stopMove()
    {
        if(moveCoroutine != null)
        {
            StopCoroutine(moveCoroutine);
        }
    }
    // Update is called once per frame
    void Update()
    {
        //Debug.Log(hexa.hasDragged);
    }

    public void moveOnPath(List<int> path)
    {
        moveCoroutine = StartCoroutine(move(path));
    }

    public void changeMoveNow(List<int> path)
    {
        StopCoroutine (moveCoroutine);
        moveCoroutine = StartCoroutine(move(path));
    }

    void tileClicked(int tileThere)
    {
        /*
        //Check if pointer is currently over a UI element, if so do nothing
        Vector2 pos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        if(IT_Utility.IsCursorOnUI(pos))
        {
            Debug.Log("on UI");
            return;
        }
        if(hexa.GetTileTagInt(tileThere) == 2)
        {
            Debug.Log("on Grass");
            return;
        }*/
        //if was just dragging, don't click destination
        if(hexa.hasDragged == true){
            return;
        }
        if(autoMove.wanderNow == true)
        {
            autoMove.wanderNow = false;
        }
            destination = tileThere;
            thisUnit.stateMachine.ChangeState(new MoveToDestination(thisUnit));
        
    }

    public List<int> findPath(int tileThere)
    {
        //Get the index of the tile we're currently at
        int tileHere = hexa.GetTileAtLocalPos(transform.position);
        //Get a list of all the tiles from here to there
        List<int> tiles = hexa.FindPath(tileHere, tileThere, 0, -1);

        

        return tiles;
    }

    //responsible for executing the movement over time
    public IEnumerator move(List<int> path)
    {
        /*while (path == null)
        {
            Debug.Log("path is null");
            yield return new WaitForSeconds(0.01f);
        }*/
        //yield return StartCoroutine(findPath(destinationIndex));
        currentTile = path[0];
        Vector3 lastPos = transform.position;
        for (int i = 0; i < path.Count; i++)
        {
            tileIndex = i;
            //get position of next tile
            
            //get current position
            currentPos = transform.position;

            t = 0f;
            
            while (t < 1f)
            {
                //This fixes the destination position just fine
                destinationPos = hexa.GetTileCenter(path[i]);
                
                t += Time.deltaTime * moveSpeed;
                transform.position = Vector3.Slerp(currentPos, destinationPos, t);

                Vector3 lookDir = transform.position - lastPos;
                //correct rotation to keep transform forward aligned with movement direction and transform up aligned with tile normal
                transform.rotation = Quaternion.LookRotation(lookDir, transform.position - hexa.transform.position);
                lastPos = transform.position;
                
                //this almost makes things work?
                //currentPos = transform.position;
                //above line not final

                yield return new WaitForSeconds (Time.deltaTime);
            }
            currentTile = path[i];
            //currentPos = transform.position;
        }
        thisUnit.stateMachine.ChangeState(new Idle(thisUnit));
       // Debug.Log("destination reached, coroutine finishing");
    }
}
