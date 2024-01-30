using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class randomPosition: MonoBehaviour {
    public GameObject catMom;
    public GameObject[] randomLocations;

    private int lastRandomNumber;
    private int randomNumber;



    public void Start() {
        randomLocations = GameObject.FindGameObjectsWithTag("Random Location");
        RandomPosition();
    }



    public void RandomPosition() {



        
              
                catMom.transform.position = randomLocations[Random.Range(0,randomLocations.Length)].transform.position;
            
        
      
          
           
        
        

    }
}


