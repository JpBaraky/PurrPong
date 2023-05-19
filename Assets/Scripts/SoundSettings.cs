using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SoundSettings: MonoBehaviour {
    public Slider musicSlider;
    public Slider effectsSlider;

    private AudioSource audioSource;

    private float savedMusicVolume;
    private float savedEffectsVolume;

    private void Awake() {
        audioSource = GetComponent<AudioSource>();
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
        if(audioSource != null) {
            audioSource.volume = savedMusicVolume;
        }
    }

    private void UpdateSliders() {
        musicSlider.value = savedMusicVolume;
        effectsSlider.value = savedEffectsVolume;
    }

    private void SavePlayerPrefs() {
        PlayerPrefs.SetFloat("MusicVolume",savedMusicVolume);
        PlayerPrefs.SetFloat("EffectsVolume",savedEffectsVolume);
        PlayerPrefs.Save();
    }

    private void OnApplicationQuit() {
        SavePlayerPrefs();
    }

    private void OnSceneChange(Scene currentScene,Scene nextScene) {
        SavePlayerPrefs();
    }

    private void OnEnable() {
        musicSlider.value = savedMusicVolume;
        effectsSlider.value = savedEffectsVolume; 
    }

    private void OnDisable() {
        SceneManager.activeSceneChanged -= OnSceneChange;
        SavePlayerPrefs();
    }
    public void GetVolume() {
        Debug.Log(PlayerPrefs.GetFloat("EffectsVolume",0.5f));
        Debug.Log(PlayerPrefs.GetFloat("MusicVolume",0.5f));
    }
}