using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.DualShock;

public class LightBarColor : MonoBehaviour
{

   
    // Start is called before the first frame update
    void Start()
    {
     
    }

    // Update is called once per frame
    void Update()
    {
          var gamepad = (DualShockGamepad)Gamepad.current;
        gamepad.SetLightBarColor(Color.black);
    }
}
