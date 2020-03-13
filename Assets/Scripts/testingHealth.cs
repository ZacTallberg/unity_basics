using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testingHealth : MonoBehaviour
{

    public Unit selfUnit;
    //Current health
    public float healthCurrent;
    //Max health
    public float healthTotal;
    //float that fades color based on currentHealth/healthPercentage
    public float healthPercentage;

    public Color selfColor;
    public Material selfMaterial;
    public bool invincible;

    // Start is called before the first frame update
    void Start()
    {
        if (selfMaterial == null) selfMaterial = gameObject.GetComponent<Renderer>().material;
        if (selfColor != selfMaterial.GetColor("_Color")) selfColor = selfMaterial.GetColor("_Color");
        if (selfUnit == null) selfUnit = gameObject.GetComponent<Unit>();
    }

    //When called, decrements hp according to how long the laser was attacking me
    public void attackMeNow(float multiplier)
    {
        if (!invincible) healthCurrent -= Time.deltaTime * multiplier;
    }
    public bool testingHPReduction;
    void Update()
    {
        //Testing reduction in hp to change cube color 
        if (testingHPReduction == true)
        {
            healthCurrent -= Time.deltaTime * 20;
        }

        //If current health is lower than 0, destroy this unit
        if(healthCurrent <= 0)
        {
            selfUnit.destroyMe();
        }
        else{
            healthPercentage = healthCurrent/healthTotal;
            selfColor = Color.Lerp(Color.red, Color.green, healthPercentage);
            selfMaterial.SetColor("_Color", selfColor);
        }
    }
    public void removeMyHP(float multiplier)
    {
        if (healthCurrent >= 0) healthCurrent -= multiplier*Time.deltaTime;
    }
}
