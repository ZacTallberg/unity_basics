using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HexasphereGrid;

public class dragWall : MonoBehaviour
{

    public adjustMenu adjustM;

    public bool stillTouching;
    public Hexasphere hexa;
    public Tile testTile;
    public int tileIndex;


    //When button for creating a wall is pressed,
    // Start is called before the first frame update
    void Start()
    {
         if(adjustM == null) adjustM = GameObject.Find("popUI").GetComponent<adjustMenu>();
        if(hexa == null) hexa = GameObject.Find("Hexasphere").GetComponent<Hexasphere>();
    }

    // Update is called once per frame
    void Update()
    {
       
        if(testTile != null) Debug.Log(testTile);
    }

 void OnEnable()
 {
    IT_Gesture.onTouchDownPosE += touchDown;
    IT_Gesture.onTouchUpPosE += touchUp;
    hexa.OnTileMouseOver += testTileVoid;
 }

 void OnDisable()
 {
     IT_Gesture.onTouchDownPosE -= touchDown;
     IT_Gesture.onTouchUpPosE -= touchUp;
     hexa.OnTileMouseOver -= testTileVoid;
 }
    public void testTileVoid(int index)
    {
        if (testTile != hexa.tiles[index]) testTile = hexa.tiles[index];
        if (tileIndex != index) tileIndex = index;
    }
    public void touchUp(Vector2 position)
    {
        if (stillTouching == true) stillTouching = false;
    }
    public void touchDown(Vector2 position)
    {
        if(stillTouching == false) stillTouching = true;
        StartCoroutine(touchDownCo(position));
    }

    public IEnumerator touchDownCo(Vector2 touchPos)
    {
        
        while (stillTouching == true)
        {

        }

        yield return null;
    }
    public void test()
    {

    }


}
