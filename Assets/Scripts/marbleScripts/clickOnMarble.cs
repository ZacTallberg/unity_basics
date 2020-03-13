using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
//using System.Linq;

public class clickOnMarble : MonoBehaviour {

	private Vector3 screenPoint;
	private Ray debugRay;

	public delegate void clickMarble(MarbleGravity marblePass, Rigidbody marbleRigidbody);
	public static event clickMarble clickedMe;

	void OnEnable()
	{
		#if UNITY_ANDROID
		IT_Gesture.onTouchDownPosE += pointToSelf;
		#endif

		#if UNITY_EDITOR
		IT_Gesture.onMouse1DownE += pointToSelf;
		#endif
	}
	void OnDisable()
	{
		#if UNITY_ANDROID
		IT_Gesture.onTouchDownPosE -= pointToSelf;
		#endif

		#if UNITY_EDITOR
		IT_Gesture.onMouse1DownE -= pointToSelf;
		#endif
	}
	void pointToSelf(Vector2 touch)
	{
		screenPoint = new Vector3 (touch.x, touch.y, 0);
		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay (new Vector3 (screenPoint.x, screenPoint.y, 0));
		debugRay = ray;
		if(Physics.Raycast (ray, out hit))
		{
			//Debug.Log (hit.transform.name);
			if(hit.transform.GetComponent<MarbleGravity>()){
				MarbleGravity thisMarble = hit.transform.GetComponent<MarbleGravity>();
				thisMarble.waitTime = 0.1f;
				thisMarble.isSleeping = false;
				Rigidbody thisRigidbody = hit.rigidbody;
				clickedMe (thisMarble, thisRigidbody);
			}
		}
	}


	// Update is called once per frame
	void Update () {
		Debug.DrawRay (debugRay.origin, debugRay.direction*10, Color.white);
	}



}
