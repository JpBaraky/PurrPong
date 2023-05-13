using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

public enum GameState {
    Playing,
    Paused,
    EndOfMatch,
}


public class GameController: MonoBehaviour {
    public GameState gameState;
    public TextMeshProUGUI PlayerWon;
    public Transform Paddle1, Paddle2;
    private Ball ballScript;
    void Start() {
        ballScript = FindObjectOfType(typeof(Ball)) as Ball;
        gameState = GameState.Playing;
    }

    void Update() {
        if(Input.GetKeyDown(KeyCode.Escape)) {
            if(gameState == GameState.Playing) {
                PauseGame();
            } else {
                ResumeGame();
            }
        }
        EndOfMatch();
    }

    void PauseGame() {
        gameState = GameState.Paused;
        Time.timeScale = 0f; // This will pause all animations, physics, etc.
    }

    void ResumeGame() {
        gameState = GameState.Playing;
        Time.timeScale = 1f; // This will resume the normal time scale.
    }
    void EndOfMatch() {
        if(ballScript.player1Score >= 5) {
            PlayerWon.gameObject.SetActive(true);
            PlayerWon.text = "Player 1 Won!!!";
            gameState = GameState.EndOfMatch;
            if(Input.GetKeyDown(KeyCode.Space)){
                ballScript.player1Score= 0;
                ballScript.player2Score = 0;
                PlayerWon.gameObject.SetActive(false);
                gameState= GameState.Playing;
                ballScript.scoreText.text = $"{ballScript.player1Score} - {ballScript.player2Score}";
                Paddle1.position = new Vector3(Paddle1.position.x, 0, 0);
                Paddle2.position = new Vector3(Paddle2.position.x,0,0);
            }
        } else if(ballScript.player2Score >= 5) {
            PlayerWon.gameObject.SetActive(true);
            PlayerWon.text = "Player 2 Won!!!";
            gameState = GameState.EndOfMatch;
            if(Input.GetKeyDown(KeyCode.Space)) {
                ballScript.player1Score = 0;
                ballScript.player2Score = 0;
                PlayerWon.gameObject.SetActive(false);
                gameState = GameState.Playing;
                ballScript.scoreText.text = $"{ballScript.player1Score} - {ballScript.player2Score}";
                Paddle1.position = new Vector3(Paddle1.position.x,0,0);
                Paddle2.position = new Vector3(Paddle2.position.x,0,0);
            }
        }
    }
}