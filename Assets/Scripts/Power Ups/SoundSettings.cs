using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class SoundSettings: MonoBehaviour {
    public Slider musicSlider;
    public Slider effectsSlider;
    public Toggle Scanlines;

    private AudioSource audioSource;

    public float savedMusicVolume;
    public float savedEffectsVolume;
    public bool scanLines;
    private string currentSceneName;
    public List<AudioSource> allAudioSources = new List<AudioSource>();

    public AudioClip[] audioClips;
    float currentVolumeEffect;
        float currentVolumeMusic;


    private void Awake() {
        audioSource = GetComponent<AudioSource>();
        currentSceneName = SceneManager.GetActiveScene().name;
    }

    

    void Update() {
         
        if (currentSceneName != SceneManager.GetActiveScene().name)
        {
            if(SceneManager.GetActiveScene().name == "Options"){
            musicSlider = GameObject.Find("MusicSlider").GetComponent<Slider>();
            effectsSlider = GameObject.Find("EffectsSlider").GetComponent<Slider>();
            Scanlines = GameObject.Find("ScanLinesToogle").GetComponent<Toggle>();
            LoadVolumeSettings();
             UpdateSliders();
            }
            currentSceneName = SceneManager.GetActiveScene().name;
        }
        if(Scanlines != null || musicSlider != null || effectsSlider != null)
        {
        
        
            scanLines = Scanlines.isOn;
            savedMusicVolume = musicSlider.value;
            savedEffectsVolume = effectsSlider.value;

        }
        CheckForNewAudioSources();
        SavePlayerPrefs();
    }
    
    private void Start() {
        LoadVolumeSettings();
        UpdateSliders();
       
       
    }

    public void SetMusicVolume(float volume) {
        if(audioSource != null) {
            audioSource.volume = volume;
        }
            savedMusicVolume = volume;
            SavePlayerPrefs();
        
    }

    public void SetEffectsVolume(float volume) {
        if(audioSource != null) {
            audioSource.volume = volume;
        }
        savedEffectsVolume = volume;
        SavePlayerPrefs();
    }

    private void LoadVolumeSettings() {
        savedMusicVolume = PlayerPrefs.GetFloat("MusicVolume",0.5f);
        savedEffectsVolume = PlayerPrefs.GetFloat("EffectsVolume",0.5f);
        scanLines = PlayerPrefs.GetInt("ScanLines",1) == 1 ? true : false;
        if(audioSource != null) {
            audioSource.volume = savedMusicVolume;
        }
    }

    private void UpdateSliders() {
         if(musicSlider != null && effectsSlider != null && Scanlines != null)
     {
        musicSlider.value = savedMusicVolume;
        effectsSlider.value = savedEffectsVolume;
        Scanlines.isOn = scanLines;
     }
    }

    private void SavePlayerPrefs() {
        PlayerPrefs.SetFloat("MusicVolume",savedMusicVolume);
        PlayerPrefs.SetFloat("EffectsVolume",savedEffectsVolume);
        PlayerPrefs.SetInt("ScanLines",scanLines ? 1 : 0);
        PlayerPrefs.Save();
    }

    private void OnApplicationQuit() {
        SavePlayerPrefs();
    }

    private void OnSceneChange(Scene currentScene,Scene nextScene) {
        SavePlayerPrefs();
    }

    private void OnEnable() {
     if(musicSlider != null && effectsSlider != null && Scanlines != null)
     {
        musicSlider.value = savedMusicVolume;
        effectsSlider.value = savedEffectsVolume;
        Scanlines.isOn = scanLines;
     }
      
    }

    private void OnDisable() {
        SceneManager.activeSceneChanged -= OnSceneChange;
        SavePlayerPrefs();
    }
    public void GetVolume() {
        Debug.Log(PlayerPrefs.GetFloat("EffectsVolume",0.5f));
        Debug.Log(PlayerPrefs.GetFloat("MusicVolume",0.5f));
    }
    public void SetScanLines() {
        scanLines = Scanlines.isOn;
    }
     void CheckForNewAudioSources()
    {
        AudioSource[] currentAudioSources = FindObjectsOfType<AudioSource>();
        
        if(currentVolumeEffect != PlayerPrefs.GetFloat("EffectsVolume",0.5f) )
        {
            currentVolumeEffect = PlayerPrefs.GetFloat("EffectsVolume",0.5f);
        
            foreach (AudioSource audioSource in currentAudioSources)
        {
            audioSource.volume = savedEffectsVolume;
            //Debug.Log("Effects Volume Changed" + currentVolumeEffect);
        }
        }

        foreach (AudioSource audioSource in currentAudioSources)
        {
            // Check if the AudioSource is not in the list
            if (!allAudioSources.Contains(audioSource))
            {
                if(audioSource.gameObject.CompareTag("Music")){
                     if(SceneManager.GetActiveScene().name == "Options"){
                    audioSource.volume = savedMusicVolume;
                     }
                }else{
                // This is a new AudioSource
                
                audioSource.volume = savedEffectsVolume;

                // Add it to the list to keep track of it
                allAudioSources.Add(audioSource);
                }
            }
        }
                // Remove null objects from the list
        allAudioSources.RemoveAll(item => item == null);

    }
   
}