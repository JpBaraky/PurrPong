using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeBall : MonoBehaviour
{
    public float speed = 10f;
    private Rigidbody2D rb;
    private Vector2 direction;
    // Start is called before the first frame update
    void Start()
    {
        direction = Vector2.right.normalized;
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = direction * speed;
        
    }
    void FixedUpdate() {
        // Move the ball in its current direction at a constant speed if the game is not paused
       
            rb.velocity = direction * speed;
           
    }

    // Update is called once per frame
    
     void OnCollisionEnter2D(Collision2D col) {
        // If the ball collides with a paddle, change its direction based on where it hit the paddle
        if(col.gameObject.tag == "Paddle") {
            float y = (transform.position.y - col.transform.position.y) / col.collider.bounds.size.y;
            direction = new Vector2(-direction.x,y).normalized;
            rb.AddTorque(Random.Range(-10, 10));

        }
        if(col.gameObject.tag == "Hurdle") {
            float y = (transform.position.y - col.transform.position.y) / col.collider.bounds.size.y;
            direction = new Vector2(-direction.x,y).normalized;           
            rb.AddTorque(Random.Range(-10, 10));
        }

        // If the ball collides with a wall, change its direction to bounce off the wall normally
        if(col.gameObject.tag == "Wall") {
            direction = new Vector2(direction.x,-direction.y);
            rb.AddTorque(Random.Range(-10, 10));
        }

        // If the ball collides with a goal wall, update the score and reset the ball
        if(col.gameObject.tag == "GoalWall") {
            //Destroy this ball
            Destroy(gameObject);
}
     }
}
