using MoreMountains.Feedbacks;
using UnityEngine;

public class PlayerPaddle : Paddle
{
    private Vector2 _direction;
    [SerializeField] private MMF_Player _mmfPlayer;

    private void Update()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) 
        {
            _direction = Vector2.up;
        } 
        
        else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) 
        {
            _direction = Vector2.down;
        } 
        
        else 
        {
            _direction = Vector2.zero;
        }
    }

    private void FixedUpdate()
    {
        if (_direction.sqrMagnitude != 0) 
        {
            Rb.AddForce(_direction * speed);
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