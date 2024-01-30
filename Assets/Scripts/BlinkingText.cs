using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BlinkingText : MonoBehaviour
{
   public float blinkTime = 0.5f;
   private TextMeshProUGUI text;
   public bool isBlinking;
    void Start()
    {
         text = GetComponent<TextMeshProUGUI>();
      
    }

    // Update is called once per frame
    void Update()
    {
    if(!isBlinking){
      StartCoroutine(Blink());
      isBlinking = true;
    }
    }
   IEnumerator Blink(){
  
           text.enabled = false;
           yield return new WaitForSeconds(blinkTime);
           text.enabled = true;
           yield return new WaitForSeconds(blinkTime);
       
    isBlinking = false;
   }
}
