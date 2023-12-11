using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderChangeVolume : MonoBehaviour
{
    public bool firstTimePlay = false;
    public AudioSource soundEffect;
    public void ChangeVolumeSound(){
        if(soundEffect != null && !soundEffect.isPlaying){
    
            if(firstTimePlay){
        
            soundEffect.Play();
            }
            else{
            
                firstTimePlay = true;
            }
        }
    }
}
