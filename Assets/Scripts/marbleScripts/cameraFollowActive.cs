using UnityEngine;
using System.Collections;

public class cameraFollowActive : MonoBehaviour {


	public GameObject averageMarblePos;
	public GameObject cameraObject;
	public GameObject planet;
	public Transform activeObj;
	public Transform cameraTarget;
	public float freedom;
	public Vector3 desiredOffset;
	public float dampSpeed;
	public Vector3 marblePos;
	private Vector3 cameraToMarble;
	private float distanceBetween;
	private Vector3 normDirection;
	public float distanceDefault;
	private float tempX;
	private float tempY;
	private float tempZ;
	// Use this for initialization
	void Start () {
			planet = GameObject.Find ("Hexasphere");
			//averageMarblePos = GameObject.Find("marbleAverage");
			

	}

	public void SetTargetObjectPosition ()
	{
		
		if (cameraTarget.position.x < (cameraObject.transform.position.x - freedom))
		{
			desiredOffset.x = cameraTarget.position.x + freedom;
		}
		else if (cameraTarget.position.x > (cameraObject.transform.position.x + freedom))
		{
			desiredOffset.x = cameraTarget.position.x - freedom;
		}
		if (cameraTarget.position.y < (cameraObject.transform.position.y - freedom))
		{
			desiredOffset.y = cameraTarget.position.y + freedom;

		}
		else if (cameraTarget.position.y > (cameraObject.transform.position.y + freedom))
		{
			desiredOffset.y = cameraTarget.position.y - freedom;

		}

		if (cameraTarget.position.z < (cameraObject.transform.position.z - freedom))
		{
			desiredOffset.z = cameraTarget.position.z + freedom;

		}
		else if (cameraTarget.position.z > (cameraObject.transform.position.z + freedom))
		{
			desiredOffset.z = cameraTarget.position.z - freedom;

		}

	}

	// Update is called once per frame
	void Update () {
		if (activeObj == null)
			activeObj = transform.Find ("Sphere");
		
		//Vector3 marblePosition = MarbleGravityObject.ActiveMarble.transform.position;
		cameraToMarble = cameraObject.transform.position - activeObj.transform.position;
		distanceBetween = cameraToMarble.magnitude;
		Vector3 marbleToPlanet = planet.transform.position - activeObj.transform.position;
		float distanceBetweenMP= marbleToPlanet.magnitude;
		Vector3 calcDirection = marbleToPlanet / distanceBetweenMP;
		normDirection = calcDirection;

		//Debug.DrawRay (gameObject.transform.position, normDirection, Color.black);


		//Vector3 distanceBetween = Vector3.Distance (MarbleGravityObject.transform.position, gameObject.transform.position);
		//Vector3 normDirectionWithDistance = normDirection * distanceFactor;
		marblePos = cameraTarget.transform.position;


		if (distanceBetweenMP > 0.6f*distanceDefault)
		{
			float distanceFactor = ((-1 * distanceBetweenMP) - 0.4f*distanceDefault);
			Vector3 aboveMarble = new Vector3 (normDirection.x*distanceFactor, normDirection.y*distanceFactor, normDirection.z*distanceFactor);
			Vector3 position = cameraTarget.position;
			position = aboveMarble;
			cameraTarget.position = position;
		}
		else{
			float distanceFactor = -distanceDefault;
			Vector3 aboveMarble = new Vector3 (normDirection.x*distanceFactor, normDirection.y*distanceFactor, normDirection.z*distanceFactor);
			Vector3 position = cameraTarget.position;
			position = aboveMarble;
			cameraTarget.position = position;
		}

		if (cameraTarget != null)
		{
			SetTargetObjectPosition();
			if (cameraObject.transform.position.x != cameraTarget.position.x + freedom)
			{
				
				tempX = cameraObject.transform.position.x;
				tempX = Mathf.Lerp (tempX, desiredOffset.x, Time.deltaTime * dampSpeed);

				//CameraObject.transform.position = new Vector3 (tempX, cameraObject.transform.position.y, cameraObject.transform.position.z);
			}
			if (cameraObject.transform.position.y != cameraTarget.position.y + freedom)
			{
				tempY = cameraObject.transform.position.y;
				tempY = Mathf.Lerp (tempY, desiredOffset.y, Time.deltaTime * dampSpeed);
				//cameraObject.transform.position = new Vector3 (cameraObject.transform.position.x, tempY, cameraObject.transform.position.z);
			}
			if (cameraObject.transform.position.z != cameraTarget.position.z + freedom)
			{
				tempZ = cameraObject.transform.position.z;
				tempZ = Mathf.Lerp (tempZ, desiredOffset.z, Time.deltaTime * dampSpeed);
				//cameraObject.transform.position = new Vector3 (cameraObject.transform.position.x, cameraObject.transform.position.y, tempZ);
			}

			cameraObject.transform.localPosition = new Vector3 (tempX, tempY, tempZ);
			//activeObj.transform.localPosition = new Vector3 (tempX + 0.5f, tempY, tempZ);
		}

		cameraObject.transform.LookAt (planet.transform);
		Vector3 newRot = cameraObject.transform.rotation.eulerAngles;
		//Debug.Log(newRot);
		/*if(cameraObject.transform.rotation.y  <= 0)
		{

		newRot = new Vector3(cameraObject.transform.rotation.eulerAngles.x,cameraObject.transform.rotation.eulerAngles.y + 180f, cameraObject.transform.rotation.eulerAngles.z);
			//cameraObject.transform.rotation.eulerAngles = new Vector3(newRot.x, newRot.y, newRot.z);
		}*/

	}

}
