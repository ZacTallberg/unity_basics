using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HexasphereGrid;

public class testUI : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
 /// <summary>
    /// This function is called when the object becomes enabled and active.
    /// </summary>
    void OnEnable()
    {
        Hexasphere.testNow += testText;
    }
    /// <summary>
    /// This function is called when the behaviour becomes disabled or inactive.
    /// </summary>
    void OnDisable()
    {
        Hexasphere.testNow -= testText;
    }
    public void testText(int index)
    {

    }
}
