using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HexasphereGrid;


public class harvestManager : MonoBehaviour
{

    public Dictionary<GameObject, float> robotDistance;    
    public List<GameObject> grasses;
    public Hexasphere hexasphere;
    public bool drawLine;
    //public Dictionary<int, GameObject> grasses;
    void OnEnable()
    {
        grass.harvestMe += addGrass;
    }
    void OnDisable()
    {
        grass.harvestMe -= addGrass;
    }

    void addGrass(GameObject grass)
    {
        grasses.Add(grass);
        checkAndRemove(grass);
        //grasses.Add(index, grass);
    }

    void checkAndRemove(GameObject grassCheck)
    {
        //GameObject tileAt = hexasphere.GetTileAtLocalPos(grassCheck.transform.position);

       // robotDistance.Add(tileAt, )
    }
    // Start is called before the first frame update
    void Start()
    {
    //if (hexasphere == null) hexasphere = GameObject.Find("Hexasphere").GetComponent<hexasphere>();
       //Dictionary<GameObject, float> robotDistance = new Dictionary<GameObject, float>();
    }

    // Update is called once per frame
    void Update()
    {
        if (drawLine == true)
        {

        }
    }
}
