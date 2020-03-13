using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HexasphereGrid;
public class Unit : MonoBehaviour
{
    public StateMachine stateMachine = new StateMachine();

    public autoMovement selfAuto;
    public MoveUnit selfMove;
    public sendUnit selfSend;
    public SetSelfColor selfcolor;
    public Hexasphere hexa;
    
    //[Tooltip("The current destination of our unit")]
    //public int destination;
   // public int tileIndex;
    //public Vector3 destinationPos;
    //public Vector3 currentPos;
    [Tooltip("The reference to the tile on which this unit currently resides")]
	public int currentTile;
  
   //[Tooltip("How quickly this unit moves between tiles")]
	//public float moveSpeed;
    [Tooltip("Float to adjust the transform to keep the bottom of the unit on the surface")]
    public float heightAdjust;
    //Coroutine for easy stopping/starting of coroutine
    //private Coroutine moveCoroutine;
    //public List<int> path;
    //public float t;
    public delegate void removeFromTurretList(Transform self);
    public static event removeFromTurretList removeMe;
    void Start()
    {
        if (selfAuto == null) selfAuto = GetComponent<autoMovement>();
        if (selfMove == null) selfMove = GetComponent<MoveUnit>();
        if (selfSend == null) selfSend = GetComponent<sendUnit>();
        if (hexa == null) hexa = Hexasphere.GetInstance("Hexasphere");
        //set height adjustment to keep object flush with surface
        heightAdjust = GetComponent<Renderer>().bounds.size.y;
        //get current tile at position, parent and set object alignment
        currentTile = hexa.GetTileAtLocalPos(transform.position);
        //hexa.ParentAndAlignToTile(gameObject, currentTile, heightAdjust, true, false, 1);
        //set initial state as idle
        stateMachine.ChangeState(new Idle(this));
    }
    void Update()
    {
        stateMachine.Update();
    }
    void OnEnable()
    {
        //hexa.OnTileClick += tileClicked;
        
    }
    void OnDisable()
    {
        //hexa.OnTileClick -= tileClicked;
        
    }
    public void destroyMe()
    {
        removeMe(gameObject.transform);
        Destroy(gameObject, 0.1f);
    }
    //Starts state change from clicked tile event
    /*public void tileClicked(int tileIndex)
    {
        destination = tileIndex;
       // Debug.Log("starting state change from Unit");
        stateMachine.ChangeState(new MoveToDestination(this));
    }*/

      
    //responsible for on-click movement
    /*public void startMove(int destinationInt)
    {
       // destinationIndex = destinationInt;
        path = findPath(destinationInt);
       // Debug.Log("destination index: " + destinationIndex.ToString());
        moveCoroutine = StartCoroutine(move(path));
    }
    public void stopMove()
    {
        if(moveCoroutine != null)
        {
            StopCoroutine(moveCoroutine);
        }
    }*/
     
    
    /*public IEnumerator findPath(int tileThere)
    {
        int tileHere = hexa.GetTileAtLocalPos(transform.position);
        path = hexa.FindPath(tileHere, tileThere, 0, -1);
        //Set all tiles on the path to the color black
        (int i = 0; i < tiles.Count; i++)
        {
            hexa.SetTileColor(tiles[i], Color.black, false);
        }
        return null;
    }*/
    /*public List<int> findPath(int tileThere)
    {
        //Get the index of the tile we're currently at
        int tileHere = hexa.GetTileAtLocalPos(transform.position);
        //Get a list of all the tiles from here to there
        List<int> tiles = hexa.FindPath(tileHere, tileThere, 0, -1);

        

        return tiles;
    }*/
    //responsible for executing the movement over time
    /*public IEnumerator move(List<int> path)
    {
        /*while (path == null)
        {
            Debug.Log("path is null");
            yield return new WaitForSeconds(0.01f);
        }
        //yield return StartCoroutine(findPath(destinationIndex));
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
                
                t += Time.deltaTime * moveSpeed;
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
        
       // Debug.Log("destination reached, coroutine finishing");
}*/
   
   public void updatePositions()
   {
       
   }
}