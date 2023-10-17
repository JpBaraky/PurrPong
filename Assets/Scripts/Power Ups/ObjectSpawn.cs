using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawn : MonoBehaviour
{
    private GameObject[] SpawnLocations;
    private ObjectsController objectController;    
    public GameObject spawnPreFab;
    public String spawnerTag;
        private bool isGoingRight;
    // Start is called before the first frame update

    public void SpawnObject(){
        if(SpawnLocations == null) {
            SpawnLocations = GameObject.FindGameObjectsWithTag(spawnerTag);
        }
         foreach (GameObject spawn in SpawnLocations)
        {
            
            GameObject spawnObject = Instantiate(spawnPreFab, spawn.transform.position, spawn.transform.rotation);
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
