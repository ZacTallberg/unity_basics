using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class harvestLines : MonoBehaviour
{

    public Dictionary<GameObject, float> robotDistances;
    public plantBase thisGrass;
    public List<Transform> robots;
    public GameObject robotParent;
    public float refreshTime;
    public float startTime;
    public bool testingLine;
    public float damageFloat;

    public bool testingLinq;
    public bool testingNonLinq;

    //LineRenderer variables
    public Color c1 = Color.yellow;
    public Color c2 = Color.red;
    public int lengthOfLineRenderer = 20;

    public LineRenderer lineRenderer;
    private void OnEnable()
    {
        spawnManager.addToList += addToList;
        Unit.removeMe += removeFromList;
    }
    private void OnDisable()
    {
        spawnManager.addToList -= addToList;
        Unit.removeMe -= removeFromList;
    }
    void addToList(Transform robot){
        robots.Add(robot);
    }
    void removeFromList(Transform robot){
        robots.Remove(robot);
    }
    // Start is called before the first frame update
    void Start()
    {
        thisGrass = this.GetComponent<grass>();
        if(robotParent == null) robotParent = GameObject.Find ("Robots");
        if(robots.Count == 0){
            Transform[] robotTemp = robotParent.GetComponentsInChildren<Transform>();
            robots = new List<Transform>(robotTemp);
            robots.Remove(robotParent.transform);
        }

        //necessary additions for the linerenderer
        if(lineRenderer==null) lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.widthMultiplier = 0.5f;
        lineRenderer.positionCount = lengthOfLineRenderer;
        // A simple 2 color gradient with a fixed alpha of 1.0f.
        float alpha = 1.0f;
        Gradient gradient = new Gradient();
        gradient.SetKeys(
            new GradientColorKey[] { new GradientColorKey(c1, 0.0f), new GradientColorKey(c2, 1.0f) },
            new GradientAlphaKey[] { new GradientAlphaKey(alpha, 0.0f), new GradientAlphaKey(alpha, 1.0f) }
        );
        lineRenderer.colorGradient = gradient;

    }

    //This script will iterate through cubes and calculate all distances from the object this script is on
    void calculateCubeDistances()
    {
        Debug.Log("calculate...");
        robots.OrderBy(
            x => Vector3.Distance(x.transform.position, this.transform.position)
        ).ToList();

        
        
    }

    void calculateNonLinq()
    {
        robots.Sort(delegate (Transform a, Transform b)
        {
            return Vector3.Distance(gameObject.transform.position, a.transform.position)
            .CompareTo(
                Vector3.Distance(gameObject.transform.position, b.transform.position));
        });


            
        
    }

    public testingHealth currentRobot;
    public bool testingCoroutineStarted;
    public IEnumerator testingCoroutine()
    {
        

        while(robots.Count > 0)
        {
            if(currentRobot != robots.First().GetComponent<testingHealth>()) currentRobot = robots.First().GetComponent<testingHealth>();
            lineRenderer.SetPosition(0, gameObject.transform.position);
            lineRenderer.SetPosition(1, robots.First().position);
            //decrement the robot's health that we're currently drawing a line to
            currentRobot.attackMeNow(damageFloat);
            yield return null;
        }
        Debug.Log("change back to false");
        testingCoroutineStarted = false;
        yield return null;
    }


    // Update is called once per frame
   
    void Update()
    {
        
        LineRenderer lineRenderer = GetComponent<LineRenderer>();
        
        refreshTime -= Time.deltaTime;
        if (refreshTime < 0 && testingLinq == true)
        {
            calculateCubeDistances();
            refreshTime = startTime;
        }

        if (refreshTime < 0 && testingNonLinq == true)
        {
            if(robots.Count > 0){
                calculateNonLinq();
            }
            
            refreshTime = startTime;
        }

        if(testingCoroutineStarted == false && robots.Count > 0)
        {
            StartCoroutine("testingCoroutine");
            testingCoroutineStarted = true;
        }

        /* if(testingLine == true)
        {
            lineRenderer.SetPosition(0, gameObject.transform.position);
            lineRenderer.SetPosition(1, robots.First().position);
        }*/

    }
}
