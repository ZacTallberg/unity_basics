using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[ExecuteInEditMode]
public class fixedButton : MonoBehaviour
{
    public float width=100;
    public float height=40;
    public float updown=3;
    public float leftright=25;
    public sendUnit sendObj;
   void OnGUI()
    {
    

    if (GUI.Button (new Rect(Screen.width-(leftright*5),(Screen.height*(updown/50)),width,height), "Send robots"))
        {
        ButtonPressed();
        };
    }
     
    public delegate void sendObjNow();
    public static event sendObjNow sendInitiate;
    void ButtonPressed()
    {
        sendInitiate();
    }
}
