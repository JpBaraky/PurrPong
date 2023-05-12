using UnityEngine;

public class CatPaddle: MonoBehaviour {
    public float speed = 10f; // Speed of the paddle movement
    public float boundaryY = 3.5f; // Vertical boundary of the paddle movement

    private Rigidbody2D rb2d; // Reference to the Rigidbody2D component

    // Start is called before the first frame update
    void Start() {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // FixedUpdate is called at a fixed interval
    void FixedUpdate() {
        float moveY = Input.GetAxisRaw("Vertical"); // Get input from vertical axis

        Vector2 velocity = rb2d.velocity;
        velocity.y = moveY * speed * Time.fixedDeltaTime; // Calculate new velocity

        Vector2 position = rb2d.position + velocity; // Calculate new position

        position.y = Mathf.Clamp(position.y,-boundaryY,boundaryY); // Clamp position within the boundary

        rb2d.MovePosition(position); // Move the paddle to the new position
    }
}

