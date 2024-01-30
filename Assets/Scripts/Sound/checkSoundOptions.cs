using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkSoundOptions : MonoBehaviour
{
    private bool checkMark;
    public AudioSource audioSource;

    public AudioClip selectSound;
    private bool firstSound;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }


    public void playSound(){
        if(firstSound == false)
        {
            firstSound = true;
            return;
        }
 audioSource.PlayOneShot(selectSound);
    }
}
