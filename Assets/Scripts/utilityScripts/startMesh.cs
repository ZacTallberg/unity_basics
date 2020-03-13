using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class startMesh : MonoBehaviour
{

    public MeshMerger meshObject;
    // Start is called before the first frame update
    void Start()
    {
        if (meshObject == null)
        {
            meshObject = GetComponent<MeshMerger>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
