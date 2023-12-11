using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class UsePowerUp : MonoBehaviour
 
{
    public PowerUpInventory itensInventory; 
    public CatPaddle catPaddlePlayer2;
    public Animator[] animators;
    
    [Header("Sound")]
    private AudioSource audioSource;
    public AudioClip noItemSound;
    [SerializeField]

    private InputActionReference UseItem1, UseItem2;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        itensInventory = FindObjectOfType<PowerUpInventory>(); 

    }

    // Update is called once per frame
    void Update()
    {
      if(!catPaddlePlayer2.GetComponent<EnemyPaddleController>().gameStarted){
        return;
      }
      if(Input.GetButtonDown("Player1Item") && itensInventory.inventoryPlayer1.Count == 0 ){
            audioSource.PlayOneShot(noItemSound);
        }else {
        if(UseItem1.action.triggered || Input.GetButtonDown("Player1Item") && itensInventory.inventoryPlayer1 != null && itensInventory.inventoryPlayer1.Count > 0){ //Checks if player 1 has an item and has pressed the button to use it 
           itensInventory.Use(true);
           animators[0].SetTrigger("Cat Attack");

        

       } }
        if(itensInventory.inventoryPlayer2.Count > 0  && !catPaddlePlayer2.isPlayer){
            Debug.Log("test");
             itensInventory.Use(false);
             animators[1].SetTrigger("Cat Attack");
        }else{
        if(UseItem2.action.triggered || Input.GetButtonDown("Player2Item")  && itensInventory.inventoryPlayer2 != null && itensInventory.inventoryPlayer2.Count > 0){
            itensInventory.Use(false);
            animators[1].SetTrigger("Cat Attack");
        }
        else{
            if(Input.GetButtonDown("Player2Item")){
            audioSource.PlayOneShot(noItemSound);
        }
        }
        }

        
    }
  
}
