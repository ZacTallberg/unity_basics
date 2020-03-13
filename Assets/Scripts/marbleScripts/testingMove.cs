using UnityEngine;
using System.Collections;
using System;

public class testingMove : MonoBehaviour {

	public MarbleGravity selfGravity;
	public Rigidbody selfBody;
	public Vector3 directionMovement;
	public float movementX;
	public float movementY;
	public Vector3 tangentRight;
	public Vector3 tangentDown;
	public Vector3 tangentLeft;
	public Vector3 tangentUp;
	public float magnitudex1;
	public float magnitudex2;
	public float magnitudex3;
	public float magnitudex4;
	public float magnitudex5;
	public float magnitudex6;
	public float speed;
	void FixedUpdate(){
		if(selfBody == null)
			selfBody = GetComponent<Rigidbody> ();
		if(selfGravity == null)
			selfGravity = GetComponent<MarbleGravity> ();
		
		Vector3 movement = new Vector3 (Input.acceleration.x, Input.acceleration.y, 0f);
		Vector3 normXrot = new Vector3(Camera.main.transform.localRotation.eulerAngles.x, Camera.main.transform.localRotation.eulerAngles.y, Camera.main.transform.localRotation.eulerAngles.z);
		Vector3 normNewX1 = Vector3.Cross (selfGravity.normalizedDirection, Vector3.up);
		Vector3 normNewX2 = Vector3.Cross (selfGravity.normalizedDirection, Vector3.down);
		Vector3 normNewX3 = Vector3.Cross (selfGravity.normalizedDirection, Vector3.forward);
		Vector3 normNewX4 = Vector3.Cross (selfGravity.normalizedDirection, Vector3.back);
		Vector3 normNewX5 = Vector3.Cross (selfGravity.normalizedDirection, Vector3.right);
		Vector3 normNewX6 = Vector3.Cross (selfGravity.normalizedDirection, Vector3.left);
		//normNewX1.Normalize();
		//normNewX2.Normalize();
		//normNewX3.Normalize();
	//	normNewX4.Normalize();
		//normNewX5.Normalize();
		//normNewX6.Normalize();
		magnitudex1 = (float)Math.Round((double)normNewX1.magnitude, 2);
		magnitudex2 = (float)Math.Round((double)normNewX2.magnitude, 2);
		magnitudex3 = (float)Math.Round((double)normNewX3.magnitude, 2);
		magnitudex4 = (float)Math.Round((double)normNewX4.magnitude, 2);
		magnitudex5 = (float)Math.Round((double)normNewX5.magnitude, 2);
		magnitudex6 = (float)Math.Round((double)normNewX6.magnitude, 2);

		//Debug.Log ("m1: " + magnitudex1 + "m2: " + magnitudex2 + "m3: " + magnitudex3 + "m4: " + magnitudex4);
		//Debug.Log("m2: "+ magnitudex2 + "m4: " + magnitudex4  + "m6: " + magnitudex6);
		if (magnitudex2 >= magnitudex4 & magnitudex2 >= magnitudex6) {
			if(magnitudex1 < magnitudex2)
			{
				Debug.Log("mag1<mag2");
			}
			else{
				tangentRight = normNewX2;
				//tangentRight = Quaternion.AngleAxis (-90, selfGravity.normalizedDirection) * tangentRight;
				tangentRight.Normalize();
				//Debug.Log("x2");
			}

		}
		else if (magnitudex4 >= magnitudex2 & magnitudex4 >= magnitudex6){
			if (transform.localPosition.x > 0 & transform.localPosition.y > 0 & transform.localPosition.z < 0)
			{
				tangentRight = normNewX4;
				tangentRight = Quaternion.AngleAxis (90, selfGravity.normalizedDirection) * tangentRight;
				tangentRight.Normalize();
				//Debug.Log("x4");
			}
			else{
			//tangentRight = normNewX3;
			//tangentRight = Quaternion.AngleAxis(90, selfGravity.normalizedDirection) * tangentRight;
			//tangentRight.Normalize();
				//Debug.Log("x3");
			}

		}
		else if(magnitudex6 >= magnitudex2 & magnitudex6 >= magnitudex4)
		{
			if (transform.position.z < 0 & transform.position.y > 0)
			{
				tangentRight = normNewX6;
				tangentRight = Quaternion.AngleAxis (90, selfGravity.normalizedDirection) * tangentRight;
				tangentRight.Normalize();
				//Debug.Log("x6");
			}
			else{
				//tangentRight = normNewX5;
				//tangentRight = Quaternion.AngleAxis (90, selfGravity.normalizedDirection) *  tangentRight;
				//tangentRight.Normalize();
				//Debug.Log("x5");
			}
		
		}

		tangentLeft = -tangentRight;
		tangentUp = Quaternion.AngleAxis (90, selfGravity.normalizedDirection) * tangentRight;
		tangentDown = Quaternion.AngleAxis (-90, selfGravity.normalizedDirection) * tangentRight;
		//Vector3 normTest1 = Vector3.Cross (selfGravity.normalizedDirection, tangentRight);
		//Vector3 normTest2 = Vector3.Cross (selfGravity.normalizedDirection, tangentLeft);


	
		Debug.DrawRay (Camera.main.transform.position, tangentRight, Color.red, 0.02f, false);

		Debug.DrawRay (Camera.main.transform.position, normNewX4, Color.blue, 0.02f, false);
		//Debug.DrawRay (Camera.main.transform.position, tangentLeft, Color.blue, 0.02f, false);
		Debug.DrawRay (Camera.main.transform.position, normNewX2, Color.green, 0.02f, false);
		//Debug.DrawRay (Camera.main.transform.position, tangentDown, Color.cyan, 0.02f, false);
		//Debug.DrawRay (Camera.main.transform.position, normNewX5, Color.magenta, 0.02f, false);
		Debug.DrawRay (Camera.main.transform.position, normNewX6, Color.yellow, 0.02f, false);
		#if UNITY_EDITOR
		if(Input.GetKey ("a"))
		{
			selfBody.AddForce (tangentLeft * speed);
		}
		else if(Input.GetKey ("d"))
		{
			selfBody.AddForce (tangentRight * speed);
		}
		if(Input.GetKey ("w"))
		{
			selfBody.AddForce (tangentUp * speed);
		}
		else if (Input.GetKey ("s"))
		{
			selfBody.AddForce (tangentDown * speed);
		}
		#endif

		if(Mathf.Abs (Input.acceleration.x) <= 0.1f)
		{
			//nothing moved here
		}
		else if(Input.acceleration.x < 0)
		{
			selfBody.AddForce (tangentLeft * speed);
		}
		else if (Input.acceleration.x > 0) {
			selfBody.AddForce (tangentRight * speed);
		}
		if(Mathf.Abs (Input.acceleration.y) <= 0.1f)
		{
			movementY = 0;
		}
		else if(Input.acceleration.y < 0)
		{
			selfBody.AddForce (tangentDown * speed);
		}
		else if (Input.acceleration.y > 0 ){
			selfBody.AddForce (tangentUp * speed);
		}

		Vector3 resultMove = new Vector3 (movementX, movementY, 0f);
		if(Mathf.Abs((Input.acceleration.x + Input.acceleration.y)/2) > 0.05f) {
			
			selfBody.AddForce (resultMove * speed);

		}


			
		//Debug.Log (normNewX);
		//Vector3 normYdir = new Vector3 (0f, selfGravity.normalizedDirection.y, 0f);
		//Vector3 normNewY = Quaternion.AngleAxis (movementY, -selfGravity.normalizedDirection) * selfGravity.normalizedDirection;
		//Debug.Log (normNewY);

		//Debug.DrawRay (gameObject.transform.position, normNewY, Color.blue, 0.05f, false);

	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(selfBody == null)
		{
			selfBody = GetComponent<Rigidbody> ();
		}
	}
}
