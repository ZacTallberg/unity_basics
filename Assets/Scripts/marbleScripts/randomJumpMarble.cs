using UnityEngine;
using System.Collections;

public class randomJumpMarble : MonoBehaviour {

	public MarbleGravity selfGravity;
	public Rigidbody selfBody;
	public float waitTime;
	public float jumpInterval;
	public bool shouldAuto;
	// Use this for initialization
	void Start () {
		if (selfGravity == null) selfGravity = gameObject.GetComponent<MarbleGravity>();
	}
/// <summary>
/// Update is called every frame, if the MonoBehaviour is enabled.
/// </summary>
void Update()
{
	if(shouldAuto == true)
	{
		waitTime -= Time.deltaTime;
	}
	
	if (waitTime <= 0 && selfGravity.touchingPlanet == true) {
			//selfMaterial.color = UnityEngine.Random.ColorHSV (0, 1, 0, 1, 0, 1, 0, 1);
			RandomJumpMarble(selfGravity, selfBody);
			//marbleObj.AddForce(thisMarble.randomizedAwayForce*thisMarble.shootForce);
			//ActiveMarble.GetComponent<Rigidbody>().AddForce(awayFromPlanet*shootForce);
			waitTime = jumpInterval;
		}
}
	public void OnEnable()
	{
		clickOnMarble.clickedMe += RandomJumpMarble;
	}

	public void OnDisable()
	{
		clickOnMarble.clickedMe -= RandomJumpMarble;
	}

	public void RandomJumpMarble(MarbleGravity thisMarble, Rigidbody marbleObj)
	{


		thisMarble.awayFromPlanet = -thisMarble.normalizedDirection;


		float awayX = Mathf.Round (Mathf.Abs (thisMarble.awayFromPlanet.x));
		float awayY = Mathf.Round (Mathf.Abs (thisMarble.awayFromPlanet.y));
		float awayZ = Mathf.Round (Mathf.Abs (thisMarble.awayFromPlanet.z));
		float rand = UnityEngine.Random.value;
		if(awayX >= awayY | awayX >= awayZ)
		{
			float randomFloat = Mathf.Abs (Mathf.Round(UnityEngine.Random.Range (7, 10))/10f);
			if(rand < 0.5f)
			{
				randomFloat = -randomFloat;
			}

			if(thisMarble.avoidX) {
				thisMarble.randomizedAwayForce = new Vector3 (thisMarble.awayFromPlanet.x, Mathf.Clamp (thisMarble.awayFromPlanet.y + randomFloat, -1f, 1f), Mathf.Clamp (thisMarble.awayFromPlanet.z + randomFloat, -1f, 1f));
			}
			else{
				thisMarble.randomizedAwayForce = new Vector3 (Mathf.Clamp (thisMarble.awayFromPlanet.x + randomFloat, -1f, 1f), thisMarble.awayFromPlanet.y, thisMarble.awayFromPlanet.z);
			}
			marbleObj.AddForce(thisMarble.randomizedAwayForce*thisMarble.shootForce);

			//Debug.Log ("awayX triggered: " + randomFloat);
			return;
		}
		if(awayY >= awayX | awayY >= awayZ)
		{
			float randomFloat = Mathf.Abs (Mathf.Round(UnityEngine.Random.Range (7, 10))/10f);
			if(rand < 0.5f)
			{
				randomFloat = -randomFloat;
			}


			if(thisMarble.avoidY) {
				thisMarble.randomizedAwayForce = new Vector3 (Mathf.Clamp (thisMarble.awayFromPlanet.x + randomFloat, -1f, 1f), thisMarble.awayFromPlanet.y, Mathf.Clamp (thisMarble.awayFromPlanet.z + randomFloat, -1f, 1f));
			}
			else{
				thisMarble.randomizedAwayForce = new Vector3 (thisMarble.awayFromPlanet.x, Mathf.Clamp (thisMarble.awayFromPlanet.y + randomFloat, -1f, 1f), thisMarble.awayFromPlanet.z);
			}
			marbleObj.AddForce(thisMarble.randomizedAwayForce*thisMarble.shootForce);

			//Debug.Log ("awayY triggered: " + randomFloat);
			return;
		}
		if(awayZ >= awayX | awayZ >= awayY)
		{
			float randomFloat = Mathf.Abs (Mathf.Round(UnityEngine.Random.Range (7, 10))/10f);
			if(rand < 0.5f)
			{
				randomFloat = -randomFloat;
			}

			if(thisMarble.avoidZ) {
				thisMarble.randomizedAwayForce = new Vector3 (Mathf.Clamp (thisMarble.awayFromPlanet.x + randomFloat, -1f, 1f), Mathf.Clamp (thisMarble.awayFromPlanet.y + randomFloat, -1f, 1f), thisMarble.awayFromPlanet.z);
			}
			else{
				thisMarble.randomizedAwayForce = new Vector3 (thisMarble.awayFromPlanet.x, thisMarble.awayFromPlanet.y, Mathf.Clamp (thisMarble.awayFromPlanet.z + randomFloat, -1f, 1f));
			}
			marbleObj.AddForce(thisMarble.randomizedAwayForce*thisMarble.shootForce);

			//Debug.Log ("awayZ triggered: " + randomFloat);
			return;
		}
		
		

	}
}
