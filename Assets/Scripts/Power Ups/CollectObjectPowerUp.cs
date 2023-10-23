using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectObjectPowerUp : MonoBehaviour
{
    [Header("PoweUp Settings")]

 
    public PowerUpBasic powerUpBasic;
    private PowerUpInventory inventory;
    public ObjectSpawn objectSpawn;
    
  


    private void Start() {
        powerUpBasic = GetComponent<PowerUpBasic>();
        inventory = FindObjectOfType(typeof(PowerUpInventory)) as PowerUpInventory;
        objectSpawn = FindObjectOfType(typeof(ObjectSpawn)) as ObjectSpawn;
    }



    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.CompareTag("Paddle")) {
      
            if(!powerUpBasic.isInventory){
                
            
           // UseItem();             

           


                    // Set the power-up as active
                
            }
            else{
                inventory.AddPowerUp(powerUpBasic, collision.GetComponent<CatPaddle>().player1);
            }
                    // Disable the collider so the power-up doesn't trigger again
                    GetComponent<Collider2D>().enabled = false;
                    GetComponent<SpriteRenderer>().enabled = false;
                
            

                
            
        }
    }

    public void UseItem(int playerID){
        StartCoroutine(powerUpBasic.DisplayText());
 
        objectSpawn.SpawnObject(powerUpBasic.IDItem, playerID);
      
    }
  
 
  
}