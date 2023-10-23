using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
   
   public float rotateSpeed = 200f;
    // Update is called once per frame
    void Update()
    {
        GetComponent<Transform>().Rotate(0, 0, rotateSpeed * Time.deltaTime);
    }
}
