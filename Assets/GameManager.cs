using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// GameManager instantiates a ball and paddle 

// to get editor access to it
public class GameManager : MonoBehaviour
{
    //to drag and drop into editor
    public Ball ball;
    public Paddle paddle;

    // Vectors hold bottom left and top right positions
    // public - can access from other scripts
    // static - access info without having a reference to GameManager
    public static Vector2 bottomLeft;
    public static Vector2 topRight;

    // Start is called before the first frame update (at the beginning of the game)
    // Anything here should run only once at the beginning
    void Start()
    {

        // Screen position =/= world position
        // Converts screen's pixel coordinate into game's coordinate (in this case 0, 0)
        bottomLeft = Camera.main.ScreenToWorldPoint(new Vector2(0, 0)); // Bottom left pixel

        // Converts screen's pixel coordinate into game's coordinate
        topRight = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height)); // Top right pixel

        // create ball
        Instantiate(ball); // spawns the ball

        // create 2 paddles
        Paddle paddle1 = Instantiate(paddle) as Paddle; // spawns the paddle as type Paddle
        Paddle paddle2 = Instantiate(paddle) as Paddle; // spawns the paddle as type Paddle
        paddle1.Init(true); // right paddle
        paddle2.Init(false); // left paddle
    }

 
}
