using UnityEngine;
using System.Collections;
using System;
using System.Linq;
public class ExampleClass : MonoBehaviour
{
    // Creates a line renderer that follows a Sin() function
    // and animates it.

    public Color c1 = Color.yellow;
    public Color c2 = Color.red;
    public int lengthOfLineRenderer = 20;

    public GameObject fromObject;
    public GameObject toObject;
    public GameObject test1;
    public GameObject test2;
    public float speedFloat = 1f;
    public float widthMultiplier;
    public float alpha;

    public float distance;
    public bool line;
    void Start()
    {
        LineRenderer lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.widthMultiplier = 0.2f;
        lineRenderer.positionCount = lengthOfLineRenderer;

        // A simple 2 color gradient with a fixed alpha of 1.0f.
        float alpha = 1.0f;
        Gradient gradient = new Gradient();
        gradient.SetKeys(
            new GradientColorKey[] { new GradientColorKey(c1, 0.0f), new GradientColorKey(c2, 1.0f) },
            new GradientAlphaKey[] { new GradientAlphaKey(alpha, 0.0f), new GradientAlphaKey(alpha, 1.0f) }
        );
        lineRenderer.colorGradient = gradient;
    }
    //Script to update line targets and set lines from/to objects
    void updateLineTargets()
    {

    }
    void Update()
    {
        distance = Vector3.Distance(fromObject.transform.position, toObject.transform.position);
        LineRenderer lineRenderer = GetComponent<LineRenderer>();
        LineRenderer differentLine = GetComponent<LineRenderer>();
        if(line == false){    
        
        
            var t = Time.time;
            float speed = speedFloat;
            t = t*speedFloat;
            var points = new Vector3[lengthOfLineRenderer];
            for (int i = 0; i < lengthOfLineRenderer; i++)
            {
                points[i] = new Vector3(i * 0.5f, Mathf.Sin(i + t), 0.0f);
            }
            lineRenderer.SetPositions(points);

            lineRenderer.widthMultiplier = 0.2f;
        
            if (lengthOfLineRenderer < 0) lengthOfLineRenderer = Mathf.Abs(lengthOfLineRenderer);
            lineRenderer.positionCount = lengthOfLineRenderer + 20;
             float alpha = 1.0f;
            Gradient gradient = new Gradient();
            gradient.SetKeys(
            new GradientColorKey[] { new GradientColorKey(c1, 0.0f), new GradientColorKey(c2, 1.0f) },
            new GradientAlphaKey[] { new GradientAlphaKey(alpha, 0.0f), new GradientAlphaKey(alpha, 1.0f) }
        );
        lineRenderer.colorGradient = gradient;
         }
         if(line == true)
         {
             //LineRenderer lineRenderer = GetComponent<LineRenderer>();
            
            lineRenderer.SetPosition(0, fromObject.transform.position);
            lineRenderer.SetPosition(1, toObject.transform.position);
            lineRenderer.SetPosition(2, fromObject.transform.position);
            lineRenderer.SetPosition(3, test1.transform.position);
            lineRenderer.SetPosition(4, fromObject.transform.position);
            lineRenderer.SetPosition(5, test2.transform.position);
            //lineRenderer.SetPosition(6, fromObject.transform.position);
            //lineRenderer.SetPosition(7, test1.transform.position);
             float alpha = 1.0f;
            Gradient gradient = new Gradient();
            gradient.SetKeys(
            new GradientColorKey[] { new GradientColorKey(c1, 0.0f), new GradientColorKey(c2, 1.0f) },
            new GradientAlphaKey[] { new GradientAlphaKey(alpha, 0.0f), new GradientAlphaKey(alpha, 1.0f) }
        );
        lineRenderer.colorGradient = gradient;
         }
        // A simple 2 color gradient with a fixed alpha of 1.0f.
       
       
    }
}