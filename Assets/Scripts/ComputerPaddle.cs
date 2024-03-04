using MoreMountains.Feedbacks;
using UnityEngine;

public class ComputerPaddle : Paddle
{
    [SerializeField]
    private Rigidbody2D ball;
    
    [SerializeField] private MMF_Player _mmfPlayer;

    private void FixedUpdate()
    {
        // Check if the ball is moving towards the paddle (positive x velocity)
        // or away from the paddle (negative x velocity)
        if (ball.velocity.x > 0f)
        {
            // Move the paddle in the direction of the ball to track it
            if (ball.position.y > Rb.position.y) {
                Rb.AddForce(Vector2.up * speed);
            } else if (ball.position.y < Rb.position.y) {
                Rb.AddForce(Vector2.down * speed);
            }
        }
        else
        {
            // Move towards the center of the field and idle there until the
            // ball starts coming towards the paddle again
            if (Rb.position.y > 0f) {
                Rb.AddForce(Vector2.down * speed);
            } else if (Rb.position.y < 0f) {
                Rb.AddForce(Vector2.up * speed);
            }
        }
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            _mmfPlayer.PlayFeedbacks();
        }
    }
}
