using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicInterScenes : MonoBehaviour
{
    private GameObject[] MusicObjects;
    public string[] SceneNames;
    private string currentScene;


    // Start is called before the first frame update
    void Start()
    {
        MusicObjects = GameObject.FindGameObjectsWithTag("Music"); //Destroy 1 game object with the tag music if there is more than one on the Scene.
        if(MusicObjects.Length > 1){
            Destroy(MusicObjects[1]);
        }
        if(SceneNames.Any(SceneNames => SceneManager.GetActiveScene().name == SceneNames )){ //compares all inside the array with the current scene name. And destroy this Object if it goes to another Scene.
            DontDestroyOnLoad(gameObject);
        }
        else{
            Destroy(gameObject);
        }
    currentScene = SceneManager.GetActiveScene().name;

    }

    // Update is called once per frame
    void Update()
    {
        if(currentScene != SceneManager.GetActiveScene().name){
            currentScene = SceneManager.GetActiveScene().name;
       if(currentScene == SceneNames[0]){

       }
       else{
        if(currentScene == SceneNames[1]){

        }
        else{
            Destroy(gameObject); //Destroy this Object if it goes to another Scene.
        }
       }
       
       }
    }
    
}
