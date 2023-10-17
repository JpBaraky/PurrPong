using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.VirtualTexturing.Debugging;

public class PaddleSizePowerUp: MonoBehaviour {


    [Header("PoweUp Settings")]
    public float powerUpDuration = 5f; // The duration of the power-up in seconds
    public float sizeIncreaseAmount; // The size that the paddle will increase

    private bool powerUpActive = false; // Flag to track whether the power-up is active
    private PowerUpBasic powerUpBasic;
    public bool isToAffectEnemy;
    private CatPaddle affectPaddle;
    private CatPaddle player1Paddle;
    private CatPaddle player2Paddle;


    private void Start() {
        powerUpBasic = GetComponent<PowerUpBasic>();
    }



    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.CompareTag("Paddle")) {
      
            if(!powerUpBasic.isInventory){
                
            StartCoroutine(powerUpBasic.DisplayText());
            
          
             
                if(!isToAffectEnemy) {

                    //affectPaddle = ball.lastBouncedPaddle;
                    affectPaddle = collision.GetComponent<CatPaddle>();
                } else {
                    //affectPaddle = ball.otherPaddle;
                    affectPaddle = collision.GetComponent<CatPaddle>().otherPaddle;
                }

                // Increase the size of the paddle
                affectPaddle.IncreaseSize(sizeIncreaseAmount);

                    // Start a coroutine to revert the paddle size after the specified duration
                    StartCoroutine(RevertPaddleSize(affectPaddle));

                    // Set the power-up as active
                    powerUpActive = true;
            }
                    // Disable the collider so the power-up doesn't trigger again
                    GetComponent<Collider2D>().enabled = false;
                    GetComponent<SpriteRenderer>().enabled = false;
                
            

                
            
        }
    }

    private IEnumerator RevertPaddleSize(CatPaddle paddle) {
        yield return new WaitForSeconds(powerUpDuration);

        // Revert the paddle size back to its original size only if the power-up is still active
        if(powerUpActive) {
            paddle.ResetSize();
            powerUpActive = false;
            Destroy(gameObject);
        }
    }
  
}