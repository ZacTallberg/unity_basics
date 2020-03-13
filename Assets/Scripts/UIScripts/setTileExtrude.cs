using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HexasphereGrid;

public class setTileExtrude : MonoBehaviour
{
    public adjustMenu menuParent;
    public touchManager touchMan;
    public Hexasphere hexa;
    public float extrudeFloat;
    public float waitTime;
    public Transform robotParent;
    // Start is called before the first frame update

    public delegate void newWall(int tileIndex);
    public static event newWall sendTileIndex;
    
    /// <summary>
    /// This function is called when the object becomes enabled and active.
    /// </summary>
    void OnEnable()
    {
        Hexasphere.testNow += testUI;
    }
    /// <summary>
    /// This function is called when the behaviour becomes disabled or inactive.
    /// </summary>
    void OnDisable()
    {
        Hexasphere.testNow -= testUI;
    }

    public void testUI(int index)
    {

    }
    void Start()
    {
        if (touchMan == null) touchMan = GameObject.Find("_Managers").GetComponent<touchManager>();
        if (menuParent == null) menuParent = GetComponentInParent<adjustMenu>();
        if (hexa == null) hexa = menuParent.hexa;
        if (robotParent == null) robotParent = GameObject.Find("Robots").GetComponent<Transform>();
    }


    public void setHeight()
    {
        StartCoroutine(setHeightCoroutine());
    }
    //Start coroutine for yield return in turret spawn script
    public IEnumerator setHeightCoroutine()
    {
        hexa.SetTileExtrudeAmount(menuParent.tileAtMenuLoc, extrudeFloat);
        hexa.SetTileColor(menuParent.tileAtMenuLoc, Color.red, false);
        hexa.SetTileCanCross(menuParent.tileAtMenuLoc, false);
        //Kill the UI menu in waitTime seconds
        StartCoroutine(menuParent.killMenu(waitTime));
        if(robotParent.childCount > 0)
        {
        //Send the tileAtMenuLoc to autoMovement to check that the path for the robot is still navigable
            sendTileIndex(menuParent.tileAtMenuLoc);
        }
        yield return null;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
