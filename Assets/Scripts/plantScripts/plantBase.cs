using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HexasphereGrid;

public class plantBase : MonoBehaviour
{
    public Hexasphere hexa;
    public float growTimeScale;
    public float growTime;
    public int growthStage;
    public float growthTotal;
    public bool harvestable;
    public List<float> stagesList;
    public Coroutine growing;
    public bool isGrowing;
    public int ageInt;

    // Start is called before the first frame update
    void Start()
    {
        if (hexa == null) hexa = Hexasphere.GetInstance("Hexasphere");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
