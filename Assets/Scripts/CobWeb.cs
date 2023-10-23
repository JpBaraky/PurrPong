using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class CobWeb : MonoBehaviour
{
    public float speedReduction = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D col) {
        // If the ball collides with a paddle, change its direction based on where it hit the paddle
        if(col.gameObject.tag == "Paddle") {
           CatPaddle collisionPaddle = col.gameObject.GetComponent<CatPaddle>();
           collisionPaddle.speed *= speedReduction;
     
          
        
        }
        if(col.gameObject.tag == "Ball") {
            Ball collisionBall = col.gameObject.GetComponent<Ball>();
            collisionBall.speed *= speedReduction;  
        
        }
        }
 
       void OnTriggerExit2D(Collider2D col) {
        if(col.gameObject.tag == "Paddle") {
           CatPaddle collisionPaddle = col.gameObject.GetComponent<CatPaddle>();
           collisionPaddle.speed /= speedReduction;
          
        
        }
              if(col.gameObject.tag == "Ball") {
            Ball collisionBall = col.gameObject.GetComponent<Ball>();
            collisionBall.speed /= speedReduction;
    
        
        }

    }


}
