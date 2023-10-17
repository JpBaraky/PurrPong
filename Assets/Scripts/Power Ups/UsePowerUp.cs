using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsePowerUp : MonoBehaviour
 
{
    public PowerUpInventory itensInventory; 
    public CatPaddle catPaddlePlayer2;

    // Start is called before the first frame update
    void Start()
    {
        itensInventory = FindObjectOfType<PowerUpInventory>(); 

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1") && itensInventory.inventoryPlayer1 != null){ //Checks if player 1 has an item and has pressed the button to use it 
            itensInventory.Use(true);

        }
        if(itensInventory.inventoryPlayer2 != null && !catPaddlePlayer2.isPlayer){
             itensInventory.Use(false);
        }else{
        if(Input.GetButtonDown("Fire2") && itensInventory.inventoryPlayer2 != null){
            itensInventory.Use(false);
        }
        }

        
    }
}
