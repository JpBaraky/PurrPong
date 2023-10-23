using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UsePowerUp : MonoBehaviour
 
{
    public PowerUpInventory itensInventory; 
    public CatPaddle catPaddlePlayer2;

    [SerializeField]

    private InputActionReference UseItem1, UseItem2;

    // Start is called before the first frame update
    void Start()
    {
        itensInventory = FindObjectOfType<PowerUpInventory>(); 

    }

    // Update is called once per frame
    void Update()
    {
      
        if(UseItem1.action.triggered || Input.GetButtonDown("Player1Item") && itensInventory.inventoryPlayer1 != null){ //Checks if player 1 has an item and has pressed the button to use it 
           itensInventory.Use(true);

        

       }
        if(UseItem2 != null  && !catPaddlePlayer2.isPlayer){
             itensInventory.Use(false);
        }else{
        if(UseItem2.action.triggered || Input.GetButtonDown("Player2Item")  && itensInventory.inventoryPlayer2 != null){
            itensInventory.Use(false);
        }
        }

        
    }
  
}
