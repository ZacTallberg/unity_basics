using UnityEngine;
 using UnityEngine.UI;
 
 [ExecuteInEditMode]
 public class CustomRenderQueue : MonoBehaviour {
 
     public UnityEngine.Rendering.CompareFunction comparison = UnityEngine.Rendering.CompareFunction.Always;
 
     public bool apply = false;
     public Image selfImage;
 
   /// <summary>
   /// This function is called when the object becomes enabled and active.
   /// </summary>
   void OnEnable()
   {
       adjustMenu.fadeNow += fade;
   }
   void OnDisable() {
       adjustMenu.fadeNow -= fade;
   }

    public void fade(float value){

        Color color = selfImage.color;
        color.a = value;
        selfImage.color = color;
    }
    void Start()
    {
        if (selfImage == null) selfImage = transform.GetComponent<Image>();
        apply = true;
    }
     private void Update()
     {
         if (apply)
         {
             apply = false;
             //Debug.Log("Updated material val");
             Image image = GetComponent<Image>();
             Material existingGlobalMat = image.materialForRendering;
             Material updatedMaterial = new Material(existingGlobalMat);
             updatedMaterial.SetInt("unity_GUIZTestMode", (int)comparison);
             image.material = updatedMaterial;
         }
     }
 }