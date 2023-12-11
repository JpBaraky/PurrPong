using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawn : MonoBehaviour
{
    private GameObject[] SpawnLocations;
    private ObjectsController objectController;    
    public GameObject[] spawnPreFab;
    public String[] spawnerTagPlayer1;
    public String[] spawnerTagPlayer2;
    public bool[] isToFollowPlayer;
    public Transform[] playersPosition;
    private bool isGoingRight;
    public bool[] dogGoingtoTheBone;
    public GameObject[] extraSpawnStuff;
      

void Start(){

    
}
    public void SpawnObject(int itemToSpawn, int playerID){
        
        if(playerID == 1){
            SpawnLocations = GameObject.FindGameObjectsWithTag(spawnerTagPlayer1[itemToSpawn]);
        } else{
            SpawnLocations = GameObject.FindGameObjectsWithTag(spawnerTagPlayer2[itemToSpawn]);
        }
     
           
      
         foreach (GameObject spawn in SpawnLocations)
        {
            if(isToFollowPlayer[itemToSpawn]){
            GameObject spawnObject = Instantiate(spawnPreFab[itemToSpawn], new Vector3 (spawn.transform.position.x, playersPosition[playerID].position.y, spawn.transform.position.z),  spawn.transform.rotation);
            objectController = spawnObject.GetComponent<ObjectsController>();   
            if(spawn.transform.position.x < 0){
                isGoingRight = true;
            } else{
                isGoingRight = false;
            }

            spawnObject.GetComponent<SpriteRenderer>().flipX = !isGoingRight;
            if(dogGoingtoTheBone[itemToSpawn] && playerID == 1){
                Instantiate(extraSpawnStuff[itemToSpawn], new Vector3 ( playersPosition[playerID].position.x + 1f, playersPosition[playerID].position.y,  playersPosition[playerID].position.z), spawn.transform.rotation);
                spawnObject.GetComponent<ObjectsController>().downDistance =  Math.Abs(spawn.transform.position.x) + Math.Abs(playersPosition[playerID].position.x) - 1.5f;
            }
            if(dogGoingtoTheBone[itemToSpawn] && playerID == 2){
                spawnObject.GetComponent<ObjectsController>().upDistance =   Math.Abs(spawn.transform.position.x) + Math.Abs(playersPosition[playerID].position.x) - 1.5f;
                Instantiate(extraSpawnStuff[itemToSpawn], new Vector3 ( playersPosition[playerID].position.x - 1f, playersPosition[playerID].position.y,  playersPosition[playerID].position.z), spawn.transform.rotation);
            }

            objectController.movingUp = isGoingRight;   
            }else{
            GameObject spawnObject = Instantiate(spawnPreFab[itemToSpawn], spawn.transform.position, spawn.transform.rotation);
            
            objectController = spawnObject.GetComponent<ObjectsController>();
            if(spawn.transform.position.x < 0){
                isGoingRight = true;
            } else{
                isGoingRight = false;
            }

            spawnObject.GetComponent<SpriteRenderer>().flipX = !isGoingRight;
            objectController.movingUp = isGoingRight;
            }
        }
    }
}
