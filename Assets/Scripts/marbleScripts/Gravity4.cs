using UnityEngine;
 using System.Collections;
 
 public class Gravity4 : MonoBehaviour {
     public float speed = 1.0f;
     private Transform transRef;
     
     void Start() {
         transRef = new GameObject().transform;
         transRef.position = transform.position;
         transRef.rotation = transform.rotation;
         transform.parent = transRef;
     }
  
     void Update() {
         float horz = Input.GetAxis ("Horizontal");
         float vert = Input.GetAxis ("Vertical");
         
 
         if (horz != 0.0f || vert != 0.0f) {
             float angle = Mathf.Atan2 (vert, horz) * Mathf.Rad2Deg + 90.0f;
             transform.localRotation = Quaternion.AngleAxis (angle, Vector3.up);
             transRef.position += transform.forward * speed * Time.deltaTime * Mathf.Clamp01(Mathf.Sqrt(horz * horz + vert * vert));
             Attract ();
             }
     }
  
     public void Attract() {
         Ray _ray = new Ray(transRef.position, -transRef.up);
         RaycastHit hit;
  
         if (Physics.Raycast(_ray, out hit, 50.0f)) {
             transRef.position = hit.point;
             transRef.rotation = Quaternion.FromToRotation(transRef.up, hit.normal) * transRef.rotation;
             transRef.Translate (Vector3.up * 0.5f);  // Bring character up off surface
         }
     }    
 }