using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flickToss : MonoBehaviour
{
    public Vector3 spawnPosition;
    public GameObject seedPrefab;
    public Vector3 worldAngle;
    public float velocity;
    public float speed;
    public float speedMultiplier;
    public float speedLimiter;
    public float offset;

    /// <summary>
    /// This function is called when the object becomes enabled and active.
    /// </summary>
    void OnEnable()
    {
        //Debug.Log("test");
        //IT_Gesture.onSwipeE += flickObject;
        IT_Gesture.onSwipeEndE += testFlick;
        
    }

    /// <summary>
    /// This function is called when the behaviour becomes disabled or inactive.
    /// </summary>
    void OnDisable()
    {
        //IT_Gesture.onSwipeE -= flickObject;
        IT_Gesture.onSwipeEndE -= testFlick;
    }
    public void triFlick(SwipeInfo swipe)
    {
        //Find angle forwards by using distance between two swipe start/end points
        //tan(a) = distance between / force forward by multiplying distance forward by how long the touch was pressed
        //gets distance between two points and /2 so that it's between 1-100
        float distanceBt = Mathf.Sqrt((Mathf.Pow((swipe.endPoint.x - swipe.startPoint.x), 2) + Mathf.Pow((swipe.endPoint.y - swipe.startPoint.y), 2)))/2;
        //Debug.Log(distanceBt);
        //Calculates a float correlated with the duration of the swipe
        float durationAdjust = swipe.duration*offset;
        Debug.Log(durationAdjust);

        float speed = distanceBt/swipe.duration;
        float adjacent = speed;
        float angle = Mathf.Tan(distanceBt/adjacent);
        
        angle = angle * speedLimiter;
        //Debug.Log(angle);
        Vector3 worldStart = spawnPosition;
        //Vector3 worldEnd = new Vector3 (spawnPosition.x, )
        //Debug.Log(swipe.duration);
    }
    public void testFlick(SwipeInfo swipe)
    {
        
        float swipeFast = swipe.duration;
        //Get start and end swipe points for use later
        Vector3 start = new Vector3 (swipe.startPoint.x, swipe.startPoint.y, spawnPosition.z);
        Vector3 end = new Vector3 (swipe.endPoint.x, swipe.endPoint.y, (spawnPosition.z + speedMultiplier));
        
        float changeY = swipe.startPoint.y - swipe.endPoint.y;
        float changeX = swipe.endPoint.x - swipe.endPoint.x;
        //Need to add more versatility here to manage distance based on swipe length
        float changeZ = transform.position.z + speedMultiplier;

        //Get local position vectors that correspond to start/end swipe points
        Vector3 selfStart = transform.position;
        //Vector3 selfEnd = 
        //end = Camera.main.ScreenToWorldPoint(end);
//        Vector3 ray = end - start;
        //Ray point = Camera.main.ScreenPointToRay(ray);
        //Debug.DrawLine(start, end, Color.blue, 20f);
        //Debug.DrawRay(point.origin, point.direction*20, Color.red, 20f);
        //Vector3 cameraToSpawn = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, (Camera.main.transform.position.z + spawnPosition.z));
        //spawnPosition = cameraToSpawn;
        //GameObject seed = Instantiate(seedPrefab, spawnPosition, Camera.main.transform.rotation) as GameObject;
        //seed.GetComponent<Rigidbody>().AddForce(ray, ForceMode.Impulse);
    }
    public void flickObject(SwipeInfo swipeData)
    {
       Debug.Log("testing swipe"); 
       float swipeLength = swipeData.GetMagnitude();
       //Find velocity from the length as compared to the duration
       velocity = swipeLength / (swipeLength - swipeData.duration);
       //Multiply velocity times multiplier to get speed
       speed = velocity * speedMultiplier;
       
       speed = speed * speedLimiter;
        //speed = speed - (speed * speedLimiter);
       worldAngle = Camera.main.ScreenToWorldPoint(new Vector3 (swipeData.endPoint.x, (swipeData.endPoint.y + offset), (Camera.main.nearClipPlane - offset)));

       GameObject seed = Instantiate(seedPrefab, spawnPosition, Quaternion.identity) as GameObject;
    }

    public void flickNow(GameObject newObj)
    {
        newObj.GetComponent<Rigidbody>().AddForce(new Vector3((worldAngle.x * speed), (worldAngle.y * speed), (worldAngle.z * speed)));
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
