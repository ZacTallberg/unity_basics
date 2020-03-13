using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HexasphereGrid;
public class grass : plantBase
{
    bool config;
    //scale of the object
    public float thisScale;
    public bool createChildren = false;
    public bool running;
    public float height;
    public Tile tile;
    public int selfTile;
    public List<int> neighborTiles;
    public List<int> possibleTiles;
    public SetSelfColor setColor;

    public delegate void canHarvest(GameObject selfObject);
    public static event canHarvest harvestMe;
    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        if(hexa == null) hexa = Hexasphere.GetInstance("Hexasphere");
        if(setColor == null) setColor = gameObject.GetComponent<SetSelfColor>();
        height = gameObject.GetComponent<BoxCollider>().bounds.size.y /2;
        setBasicAttributes();
        setSize();
        
    }
    
    void setBasicAttributes()
    {
        selfTile = hexa.GetTileAtLocalPos(gameObject.transform.localPosition);
        hexa.SetTileTag(selfTile, 2);
        //hexa.ParentAndAlignToTile(gameObject, selfTile, height, false, false, 1);
        hexa.SetTileColor(selfTile, Color.red, false);
        StartCoroutine(findNeighbors());
        
    }

    void setHeight()
    {
        height = (gameObject.GetComponent<BoxCollider>().bounds.size.y /2);
        //height = gameObject.transform.localScale.y /2;
        //Debug.Log(height);
        hexa.ParentAndAlignToTile(gameObject, selfTile, height, false, false, 1);
    }
    void Update()
    {
             if (isGrowing == true & running == false)
             {
                 StartCoroutine(growCoroutine());
             }
             //Debug.Log(stagesList.Count);
             
    }

    public IEnumerator findNeighbors()
    {
        //if(neighborTiles.Count == 0)
        //{
                if(neighborTiles.Count == 0)
                {
                    //Get array of all tile neighbors
                    int[] array = hexa.GetTileNeighbours(selfTile);
                    //Turn arry into List   
                    neighborTiles = new List<int>(array);
                }
                //Set values on all neighbors
                for (int i = 0; i < neighborTiles.Count; i++)
                {
                    //Debug.Log(neighborTiles[i].ToString() + " tag is: " + hexa.GetTileTagInt(neighborTiles[i]));
                    //get tile's tagInt
                    int tag = hexa.GetTileTagInt(neighborTiles[i]);
                    //If the tile isn't already tagged as 2 (grass), tag it and set color
                    if(tag != 2)
                    {
                        if(!possibleTiles.Contains(neighborTiles[i]))
                        {
                            possibleTiles.Add(neighborTiles[i]);
                        }
                        //visually debug neighbor tiles
                        hexa.SetTileColor(neighborTiles[i], Color.black, false);
                    }
                    else if (tag == 2){
                        
                        
                        //Debug.Log(neighborTiles[i].ToString() + " is already grass");
                        //Debug.Log(neighborTiles[i].ToString());
                        if(possibleTiles.Contains(neighborTiles[i]))
                        {
                            Debug.Log("removing");
                            possibleTiles.Remove(neighborTiles[i]);
                        }
                    }
                   
                    //yield return new WaitForSeconds(Time.deltaTime);
                }
        //}
        yield return null;
    }
    public void growingPlant()
    {

    }

    
    public IEnumerator newChildren()
    {
        //STILL HAVE A BUG THAT LETS GRASS SPAWN ON TILES THAT ALREADY HAVE GRASS ON THEM AS AVAILABLE

        /*if(possibleTiles.Count == 0)
        {

           yield break;
        }
        if(possibleTiles.Count != 0)
        {
            //check for possible tiles right before instantiating
            for(int i = 0; i < possibleTiles.Count - 1; i++)
            {
                if (hexa.GetTileTagInt(possibleTiles[i]) == 2)
                {
                    possibleTiles.Remove(possibleTiles[i]);
                }
            }*/
            //Get random number between first index and last index
            int random = Random.Range(0, possibleTiles.Count - 1);
            //Get center of tile chosen from random number
            Vector3 tileCenter = hexa.GetTileCenter(possibleTiles[random], true, true);
            //Set tile tag to 2
            hexa.SetTileTag(possibleTiles[random], 2);
            Quaternion farAwayQ = new Quaternion (0, 0, 0, 0);

            //PROBABLY NEEDS A SPAWNING MANAGER CLASS TO TAKE OVER THIS JOB
            
            //BUG FIX
            //INSTEAD OF CREATING NEW OBJECT RIGHT AWAY, ADD THE LOCATION TO A POOL 
            //AND CHECK IF THERE'S ALREADY ONE TO BE LOADED THERE
            //IF YES, THEN DO NOT ADD AGAIN
            //IF NO, THEN PLAN TO SPAWN A NEW OBJECT THERE
            //AT SOME POINT IN THE NEXT FEW FRAMES


            //Load prefab as gameobject
            GameObject newGrass = Instantiate (Resources.Load("grassPrefab") as GameObject, tileCenter, farAwayQ);
            newGrass.name = newGrass.name.ToString() + " " + possibleTiles[random].ToString();
            hexa.ParentAndAlignToTile(newGrass, possibleTiles[random], 0, false, false, 1);
            possibleTiles.Remove(possibleTiles[random]);
            //neighborTiles.Remove(neighborTiles[random]);
        //}


        //yield return StartCoroutine(findNeighbors());
        createChildren = false;
        yield return new WaitForSeconds(Time.deltaTime);

        
        


       
    }
    public void setSize()
    {
        if(growthTotal == 0)
        {
            Vector3 selfT = gameObject.transform.localScale;
            selfT = new Vector3 (thisScale, thisScale, thisScale);
            gameObject.transform.localScale = selfT;
            return;
        }
        //Debug.Log("didn't do this");
        float stageScale = thisScale * (growthStage + 1);
        //Vector3 scale = new Vector3 (stageScale, stageScale, stageScale);
        Vector3 selfTransform = gameObject.transform.localScale;
        selfTransform = new Vector3 (stageScale, stageScale, stageScale);
        gameObject.transform.localScale = selfTransform;
        setHeight();
        //gameObject.transform.localScale.Set(stageScale, stageScale, stageScale);
    }
    public IEnumerator growCoroutine()
    {
        running = true;
        growthStage = 1;
        while(isGrowing)
        {
            //call out event to add this instance prefab to a pool for harvesting
            if(growthStage == stagesList.Count && harvestable != true)
            {
                //event to send tile to Robot for harvesting
                harvestMe(gameObject);
                setColor.SetColor(Color.green);
                //bool so robots know that this object is harvestable
                harvestable = true;
                //turn off growing, as it has reached the max
                isGrowing = false;
                
            }
            //increment the growth float by adding deltaTime multiplied by another float
            growthTotal += Time.deltaTime * growTimeScale;
            
            //only increment growthStage if not already = to the max stageList
            if(growthStage != stagesList.Count)
            {
               //If total growth progress is larger than the float for the stage it's currently in, increment growthStage
               if(growthTotal >= stagesList[growthStage])
               {
                   //Debug.Log("made it here");
                   if(growthStage < stagesList.Count)
                   {   
                       //Debug.Log("set scale");
                       setColor.SetColor(Color.red);
                       growthStage += 1;
                       setSize();
                   }
               }
            }

            if(growthStage == stagesList.Count)
            {
                //createChildren = true;
                //findNeighbors();
                //Debug.Log("time to make a new plant");
                
                
            if(possibleTiles.Count != 0)
            {
            //check for possible tiles right before instantiating
            for(int i = 0; i < possibleTiles.Count - 1; i++)
            {
                if (hexa.GetTileTagInt(possibleTiles[i]) == 2)
                {
                    possibleTiles.Remove(possibleTiles[i]);
                }
            }
                if(createChildren == true)
                {
                    
                    StartCoroutine(newChildren());
                    growthStage = 0; 
                    growthTotal = 0;
                    setSize();
                }
            }
            else if(possibleTiles.Count == 0)
                    {
                        growthStage = 0;
                        growthTotal = 0;
                        setSize(); 
                        Debug.Log ("Nope");
                        createChildren = false;
                        yield break;
                    }
            }
            
            
            

        
            yield return new WaitForSeconds(Time.deltaTime);
        }
        //Debug.Log("stopping growing");
        running = false;
        //yield return new WaitForSeconds(0.1f);
    }
}
