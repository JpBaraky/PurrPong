using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseButtomReturnTittle : MonoBehaviour
{
    private changeScene ChangeScene;
    // Start is called before the first frame update
    void Start()
    {
        ChangeScene = FindObjectOfType<changeScene>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PauseButtonReturn(){
        ChangeScene.ChangeScene("TittleScreen");
    }
    
}
