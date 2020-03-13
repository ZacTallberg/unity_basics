using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class averageMarblePosition : MonoBehaviour {
	public GameObject marble;
	public GameObject marbleParent;
	public List<GameObject> marbleList;
	public List<Transform> marbleTest;
	public int number;
	// Use this for initialization
	void Start () {
		if(marbleParent == null)
		{
			marbleParent = GameObject.Find ("Marbles");
		}
	}
	
	// Update is called once per frame
	void Update () {
		/*if( number ==0){ 
			number = -1;
		}*/
			
		//number = marbleParent.transform.childCount;
		if (number != marbleList.Count || number == 0) {
			
			Debug.Log ("checking for infinite loop");
			Transform[] objects = marbleParent.GetComponentsInChildren<Transform> ();
			marbleTest = marbleParent.GetComponentsInChildren<Transform> ().ToList();
			foreach(Transform m in objects) {
				
					if (!marbleList.Contains (m.gameObject)) {
						if (m.gameObject != marbleParent) {
							number += 1;
							marbleList.Add ((m.gameObject));
						}
					}
			}
		}
		Vector3 marblePos = gameObject.transform.position;
		marblePos = marble.transform.position;
		gameObject.transform.position = marblePos;

	}
}
