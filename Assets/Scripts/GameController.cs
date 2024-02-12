using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public enum GameState {
    Playing,
    Paused,
    EndOfMatch,
}


public class GameController: MonoBehaviour {
    private changeScene changeScene;
    private string currentSceneName;
    [Header("UI")]
    public TextMeshProUGUI PlayerWon;
    public TextMeshProUGUI PerfectGame;
    public TextMeshProUGUI PressToStart;
    private bool StartBlinking;
    public GameObject pauseMenu;
    [Header("Gameplay")]
    public GameState gameState;
    private GameState oldGameState;
    public Transform Paddle1, Paddle2;
    public string[] Stages;
    public bool singlePlayer;
    private Ball ballScript;
    private bool canProgress;
    private bool matchEnded;
    public int currentLevel = 0;
  
    [Header("Sound and ScreenSettings")]
    
    public AudioSource SoundEffects;
    private SoundSettings soundSettings;
    public AudioSource Music;
    public AudioClip player1Wins, player2Wins, perfectGame;
    public ScanlinesEffect GameCameraScanline;

   
 
    
   
    [SerializeField]

    private InputActionReference PauseButton;
    
    void Start() {
        changeScene = GetComponent<changeScene>();
        soundSettings = GetComponent<SoundSettings>();
        ballScript = FindObjectOfType(typeof(Ball)) as Ball;
        if(SoundEffects != null && Music != null){
    SoundEffects.volume = PlayerPrefs.GetFloat("EffectsVolume",0.5f);
        Music.volume = PlayerPrefs.GetFloat("MusicVolume",0.5f);
        }
    
        gameState = GameState.Playing;
        GameCameraScanline = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<ScanlinesEffect>();
        if(GameCameraScanline != null){
            GameCameraScanline.enabled = soundSettings.scanLines;
        }
        currentSceneName = SceneManager.GetActiveScene().name;
        if(currentSceneName.Substring(0, 4) == "Stage 1"){
        pauseMenu.SetActive(false);
        }
    }

    void Update() {
 
                 

            
       
        

         if (currentSceneName != SceneManager.GetActiveScene().name)
        {
            
            if(SceneManager.GetActiveScene().name.Substring(0,4) == "Stage"){
                pauseMenu = GameObject.FindGameObjectWithTag("Pause Menu");
               
                 pauseMenu.SetActive(false);
                 Paddle1 = GameObject.Find("Paddle1").transform;
                 Paddle2 = GameObject.Find("Paddle2").transform;
            }
             ballScript = FindObjectOfType(typeof(Ball)) as Ball;
            GameCameraScanline = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<ScanlinesEffect>();
                  currentSceneName = SceneManager.GetActiveScene().name;
            
        }
        if(GameCameraScanline != null && soundSettings != null){
        GameCameraScanline.enabled = soundSettings.scanLines;
        }
        
        if(Input.GetButtonDown("Pause")) {
            if(gameState == GameState.Playing) {
                PauseGame();
            } else {
                if(gameState == GameState.Paused) {
                    ResumeGame();
                }
            }
             
            }
        
        if(gameState == GameState.Playing) {
       EndOfMatch();
        }
        if(gameState == GameState.EndOfMatch){
        EndOfMatchButton();
        }
    }

    public void PauseGame() {
        oldGameState = gameState;
        gameState = GameState.Paused;
        Time.timeScale = 0f; // This will pause all animations, physics, etc.
        pauseMenu.SetActive(true);
        SelectGameobject(GameObject.Find("Resume Game Button"));
    }

    public void ResumeGame() {
        gameState = oldGameState;
        Time.timeScale = 1f; // This will resume the normal time scale.
        pauseMenu.SetActive(false);
    }
    void EndOfMatch() {
    
        if(ballScript == null || Paddle1 == null || Paddle2 == null) {
            return;
        } 
        if(!matchEnded){
            if(ballScript.player1Score >= 5) {
                canProgress = true;
            PlayerWon.gameObject.SetActive(true);
            PlayerWon.text = "Player 1 Won!!!";
            StartCoroutine(PlayEndOfMatchSound(player1Wins));         
            gameState = GameState.EndOfMatch;
            matchEnded = true;
            
        } else if(ballScript.player2Score >= 5) {
            canProgress = true;
            PlayerWon.gameObject.SetActive(true);
            gameState = GameState.EndOfMatch;
            PlayerWon.text = "Player 2 Won!!!";
            StartCoroutine(PlayEndOfMatchSound(player2Wins));
            matchEnded = true;
            
                   
        }
        }
    }
    private void EndOfMatchButton(){
        if(Input.anyKey && canProgress && matchEnded){
                canProgress = false;
                 if(ballScript.player1Score >= 5 && !Paddle2.gameObject.GetComponent<CatPaddle>().isPlayer) {
                    Debug.Log("next");
                NextLevel();
                 }
                 else{
                    if(Paddle2.GetComponent<CatPaddle>().isPlayer){
                        RandomLevel();
                    } else{

                    
                ResetMatch();
                    }
                 }
              
            }

    }
    private void ResetMatch() {
        ballScript.player1Score = 0;
        ballScript.player2Score = 0;
        PlayerWon.gameObject.SetActive(false);
        PerfectGame.gameObject.SetActive(false);
        gameState = GameState.Playing;
        ballScript.scoreText.text = $"{ballScript.player1Score} - {ballScript.player2Score}";
        Paddle1.position = new Vector3(Paddle1.position.x,0,0);
        Paddle2.position = new Vector3(Paddle2.position.x,0,0);
        Paddle1.GetComponent<CatPaddle>().ResetAll();
        Paddle2.GetComponent<CatPaddle>().ResetAll();
        matchEnded = false;

    }
    private void NextLevel() {
        currentLevel += 1;
        PlayerPrefs.SetString("CurrentStage",Stages[currentLevel]);
        if(currentLevel >= Stages.Length) {
            PlayerPrefs.SetString("CurrentStage",Stages[0]);
        }
        
        changeScene.targetScene = Stages[currentLevel];
        changeScene.isChangeScene= true;
    }
    private void RandomLevel(){
        changeScene.targetScene = Stages[Random.Range(0, Stages.Length - 1)];
        changeScene.isChangeScene= true;
    }
    void OnSceneLoaded(Scene scene,LoadSceneMode mode) {
        changeScene = GetComponent<changeScene>();
        ballScript = FindObjectOfType(typeof(Ball)) as Ball;
        gameState = GameState.Playing;
       
       
    }
    public void SinglePlayer(){
        singlePlayer = true;
    }
    public void MultiPlayer(){
        singlePlayer = false;
    
    }
    public void SelectGameobject(GameObject myGameObject){
        GameObject myEventSystem = GameObject.Find("EventSystem");
        myEventSystem .GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(myGameObject);
    }
    IEnumerator PlayEndOfMatchSound(AudioClip playerWon){
        SoundEffects.clip = playerWon;
        if(SoundEffects.clip != null){
            SoundEffects.Play();
        
            yield return new WaitWhile (()=> SoundEffects.isPlaying);
        }
            if(ballScript.player1Score == 0 || ballScript.player2Score == 0){
                     yield return new WaitForSeconds(1);
                     PerfectGame.gameObject.SetActive(true);
                     SoundEffects.clip = perfectGame;
                     if(SoundEffects.clip != null){
                     SoundEffects.Play();
                     yield return new WaitWhile (()=> SoundEffects.isPlaying);
                     }

            }
        
            canProgress = true;
       

    }

}