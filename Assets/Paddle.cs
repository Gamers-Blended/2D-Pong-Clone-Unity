using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    // public float speed; will be available to other c# files
    [SerializeField] // Can adjust in real-time
    float speed;
    float height;

    string input;
    public bool isRight;

    // Start is called before the first frame update
    void Start()
    {
        height = transform.localScale.y;
    }

    public void Init(bool isRightPaddle)
    {
        isRight = isRightPaddle;
        // Variable to hold the position
        Vector2 pos = Vector2.zero; // pos is a vector
        if(isRightPaddle)
        {
            // Places paddle on the right of the screen
            pos = new Vector2(GameManager.topRight.x, 0); // Since static is used; need to create new vector - y = 0
            // Move a bit to the left
            pos -= Vector2.right * transform.localScale.x; // (1,0) * localScale.x (width), - to go left

            input = "PaddleRight";
        }
        else
        {
            // Places paddle on the left of the screen
            pos = new Vector2(GameManager.bottomLeft.x, 0);
            // Move a bit to the right
            pos += Vector2.right * transform.localScale.x; // (1,0) * localScale.x (width), + to go right

            input = "PaddleLeft";
        }

        // Updates this paddle's position
        transform.position = pos; // to change position to whatever vector

        transform.name = input; // Set name of GameObject paddle to input name
    }
    // Update is called once per frame
    void Update()
    {
        // To move paddles
        // Time.deltaTime = Divide by framerate, ensures inputs are framerate indepedent - if framerate fast, paddles move very fast
        float move = Input.GetAxis(input) * Time.deltaTime * speed; // GetAxis is a number between 1 & -1
    
        // Restrict paddle movement if too far up/down
        // If paddle is too low and user is continuing to move down, stop
        if(transform.position.y < GameManager.bottomLeft.y + height/2 && move < 0) // paddle already moving down
        // x & y position of paddle is its center -> need height/2
        {
            move = 0;
        }

        // If paddle is too high and user is continuing to move up, stop
        if (transform.position.y > GameManager.topRight.y - height / 2 && move > 0) // paddle already moving up
        // x & y position of paddle is its center -> need height/2
        {
            move = 0;
        }

        // To move paddles, use Translate()
        transform.Translate(move * Vector2.up); // Converts the float move into up/down vector, Vector2.up = (0, 1)

    }
}
