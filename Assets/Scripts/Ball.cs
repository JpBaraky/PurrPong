using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Ball: MonoBehaviour {
    public float speed = 10f;
    private Rigidbody2D rb;
    private Vector2 direction;
    public  int player1Score = 0;
    public  int player2Score = 0;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI readyToStartText;
    public bool readyToStart = true;
    public EnemyPaddleController enemyPaddleController;
    private GameController gameController;
    public Transform Paddle1, Paddle2;
    public Animator CatAnimator;
    public CatPaddle lastBouncedPaddle;
    public CatPaddle otherPaddle;
    public bool isFakeBall;
    private float startingSpeed;
    private AudioSource ballSounds;
    [Header("Sound Clips")]
    public AudioClip hitSoundHurdle;
    public AudioClip hitSoundPaddle;
    public AudioClip goalSound;

[SerializeField]

private InputActionReference StartGame;

    void Start() {
        // Set the initial direction of the ball
        direction = Vector2.right.normalized;
        gameController = FindObjectOfType(typeof(GameController)) as GameController;
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.zero; 
        scoreText.text = $"{player1Score} - {player2Score}";
        startingSpeed = speed;
        ballSounds = GetComponent<AudioSource>();
        readyToStartText = GameObject.Find("Press to start").GetComponent<TextMeshProUGUI>();
        readyToStartText.gameObject.SetActive(true);   
    }

    void FixedUpdate() {
        // Move the ball in its current direction at a constant speed if the game is not paused
        if(!readyToStart) {
            rb.velocity = direction * speed;
        }
    }

    void Update() {
          if(player1Score >= 5 || player2Score >= 5) {
           readyToStartText.gameObject.SetActive(false);
            readyToStartText.gameObject.GetComponent<BlinkingText>().isBlinking = false;
        }
        if(gameController.gameState != GameState.Playing) {
            return;
        }
        // Check if the player has pressed the space bar to start the game
        if(StartGame.action.IsPressed() || Input.GetButtonDown("Fire3")
         && !enemyPaddleController.gameStarted
        && readyToStart) {
            // Enable the ball's movement and mark it as not ready to start again
            enemyPaddleController.gameStarted = true;
            readyToStartText.gameObject.SetActive(false);
            readyToStartText.gameObject.GetComponent<BlinkingText>().isBlinking = false;
            readyToStart = false;
        }
      
    }

    void OnCollisionEnter2D(Collision2D col) {
        // If the ball collides with a paddle, change its direction based on where it hit the paddle
        if(col.gameObject.tag == "Paddle") {
            CatAnimator = col.gameObject.GetComponent<Animator>();
            CatAnimator.SetTrigger("Cat Attack");
            ballSounds.PlayOneShot(hitSoundPaddle);
            float y = (transform.position.y - col.transform.position.y) / col.collider.bounds.size.y;
            direction = new Vector2(-direction.x,y).normalized;
            otherPaddle = lastBouncedPaddle;
            lastBouncedPaddle = col.gameObject.GetComponent<CatPaddle>();
            rb.AddTorque(Random.Range(-10, 10));
        }
        if(col.gameObject.tag == "Hurdle") {
            //direction = new Vector2(direction.x,-direction.y);
            direction = Vector2.Reflect(direction, col.contacts[0].normal);
            ballSounds.PlayOneShot(hitSoundHurdle);
        }
        if(col.gameObject.tag == "HurdleStage") {
            //direction = new Vector2(direction.x,-direction.y);
            direction = Vector2.Reflect(direction, col.contacts[0].normal);
            ballSounds.PlayOneShot(hitSoundHurdle);
        }


        // If the ball collides with a wall, change its direction to bounce off the wall normally
        if(col.gameObject.tag == "Wall") {
            //direction = new Vector2(direction.x,-direction.y);
            direction = Vector2.Reflect(direction, col.contacts[0].normal);
            ballSounds.PlayOneShot(hitSoundHurdle);            
            rb.AddTorque(Random.Range(-10, 10));
        }

        // If the ball collides with a goal wall, update the score and reset the ball
        if(col.gameObject.tag == "GoalWall") {
            if(!isFakeBall){

            ballSounds.PlayOneShot(goalSound);
            if(col.gameObject.transform.position.x > 0) {
                direction = Vector2.left.normalized;
                player1Score++;
            } else {
                direction = Vector2.right.normalized;
                player2Score++;
            }

            scoreText.text = $"{player1Score} - {player2Score}";  
            Reset();
            
                     
            
            } else{
                Destroy(gameObject);
            }
            
    }
    void Reset() {
// Reset the ball's position and velocity
            transform.position = Vector2.zero;
            rb.velocity = Vector2.zero;
            readyToStart = true;
            rb.rotation = 0; 
            rb.inertia = 0;
            rb.centerOfMass = Vector3.zero;
            enemyPaddleController.gameStarted=false;
            enemyPaddleController.startBoost = false;
            Paddle1.position = new Vector3(-5f,0,0);
            Paddle2.position = new Vector3(5f,0,0);
           //Stop Ball's rotation
            rb.angularVelocity = 0;
            rb.velocity = Vector2.zero;
             //Destroy all objects with the tag FakeBall
             GameObject[] fakeBalls = GameObject.FindGameObjectsWithTag("FakeBall");
             foreach(GameObject fakeBall in fakeBalls){
                 Destroy(fakeBall);
             }
             GameObject[] powerUps = GameObject.FindGameObjectsWithTag("Bone");
             foreach(GameObject powerUp in powerUps){
                 Destroy(powerUp);
             }
             GameObject[] Hurdles = GameObject.FindGameObjectsWithTag("Hurdle");
             foreach(GameObject hurdles in Hurdles){
                 Destroy(hurdles);
             }
            readyToStartText.gameObject.SetActive(true);
        }
        
    }
    public void IncreaseSize(float sizeAmount) {
        transform.localScale = transform.localScale * sizeAmount;
    }
    public void ResetSize() {
        transform.localScale = Vector3.one;
 
    }
    public void IncreaseSpeed(float speedAmount) {
        speed += speedAmount;
        
    }
    public void ResetSpeed() {
        speed = startingSpeed;

    }
}