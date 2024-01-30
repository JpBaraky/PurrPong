using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonSound : MonoBehaviour
{
    [Header("Sound Clips")]
    public AudioClip buttonClickSound; // Assign your sound clip in the Inspector
    public AudioClip buttonSelectedSound;
    private Button button;
    public AudioSource audioSource;
    private bool selected;


    void Start()
    {
        button = GetComponent<Button>();
        audioSource = GetComponent<AudioSource>();
if(button != null){

     button.onClick.AddListener(PlayButtonClickSound);
}


    }
    void Update(){
        OnSelect();
    }

    public void PlayButtonClickSound()
    {
        if (buttonClickSound != null)
        {
           
            audioSource.PlayOneShot(buttonClickSound);
        }
    }
    public void OnSelect(){
        if(EventSystem.current.currentSelectedGameObject == this.gameObject)
        
        {
            if(!selected){
            audioSource.PlayOneShot(buttonSelectedSound);
            selected = true;
            }

        } else{
            selected = false;
        }

    }
}