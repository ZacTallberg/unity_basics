using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dynamicMenu : MonoBehaviour
{
    public adjustMenu adjustMenuScript;
    private void OnEnable() {
        adjustMenu.disableMenu += checkInputPos;
    }
    private void OnDisable() {
        adjustMenu.disableMenu -= checkInputPos;
    }

    void checkInputPos()
    {
        StartCoroutine(checkMouseCursor());
    }
     public IEnumerator checkMouseCursor()
    {
        while(adjustMenuScript.selfCanvas.enabled == true)
        {
            // RaycastHit hit;
            // Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            GameObject testingUI = IT_Utility.GetHoveredUIElement(Input.mousePosition);
            // if (Physics.Raycast(ray, out hit)) {
            //     Transform objectHit = hit.transform; 
            //     Debug.Log(objectHit);
            // }
            if(testingUI)
            {
                Debug.Log(testingUI);
            }
            yield return new WaitForSeconds(0.1f);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        if(adjustMenuScript == null) adjustMenuScript = GameObject.Find("popUI").GetComponent<adjustMenu>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
