using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PowerUpInventory : MonoBehaviour
{
    public List<PowerUpBasic> inventoryPlayer1;
    public List<PowerUpBasic> inventoryPlayer2;
    private Sprite itemIconPlayer1;
    private Sprite itemIconPlayer2;
    public SpriteRenderer itemIconRendererPlayer1;
    public SpriteRenderer itemIconRendererPlayer2;
    

    
   
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        itemIconRendererPlayer1.sprite = itemIconPlayer1;
        itemIconRendererPlayer2.sprite = itemIconPlayer2;   
    }
    public void AddPowerUp(PowerUpBasic collectedPowerUp, bool isPlayer1)
    {
        
    
        if (isPlayer1){
            if(inventoryPlayer1.Count > 0){
                inventoryPlayer1[0].SendMessage("UseItem");
             inventoryPlayer1.RemoveAt(0);
            }
             inventoryPlayer1.Add(collectedPowerUp);
             itemIconPlayer1 = collectedPowerUp.powerUpSprite.sprite;
        }else{
             if(inventoryPlayer2.Count > 0) {
                inventoryPlayer2[0].SendMessage("UseItem");
             inventoryPlayer2.RemoveAt(0);
             }
             inventoryPlayer2.Add(collectedPowerUp);
             itemIconPlayer2 = collectedPowerUp.powerUpSprite.sprite;
            
    
             
        }
        
        
       
    }
    public void Use(bool isPlayer1){
            if (isPlayer1 && inventoryPlayer1.Count > 0 ){
            inventoryPlayer1[0].SendMessage("UseItem");
             inventoryPlayer1.RemoveAt(0);
             itemIconPlayer1 = null;
        }else{
            if(!isPlayer1 && inventoryPlayer2.Count > 0  ){
             inventoryPlayer2[0].SendMessage("UseItem");
             inventoryPlayer2.RemoveAt(0);
             itemIconPlayer2 = null;
            }
        }
           
        
    }
      
     
        
    
}
