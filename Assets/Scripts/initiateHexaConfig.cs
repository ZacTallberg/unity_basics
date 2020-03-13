using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using HexasphereGrid;

[ExecuteInEditMode]
public class initiateHexaConfig : MonoBehaviour
{
    public HexasphereConfig hexa;
    // Start is called before the first frame update
    void Start()
    {
        if(hexa == null) hexa = gameObject.GetComponent<HexasphereConfig>();
        hexa.enabled = true;
    }
    
    // Update is called once per frame
    void Update()
    {
        if(hexa == null) hexa = gameObject.GetComponent<HexasphereConfig>();
        if (hexa.enabled == false) hexa.enabled = true;
    }
}
