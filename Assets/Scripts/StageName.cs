using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageName : MonoBehaviour

    
{
    public TextMeshProUGUI stageText;
    public string stageName;
    public int flashCount = 3;
    public float flashInterval = 1.0f;
    public float displayDuration = 3f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DisplayText());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public IEnumerator DisplayText() {
        // Show the text
        stageText.enabled = true;
        stageName = SceneManager.GetActiveScene().name;
        stageText.text = stageName;
        
        // Flash the text
        for(int i = 0; i < flashCount; i++) {
            stageText.enabled = true;
            yield return new WaitForSeconds(flashInterval);
            stageText.enabled = false;
            yield return new WaitForSeconds(flashInterval);
        }

        // Wait for the remaining duration
        yield return new WaitForSeconds(displayDuration - (flashCount * 2 * flashInterval));

        // Hide the text
        stageText.enabled = false;
    }

}

