using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] // to adjust in real-time
    float speed;

    float radius;
    Vector2 direction;

    // Start is called before the first frame update
    void Start()
    {
        // At start, set direction to (1, 1) towards top right of screen, mag = 1
        direction = Vector2.one.normalized; // (1,1) has mag. of sqrt(2), will affect speed cal -> Need normalize
        radius = transform.localScale.x / 2; // Width/Height divided by 2
    }

    // Update is called once per frame
    void Update()
    {
        // To move ball
        transform.Translate(direction * speed * Time.deltaTime); // Adjust for changes in framerate

        // Ball to bounce off top & bottom
        // Bounce off bottom
        if (transform.position.y < GameManager.bottomLeft.y + radius && direction.y < 0) // moving down
        {
            direction.y = -direction.y; // invert direction
        }

        // Bounce off top
        if (transform.position.y > GameManager.topRight.y - radius && direction.y > 0) // moving up
        {
            direction.y = -direction.y; // invert direction
        }

        // Game over if ball hits left & right boundaries
        // Hits left
        if (transform.position.x < GameManager.bottomLeft.x + radius && direction.x < 0) // moving left
        {
            Debug.Log("Right player wins!");
            // Freeze time
            Time.timeScale = 0;
            enabled = false; // Stop updating script
        }

        // Hits right
        if (transform.position.x > GameManager.topRight.x - radius && direction.x > 0) // moving right
        {
            Debug.Log("Left player wins!");
            // Freeze time
            Time.timeScale = 0;
            enabled = false; // Stop updating script
        }
    }

    // Collision with paddles -> Change direction (need Collider w Trigger & RigidBody2D w Kinematics
    void OnTriggerEnter2D(Collider2D other) // Lets us know what has collided with ball
    {
        if(other.tag == "Paddle")
        {
            bool isRight = other.GetComponent<Paddle>().isRight; // Gets the Paddle script

            // If hitting right paddle and moving right, flip direction
            if (isRight == true && direction.x > 0)
            {
                direction.x = -direction.x;
            }
            // If hitting left paddle and moving left, flip direction
            if (isRight == false && direction.x < 0)
            {
                direction.x = -direction.x;
            }
        }
    }
}
