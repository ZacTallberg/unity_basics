using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HexasphereGrid;

public class autoMovement : MonoBehaviour
{
    public bool stillMoving;
    public float autoMoveSpeed;
    public Unit thisUnit;
    public bool wanderNow;
    public Hexasphere hexa;
    //coroutine to handle starting/stopping coroutines
    public Coroutine moveCoroutine;

    public int lastDestination;
    public int tileIndex;
    public Vector3 destinationPos;
    public Vector3 currentPos;
    public int currentTile;
    public List<int> destinations;
    public int currentDestination;
    public List<int> path;
    public float t;

    
    void OnEnable()
    {
        calculateDestinationPool.sendDestinations += addDestinations;
        setTileExtrude.sendTileIndex += checkIndexInPath;
    }

    void OnDisable()
    {
        calculateDestinationPool.sendDestinations -= addDestinations;
        setTileExtrude.sendTileIndex -= checkIndexInPath;
    }
    // Start is called before the first frame update
    void Start()
    {
        if (thisUnit == null) thisUnit = gameObject.GetComponent<Unit>();
        if (hexa == null) hexa = Hexasphere.GetInstance ("Hexasphere");
      //  if (hexa == null) hexa = GameObject.Find("Hexasphere").GetComponent<Hexasphere>();
        currentTile = thisUnit.currentTile;
        lastDestination = currentTile;
        //Moved this coroutine to it's own function
       // StartCoroutine(calculateAutoDestinations());
    }

    //links to setTileExtrude, checks if the new wall index is in the current path, if true then recalculate path
    void checkIndexInPath(int index)
    {
        if(path == null)
        {
            Debug.Log("the path is null");
        }
        //if the path contains our newly created wall
        if (path.Contains(index))
        {
            //Get a new path
            path = findPathList(currentDestination);

            //restart automove coroutine
            stopMove();
            moveCoroutine = StartCoroutine(autoMove(path));
        }
        else if (path == null)
        {
            Debug.Log("index is null");
        }
        else{
            Debug.Log("yep, no problem can still get there");
        }
    }

    //Add all the possible autoDestinations to the pool for later
    void addDestinations(List<int> passedInts)
    {
        destinations = passedInts;
        //Debug.Log("received and added destinations");
    }
    // Update is called once per frame
    void Update()
    {
        if (wanderNow == true && (thisUnit.stateMachine.currentState.ToString() != "autoWander"))
        {
            thisUnit.stateMachine.ChangeState(new autoWander(thisUnit));
        }
    }

    public void startAutoMove()
    {
        stillMoving = true;
        moveCoroutine = StartCoroutine(findAutoMovementDestinations());

    }

    public void stopMove()
    {
        if(moveCoroutine != null)
        {
            StopCoroutine(moveCoroutine);
        }
    }
    

    //Finds all destination tiles with tagInt 1, then moves towards random destination in said list
   public IEnumerator findAutoMovementDestinations()
	{
        while(destinations.Count == 0)
        {
            Debug.Log("Waiting for destinations to populate...");
            yield return new WaitForSeconds (0.2f);
        }
        //if the list of auto destinations doesn't have our last destination AND it's navigable, add it back
        if(destinations.Contains(lastDestination) == false && hexa.GetTileCanCross(lastDestination) == true){
            if(hexa.GetTileTagInt(lastDestination) != 0)
            {
                //Debug.Log(hexa.GetTileTagInt(lastDestination));
                this.destinations.Add(lastDestination);
            }
        }
        //set our last destination to the tile we're currently on
        lastDestination = currentTile;
        //remove our current tile from destination possibilities
        this.destinations.Remove(currentTile);
        //choose a random tile from our current list of destinations
        int random = Random.Range(0, destinations.Count);
		currentDestination = destinations[random];
		
        yield return findPathVoid(currentDestination);
        /*/path = findPath(currentDestination);
        /while (path == null){
            path = findPath(currentDestination)
        }
        if(path == null || path.Count == 0){
            Debug.Log("path is null");
            Debug.Log("destination is: " + currentDestination.ToString());
            path = findPath(currentDestination);
            yield return new WaitForSeconds(0.1f);
        } */
        moveCoroutine = StartCoroutine(autoMove(path));
        yield return null;
		
	}

    public void restartAutoMove()
    {

    }
    //returns the list of tile indexes along the path to the destinationIndex
    public IEnumerator findPathVoid(int tileThere)
    {
        int tileHere = hexa.GetTileAtLocalPos(transform.position);
        path = hexa.FindPath(tileHere, tileThere, 0, -1);

        yield return null;
    }
    //returns a new path list for quick variable allocation
    public List<int> findPathList(int tileThere)
    {
        int tileHere = hexa.GetTileAtLocalPos(transform.position);
        path = hexa.FindPath(tileHere, tileThere, 0, -1);

        return path;
    }

    //responsible for executing the movement over time
    public IEnumerator autoMove(List<int> path)
    {
        
        while (path == null)
        {
            /* Replaced by an event sent from setTileExtrude to autoMovement.checkIndexInPath() 

            //If the destination tile can't be crossed, remove it from the destination pool
            if(hexa.GetTileCanCross(path[(path.Count - 1)]) == false)
            {
                destinations.Remove(path[(path.Count - 1)]);
                StartCoroutine(findAutoMovementDestinations());
                break;
            }*/

            Debug.Log("path is null" + " " + gameObject.name); 

            yield return new WaitForSeconds(0.01f);
        }
        currentTile = path[0];
        Vector3 lastPos = transform.position;
        for (int i = 0; i < path.Count; i++)
        {
            tileIndex = i;
            //get position of next tile
            
            //get current position
            currentPos = transform.position;

            t = 0f;

            while (t < 1f)
            {
                //This fixes the destination position just fine
                destinationPos = hexa.GetTileCenter(path[i]);
                
                t += Time.deltaTime * autoMoveSpeed;
                transform.position = Vector3.Slerp(currentPos, destinationPos, t);

                Vector3 lookDir = transform.position - lastPos;
                //correct rotation to keep transform forward aligned with movement direction and transform up aligned with tile normal
                transform.rotation = Quaternion.LookRotation(lookDir, transform.position - hexa.transform.position);
                lastPos = transform.position;
                
                //this almost makes things work?
                //currentPos = transform.position;
                //above line not final

                yield return new WaitForSeconds (Time.deltaTime);
            }
            currentTile = path[i];
            //currentPos = transform.position;
        }
        stillMoving = false;
        //if autoWander is enabled, return to autoWander state
        /*if(wanderNow == true)
        {
           // Debug.Log("still wandering");
            thisUnit.stateMachine.ChangeState(new autoWander(thisUnit));
        }*/
        if(wanderNow == false && thisUnit.stateMachine.currentState.ToString() == "autoWander")
        {
           // Debug.Log("changing to idle now");
            thisUnit.stateMachine.ChangeState(new Idle(thisUnit));
        }
        //Debug.Log("destination reached, coroutine finishing");
    }
}
