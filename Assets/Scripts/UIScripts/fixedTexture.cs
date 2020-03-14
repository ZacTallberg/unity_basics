using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[ExecuteInEditMode]
public class fixedTexture : MonoBehaviour
{
    public float width=100;
    public float height=40;
    public float updown=3;
    public float leftright=25;
    public float floatVal;
    public Texture UI;

    void OnGUI() => GUI.DrawTexture(new Rect(Screen.width - (leftright * 5), (Screen.height * (updown / 50)), width, height), UI, ScaleMode.ScaleToFit, true, floatVal);
}