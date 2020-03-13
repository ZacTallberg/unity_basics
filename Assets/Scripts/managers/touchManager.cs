using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HexasphereGrid;

public class touchManager : MonoBehaviour
{

    //This class should handle all input and send events to any objects that need to do something
    //Current rules:
    //If TileTagInt == 2, that's a grass tile and shouldn't move
    //If IT_Utility.IsCursorOnUI(), that's a UI element and shouldn't move
   
    public adjustMenu menuObject;
    public Hexasphere hexa;
    Vector3 clickedTilePos;
    public int tileClicked;
    int tileTag;
    Vector2 inputPos;
   
   //Initialization functions
    void Start()
    {
        if (hexa == null) hexa = Hexasphere.GetInstance("Hexasphere");
        if (menuObject == null) menuObject = GameObject.Find("popUI").GetComponent<adjustMenu>();
    }
   
    void Update()
    {
        
    }
    void OnEnable()
    {
        hexa.OnTileClick += checkActionTile;
        IT_Gesture.onShortTapE += checkForGameobject;
    }
   
    void OnDisable()
    {
        hexa.OnTileClick -= checkActionTile;
        IT_Gesture.onShortTapE -= checkForGameobject;
    }
    
    //Event Declarations

    //Sends location of new menu position
    public delegate void newMenu(Vector3 position);
    public static event newMenu menuPos;
    public void moveMenuNow()
    {
        //Send event to adjustMenu()
        menuPos(clickedTilePos);
    }
    //Starts movement of robots
    public delegate void moveNow(int destination);
    public static event moveNow moveRobot;
    void sendMoveToRobots(int tileIndex)
    {
        ///Send event to MoveUnit
        moveRobot(tileIndex);
    }
    public void moveRobots()
    {
        //Send event to MoveUnit
        moveRobot(tileClicked);
    }
    //Other Functions
    void checkActionTile(int index)
    {
        //If your tap is over top a UI element, don't click the tile below it
        if(IT_Utility.IsCursorOnUI(inputPos))
        {
            Debug.Log("on UI");
            return;
        }
        tileTag = hexa.GetTileTagInt(index);
        tileClicked = index;
        clickedTilePos = hexa.GetTileCenter(index);
        //If your tap is over top a grass tile (tileTag == 2), move menu and know that it's a grass tile
        if(tileTag == 2)
        {
            moveMenuNow();
            Debug.Log("on Grass");
            return;
        }
        else{
            //Debug.Log(clickedTilePos);
            moveMenuNow();

            //sendMoveToRobots(tileClicked);
        }
        
    }
    
    //Checks for objects at the screen position that was tapped, reference it for use
    void checkForGameobject(Vector2 position)
    {
        
        inputPos = position;
        if (IT_Utility.GetHovered3DObject(position, Camera.main) != null)
        {
            GameObject foundObject = IT_Utility.GetHovered3DObject(position, Camera.main);
            
        }
    }
    
}
