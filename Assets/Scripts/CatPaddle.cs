using UnityEngine;
using UnityEngine.InputSystem;


public class CatPaddle: MonoBehaviour {

    public float speed = 10f; // Speed of the paddle movement
    public float boundaryY = 3.5f; // Vertical boundary of the paddle movement
    public float boundaryX1;// Horizontal boundary of the paddle movement
    public float boundaryX2;// Horizontal boundary of the paddle movement
    public EnemyPaddleController enemyPaddleController;
    public CatPaddle otherPaddle;
    private GameController gameController;
    public bool isPlayer;
    public bool player1;
    private Vector3 startingScale;
    private float startingSpeed;
    private float moveX, moveY;

    private Rigidbody2D rb2d; // Reference to the Rigidbody2D component

    [SerializeField]
    private InputActionReference Move;

    // Start is called before the first frame update
    void Start() {
        startingScale= transform.localScale;
        startingSpeed = speed;
        rb2d = GetComponent<Rigidbody2D>();
        gameController = FindObjectOfType(typeof(GameController)) as GameController;
        if(!player1){
            isPlayer = !gameController.singlePlayer;
        }
    }
public void OnMove(){
     moveY = Move.action.ReadValue<Vector2>().y;// Get input from vertical axis
        
     moveX = Move.action.ReadValue<Vector2>().x; // Get input from horizontal axis
        
}
    // FixedUpdate is called at a fixed interval
    void FixedUpdate() {
        
        if(gameController.gameState != GameState.Playing || !isPlayer) {
            return;
        }
        Vector2 velocity = rb2d.velocity;
        velocity.y = moveY * speed * Time.fixedDeltaTime; // Calculate new velocity
        velocity.x = moveX * speed * Time.fixedDeltaTime;
        if(player1) {
       float moveX = Input.GetAxisRaw("Horizontal");// Get input from vertical axis
       
       float moveY = Input.GetAxisRaw("Vertical"); // Get input from horizontal axis
        velocity.y = moveY * speed * Time.fixedDeltaTime; // Calculate new velocity
        velocity.x = moveX * speed * Time.fixedDeltaTime;
        }
        if(!player1){
            float moveY = Input.GetAxisRaw("Vertical2");  // Get input from vertical axis
            float moveX = Input.GetAxisRaw("Horizontal2");  // Get input from horizontal axis
        velocity.y = moveY * speed * Time.fixedDeltaTime; // Calculate new velocity
        velocity.x = moveX * speed * Time.fixedDeltaTime;
        }
        
       
        Vector2 position = rb2d.position + velocity; // Calculate new position

        position.y = Mathf.Clamp(position.y,-boundaryY,boundaryY);
        position.x = Mathf.Clamp(position.x,boundaryX1,boundaryX2);// Clamp position within the boundary

        rb2d.MovePosition(position); // Move the paddle to the new position
    }
    public void IncreaseSize(float sizeAmount) {
        transform.localScale = transform.localScale * sizeAmount;
    }
    public void ResetSize() {
        transform.localScale = startingScale;
 
    }
    public void IncreaseSpeed(float speedAmount) {
        speed += speedAmount;
        
    }
    public void ResetSpeed() {
        speed = startingSpeed;

    }


}

