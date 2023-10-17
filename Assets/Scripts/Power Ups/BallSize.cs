using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSize : MonoBehaviour
{
 
    [Header("PowerUp Settings")]
    public float powerUpDuration = 5f; // The duration of the power-up in seconds
    public float sizeIncreaseAmount; // The size that the paddle will increase

    private bool powerUpActive = false; // Flag to track whether the power-up is active
    private PowerUpBasic powerUpBasic;
    
    private Ball ball;



    private void Start() {
        powerUpBasic = GetComponent<PowerUpBasic>();
        ball = FindObjectOfType<Ball>();
    }



    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.CompareTag("Paddle")) {
          
        
            StartCoroutine(powerUpBasic.DisplayText());
            
             
               

                // Increase the size of the ball
                ball.IncreaseSize(sizeIncreaseAmount);

                    // Start a coroutine to revert the ball size after the specified duration
                    StartCoroutine(RevertPaddleSize(ball));

                    // Set the power-up as active
                    powerUpActive = true;

                    // Disable the collider so the power-up doesn't trigger again
                    GetComponent<Collider2D>().enabled = false;
                    GetComponent<SpriteRenderer>().enabled = false;
                


                
            
        }
    }

    private IEnumerator RevertPaddleSize(Ball paddle) {
        yield return new WaitForSeconds(powerUpDuration);

        // Revert the paddle size back to its original size only if the power-up is still active
        if(powerUpActive) {
            paddle.ResetSize();
            powerUpActive = false;
            Destroy(gameObject);
        }
    }
  
}