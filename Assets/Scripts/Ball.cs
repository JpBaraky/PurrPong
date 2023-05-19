using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Ball: MonoBehaviour {
    public float speed = 10f;
    private Rigidbody2D rb;
    private Vector2 direction;
    public  int player1Score = 0;
    public  int player2Score = 0;
    public TextMeshProUGUI scoreText;
    public bool readyToStart = true;
    public EnemyPaddleController enemyPaddleController;
    private GameController gameController;
    public Transform Paddle1, Paddle2;
    public CatPaddle lastBouncedPaddle;
    public CatPaddle otherPaddle;


    void Start() {
        // Set the initial direction of the ball
        direction = Vector2.right.normalized;
        gameController = FindObjectOfType(typeof(GameController)) as GameController;
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.zero; 
        scoreText.text = $"{player1Score} - {player2Score}";
    }

    void FixedUpdate() {
        // Move the ball in its current direction at a constant speed if the game is not paused
        if(!readyToStart) {
            rb.velocity = direction * speed;
        }
    }

    void Update() {
        if(gameController.gameState != GameState.Playing) {
            return;
        }
        // Check if the player has pressed the space bar to start the game
        if(Input.GetKeyDown(KeyCode.Space) && readyToStart) {
            // Enable the ball's movement and mark it as not ready to start again
            enemyPaddleController.gameStarted = true;
            readyToStart = false;
        }
    }

    void OnCollisionEnter2D(Collision2D col) {
        // If the ball collides with a paddle, change its direction based on where it hit the paddle
        if(col.gameObject.tag == "Paddle") {
            float y = (transform.position.y - col.transform.position.y) / col.collider.bounds.size.y;
            direction = new Vector2(-direction.x,y).normalized;
            otherPaddle = lastBouncedPaddle;
            lastBouncedPaddle = col.gameObject.GetComponent<CatPaddle>();

        }
        if(col.gameObject.tag == "Hurdle") {
            float y = (transform.position.y - col.transform.position.y) / col.collider.bounds.size.y;
            direction = new Vector2(-direction.x,y).normalized;           

        }

        // If the ball collides with a wall, change its direction to bounce off the wall normally
        if(col.gameObject.tag == "Wall") {
            direction = new Vector2(direction.x,-direction.y);
        }

        // If the ball collides with a goal wall, update the score and reset the ball
        if(col.gameObject.tag == "GoalWall") {
            if(col.gameObject.transform.position.x > 0) {
                player1Score++;
            } else {
                player2Score++;
            }

            scoreText.text = $"{player1Score} - {player2Score}";

            // Reset the ball's position and velocity
            transform.position = Vector2.zero;
            rb.velocity = Vector2.zero;
            readyToStart = true;
            enemyPaddleController.gameStarted=false;
            enemyPaddleController.startBoost = false;
            Paddle1.position = new Vector3(Paddle1.position.x,0,0);
            Paddle2.position = new Vector3(Paddle2.position.x,0,0);
        }
    }
}