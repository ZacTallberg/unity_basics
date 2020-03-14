using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HexasphereGrid;

public class calculateDestinationPool : MonoBehaviour
{

    Hexasphere hexa; 
    public List<int> autoDestinations;

    public delegate void destinationsNow(List<int> passedDestinations);
    public static event destinationsNow sendDestinations;
    public int numRand;
    Coroutine startNow;

    public bool spawnGrass;
    public GameObject grassPrefab;
    public Transform robotParent;
    // Start is called before the first frame update
    void Start()
    {
        if (hexa == null) hexa = Hexasphere.GetInstance("Hexasphere");
        if (robotParent == null) robotParent = GameObject.Find("Robots").GetComponent<Transform>();
        if (robotParent.childCount > 0 ){
            startNow = StartCoroutine(calculateAutoDestinations());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator calculateAutoDestinations()
    {
        
        int temp = 0;
        yield return new WaitForSeconds(0.1f);
        for(int i = 0; i < hexa.tiles.Length; i++){
            
            temp++;
            string hexaIndex = hexa.tiles[i].index.ToString();
            //If tile already has tagInt 1, add to destinations
			if (hexa.tiles[i].tagInt == 1)
			{
                if(!autoDestinations.Contains(hexa.tiles[i].index))
                {
				    autoDestinations.Add(hexa.tiles[i].index);
				}
			}
            //If tile is == random number, set tagInt and change color
            if (temp == numRand)
            {
                temp = 0;
                //Debug.Log("yes");
                if(!autoDestinations.Contains(hexa.tiles[i].index))
                {
                    autoDestinations.Add(hexa.tiles[i].index);
                    hexa.tiles[i].tagInt = 1;
                    hexa.SetTileColor(hexa.tiles[i].index, Color.white, true);
                    Vector3 test = new Vector3 (100f, 100f, 100f);
                    if(spawnGrass == true)
                    {
                        GameObject newGrass = GameObject.Instantiate(grassPrefab, test, Quaternion.identity);
                        hexa.ParentAndAlignToTile(newGrass, hexa.tiles[i].index, newGrass.GetComponent<grass>().height, false, false, 1);
                    }
                }
            }
			//Debug.Log("calculating...");
		}
        if (robotParent.childCount > 0){
            sendDestinations(autoDestinations);
        }
        //Debug.Log("Sending destinations now");
    }
}
