using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
//using System.Linq;

public class MarbleGravity : MonoBehaviour {


	public GameObject Planet;
	//public GameObject ActiveMarble;
	public float gravityConstant;
	public Vector3 gravityForce;
	public float distanceBetween;
	public Vector3 normalizedDirection;
	public float massTimesConstant;
	public Material selfMaterial;
	public Vector3 awayFromPlanet;
	public Vector3 marbleToPlanet;
	public Vector3 randomizedAwayForce;
	public Vector3 inverseNormx;
	public Vector3 inverseNormy;
	public Vector3 inverseNormz;
	public float shootForce;
	public float waitTime;
	public bool avoidX;
	public bool avoidY;
	public bool avoidZ;
	public bool touchingPlanet;
	//calculates force towards the planet
	//(constant*m1*m2)/(distance^2)
	public bool isSleeping;
	public Rigidbody selfBody;
	private float max;
	private int collCount;
	// Use this for initialization
	void Start () {
		collCount = 0;
		Planet = GameObject.Find ("Hexasphere");
		selfMaterial = gameObject.GetComponent<MeshRenderer>().material;
		selfBody = gameObject.GetComponent<Rigidbody> ();
		max = selfBody.sleepThreshold;
		selfBody.sleepThreshold = 0.0025f;
	}

	void OnCollisionEnter(Collision other)
	{
		isSleeping = false;
		if(other.transform.name == "Hexasphere")
		{
			collCount = 1;
			selfBody.drag = 1f;
			selfBody.angularDrag = 1f;
			touchingPlanet = true;

		}
		else{
			//Debug.Log(other.transform.name);
		}
	}

	void OnCollisionExit(Collision other)
	{
		if(other.transform.name == "Hexasphere")
		{
			collCount = 0;
			selfBody.drag = 0.2f;
			selfBody.angularDrag = 0.1f;
		}
	}

	void OnDrawGizmos()
	{


		//Gizmos.DrawWireSphere (Hexasphere.transform.position, 10f);
		//Gizmos.DrawLine (gameObject.transform.position, inverseNormx);
		//Gizmos.DrawCube (gameObject.transform.position, new Vector3 (1f, 1f, 1f));

	}

	void addForceToMarble()
	{
		gameObject.GetComponent<Rigidbody>().AddForce(gravityForce);
	}

	// Update is called once per frame
	void FixedUpdate () {
		inverseNormx = new Vector3 (-1 / normalizedDirection.x, 0f, 0f);
		inverseNormy = new Vector3 (0f, -1 / normalizedDirection.y, 0f);
		inverseNormz = new Vector3 (0f, 0f, -1 / normalizedDirection.z);
		Vector3 inverseNormXYZ = new Vector3 (-1 / normalizedDirection.x, -1 / normalizedDirection.y, -1/normalizedDirection.z);
		marbleToPlanet = Planet.transform.position - gameObject.transform.position;
		distanceBetween = marbleToPlanet.magnitude;
		Vector3 calcDirection = marbleToPlanet / distanceBetween;
		normalizedDirection = calcDirection;
		massTimesConstant = gravityConstant*Planet.GetComponent<Rigidbody>().mass*gameObject.GetComponent<Rigidbody>().mass;
		gravityForce = normalizedDirection*(massTimesConstant)/(distanceBetween*distanceBetween);
		float average = (selfBody.velocity.x + selfBody.velocity.y + selfBody.velocity.z) / 3;

		if(collCount == 0)
		{
			if(isSleeping!= false) {
				isSleeping = false;

			}
		}


		//if (Mathf.Abs (selfBody.velocity.x) <= max || Mathf.Abs (selfBody.velocity.y) <= max || Mathf.Abs (selfBody.velocity.z) <= max) {
		if((Mathf.Abs (average) <= max)& collCount == 1){
			
			if(waitTime <= 0f){
				if (isSleeping != true) {
					isSleeping = true;
					selfMaterial.color = Color.green;
					selfBody.Sleep ();
				}
			}
			else{
				waitTime -= Time.deltaTime;
			}
				
		} else if (isSleeping == false){
				
				waitTime = 0.1f;
				if (selfMaterial.color != Color.red)
				{
				
					selfMaterial.color = Color.red;
				}
				addForceToMarble ();
			}
		//Debug.Log (collCount);
		//Debug.Log (selfBody.IsSleeping ());
		Debug.DrawRay (gameObject.transform.position, inverseNormx, Color.red, 0.1f, false);
		Debug.DrawRay (gameObject.transform.position, inverseNormy, Color.blue, 0.1f, false);
		Debug.DrawRay (gameObject.transform.position, inverseNormz, Color.green, 0.1f, false);
		Debug.DrawRay (gameObject.transform.position, inverseNormXYZ, Color.cyan, 0.1f, false);
		Debug.DrawRay (gameObject.transform.position, normalizedDirection, Color.white, 0.1f, false);


		//Debug.Log ("is " + (float)Math.Round ((double)average, 3) + " less than " + gameObject.GetComponent<Rigidbody> ().sleepThreshold);



	}

}
