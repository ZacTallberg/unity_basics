using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class toggleWander : MonoBehaviour
{
    public List<Transform> cubes;
    // Start is called before the first frame update
    public void togglewanderNow()
    {
        for (int i = 0; i < cubes.Count; i++)
        {
            bool wander = cubes[i].GetComponent<autoMovement>().wanderNow;
            wander = !wander;
            cubes[i].GetComponent<autoMovement>().wanderNow = wander;
        }
    }
    void Start()
    {
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            cubes.Add(gameObject.transform.GetChild(i));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
