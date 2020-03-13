using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HexasphereGrid;
public class turretSpawn : MonoBehaviour
{
    public GameObject turretObj;
    public touchManager touchM;
    public Hexasphere hexa;
    public setTileExtrude tileExtrude;

    // Start is called before the first frame update
    void Start()
    {
        if (touchM == null) touchM = GameObject.Find("_Managers").GetComponent<touchManager>();
        if (turretObj == null) turretObj = Resources.Load<GameObject>("turret");
        if (hexa == null) hexa = GameObject.Find("Hexasphere").GetComponent<Hexasphere>();
        if (tileExtrude == null) tileExtrude = GameObject.Find("extrude").GetComponent<setTileExtrude>();
        
    }
    
    //scripting object creation
    GameObject createTurret()
    {
        //zero'd v3 & Quaternion
        Vector3 zeroV = new Vector3(0,0,0);
        Quaternion zeroQ = new Quaternion(0,0,0,0);
        //instantiate turret with zero pos and rot
        GameObject turret = GameObject.Instantiate(turretObj, zeroV, zeroQ);
        //save collider for further modification
        Collider turretCol = turret.GetComponent<Collider>();
        //disable collider if enabled
        if(turretCol.enabled == true) turretCol.enabled = false;
        return turret;
    }

    public void spawnTurretNow()
    {
        StartCoroutine(spawnTurretCoroutine());
    }
    public IEnumerator spawnTurretCoroutine()
    {
        if(hexa.GetTileExtrudeAmount(touchM.tileClicked) != tileExtrude.extrudeFloat)
        {
            //If the tile we're spawning a turret on isn't extruded, wait to extrude it before continuing
            yield return tileExtrude.setHeightCoroutine();
        }
       
        //create new turret from function
        GameObject newTurret = createTurret();
        hexa.ParentAndAlignToTile(newTurret, touchM.tileClicked, 1, false, false, 1);   
        yield return null;
    }
}
