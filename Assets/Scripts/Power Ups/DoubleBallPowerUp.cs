using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleBallPowerUp : MonoBehaviour
{
    private PowerUpBasic powerUpBasic;
    public GameObject ballObject;
    private Transform ballPosition;

    
    // Start is called before the first frame update
    void Start()
    {
         powerUpBasic= GetComponent<PowerUpBasic>();
         ballPosition = GameObject.FindGameObjectWithTag("Ball").transform;
         
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.CompareTag("Paddle")) {
         
            
            StartCoroutine(powerUpBasic.DisplayText());
            if(ballObject != null) {
              //Instantiate a new ball and set it to the same position as the old one
              GameObject ball = Instantiate(ballObject, ballPosition.position, Quaternion.identity);
              // Rotate the new ball to a random direction
              ball.GetComponent<Rigidbody2D>().AddTorque(Random.Range(-10, 10));
                           

        
                

                    // Disable the collider so the power-up doesn't trigger again
                    GetComponent<Collider2D>().enabled = false;
                    GetComponent<SpriteRenderer>().enabled = false;



                }
            }
        }
    
        

   
}
