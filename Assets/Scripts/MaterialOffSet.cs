using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MaterialOffSet : MonoBehaviour
{
   [SerializeField] private float speed = 1f;
 
    private float offset;
 
    public Material material;
 

      
 
    void Update()
    {
        offset += speed * Time.deltaTime;
 
        // with both methods, the result is the same
        // material.mainTextureOffset = new Vector2(offset, offset);
        material.SetTextureOffset("_MainTex", new Vector2(offset, offset));
    }
}