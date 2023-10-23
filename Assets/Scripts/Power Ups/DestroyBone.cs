using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBone : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D col){
      
        if(col.gameObject.tag == "Bone"){
            Destroy(col.gameObject);
        }
    }
     void OnCollisionEnter2D(Collision2D col){
   
        if(col.gameObject.tag == "Bone"){
            Destroy(col.gameObject);
        }
    }

}
