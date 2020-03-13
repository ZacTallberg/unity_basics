using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public adjustMenu menu;
    public List<Image> popUIChildren;
    Coroutine fadeCo;
    GameObject popUI;
    
    public void OnEnable() {
        //adjustMenu.fadeNow += startFade;
    }
    public void OnDisable(){
        //adjustMenu.fadeNow -= startFade;
    }
    // Start is called before the first frame update
    void Start()
    {
        if(menu == null) menu = GameObject.Find("popUI").GetComponent<adjustMenu>();
        if(popUI == null) popUI = GameObject.Find("popUI");
        for (int i = 0; i < popUI.transform.childCount; i++)
        {
            Image child = popUI.transform.GetChild(i).GetComponent<Image>();
            //CanvasGroup childCanvas = child.GetComponent<CanvasGroup>();
            popUIChildren.Add(child);
        }
    }

    public void startFade()
    {
        menu.outsideMin = true;
       fadeCo = StartCoroutine(fadeNow());
    }

    public IEnumerator fadeNow()
    {
        while (menu.outsideMin == true){
                float distance = menu.distanceFrom - menu.minDistance;
                //calculate percentage past the min distance
                float fade = 100 - ((100*distance)/(menu.maxDistance - menu.minDistance));
                //fadeNow(fade);
                Debug.Log(fade);

                for (int i = 0; i < popUIChildren.Count; i++)
                {
                    //Image childCanvas = popUIChildren[i].GetComponent<Image>();
                    float fadeP = fade/100;
                    Color color = popUIChildren[i].color;
                    color.a = fadeP;
                    popUIChildren[i].color = color;
                yield return new WaitForSeconds(Time.deltaTime);
                }

        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
