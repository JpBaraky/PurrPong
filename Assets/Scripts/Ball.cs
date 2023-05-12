using TMPro;
using UnityEngine;

public class Ball: MonoBehaviour {
    public float speed = 10f; // Speed of the ball movement
    public float maxSpeed = 20f; // Maximum speed of the ball
    public float randomAngle = 15f; // Random angle range for initial ball direction
    public float maxAngle = 10f;
    public int player1Score = 0;
    public int player2Score= 0;
    public TextMeshProUGUI scoreText;

    private Rigidbody2D rb2d; // Reference to the Rigidbody2D component

    // Start is called before the first frame update
    void Start() {
        rb2d = GetComponent<Rigidbody2D>();

        // Generate random initial direction for the ball
        float angle = Random.Range(-randomAngle,randomAngle);
        Vector2 direction = new Vector2(Mathf.Cos(angle),Mathf.Sin(angle));
        rb2d.velocity = direction * speed;
        scoreText.text = $"{player1Score} - {player2Score}";
    }

    // Update is called once per frame
    void Update() {
        // Limit the maximum speed of the ball
        if(rb2d.velocity.magnitude > maxSpeed) {
            rb2d.velocity = rb2d.velocity.normalized * maxSpeed;
        }
    }

    // OnCollisionEnter2D is called when the ball collides with another object
    void OnCollisionEnter2D(Collision2D collision) {
        // Reflect the ball's velocity off the collision normal
        Vector2 normal = collision.contacts[0].normal;

        rb2d.velocity = Vector2.Reflect(rb2d.velocity,normal).normalized * speed;
    }

    // OnTriggerEnter2D is called when the ball enters a trigger collider
    void OnTriggerEnter2D(Collider2D collider) {
        // Check if the trigger collider is a wall
        if(collider.CompareTag("Wall")) {
            // Check if the wall is a goal wall
            if(collider.transform.parent != null && collider.transform.parent.name == "GoalWall") {
                // Increment the score of the player that scored the goal
                if(collider.transform.position.x > 0) {
                    player1Score++;
                } else {
                    player2Score++;
                }

                // Update the UI to display the new scores
                scoreText.text = $"{player1Score} - {player2Score}";

                // Reset the ball's position and velocity
                transform.position = Vector2.zero;
                rb2d.velocity = new Vector2(Random.Range(-1f,1f),Random.Range(-1f,1f)).normalized * speed;
            } else {
                // Reflect the ball's velocity off the wall's normal
                Vector2 normal = collider.transform.right;
                Vector2 reflected = Vector2.Reflect(rb2d.velocity,normal).normalized;

                // Add a small random perturbation to the reflected velocity
                float angle = Random.Range(-maxAngle,maxAngle);
                reflected = Quaternion.Euler(0,0,angle) * reflected;

                // Set the ball's velocity to the perturbed reflected velocity
                rb2d.velocity = reflected * speed;
            }
        }
    }
}