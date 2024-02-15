using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
public class changeScene : MonoBehaviour
{
    public string targetScene;
    public bool isChangeScene;
    public float secondsToWait;
    private fadeBackground fadeBackground;
    
    // Start is called before the first frame update
    void Start()
    {
       fadeBackground = FindObjectOfType(typeof(fadeBackground)) as fadeBackground;
        if(isChangeScene) {
            StartCoroutine("changeSceneFadeOut");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(catOutOfBox.catIsOut) {
            catOutOfBox.catIsOut = false;
         StartCoroutine("changeSceneFadeOut");
        }
        if(isChangeScene) {
            isChangeScene = false;
         StartCoroutine("changeSceneFadeOut");
        }
        
    }
     IEnumerator changeSceneFadeOut() {
        yield return new WaitForSecondsRealtime(secondsToWait);
        fadeBackground.fadeIn();
        yield return new WaitWhile(() => fadeBackground.fume.color.a < 0.9f);
        yield return new WaitForEndOfFrame();        
         SceneManager.LoadScene(targetScene);
    }
    public void ButtonChangeScene(string sceneName) {
        targetScene= sceneName;
        if(EventSystem.current.currentSelectedGameObject.name == "New Game") {
            PlayerPrefs.DeleteKey("CurrentStage");
  
        }
        isChangeScene= true;
   
    }
    public void ChangeScene(String sceneName) {
      targetScene= sceneName;
         isChangeScene= true;
    }
    public void LoadLevel() {
        targetScene = PlayerPrefs.GetString("CurrentStage", "Stage 1");
        isChangeScene = true;

    }
    public void DestroyThis(String destroy) {
        Destroy(GameObject.Find(destroy));
    }

 
    
    
    
}