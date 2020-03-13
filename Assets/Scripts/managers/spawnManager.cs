using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HexasphereGrid;


public class spawnManager: MonoBehaviour
{
    public Vector3 spawnPoint;
    public Transform spawnParent;
    public Transform destination;
    public Transform start;
    public GameObject robot;
    public int robotHeight;
    public Unit thisRobot;
    public Hexasphere hexasphere;
 
    public delegate void addToTurretList(Transform target);
    public static event addToTurretList addToList;
    
    void OnEnable()
    {
       fixedButton.sendInitiate += startCoroutine;
    }
    void OnDisable()
    {
        fixedButton.sendInitiate -= startCoroutine;
    }
    public void startCoroutine()
    {
        StartCoroutine(startMove());
    }
    public IEnumerator startMove()
    {
        CoroutineWithData cd = new CoroutineWithData(this, spawn());
        yield return cd.coroutine;
        GameObject spawnedRobot = cd.result as GameObject;
        sendUnit thisSendUnit = spawnedRobot.GetComponent<sendUnit>();
        thisSendUnit.destinationTransform = destination;
        thisSendUnit.startingTransform = start;
        thisSendUnit.initiate();
    }
    public IEnumerator spawn()
    {
        GameObject newRobot = Instantiate(robot, spawnPoint, Quaternion.identity);
        newRobot.transform.SetParent(spawnParent, true);
        addToList(newRobot.transform);
        int spawnTile = hexasphere.GetTileAtLocalPos(start.position);
        Unit thisUnit = newRobot.GetComponent<Unit>();
        hexasphere.ParentAndAlignToTile(newRobot, spawnTile, robotHeight, false, false, 1);
        thisUnit.currentTile = spawnTile;
        
        yield return newRobot;
        
    
    }
    // Start is called before the first frame update
    void Start()
    {
        if (hexasphere == null) hexasphere = Hexasphere.GetInstance("Hexasphere");
       //Dictionary<GameObject, float> robotDistance = new Dictionary<GameObject, float>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
