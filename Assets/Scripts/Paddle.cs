using MoreMountains.Feedbacks;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class Paddle : MonoBehaviour
{
    protected Rigidbody2D Rb;

    public float speed = 8f;
    public bool useDynamicBounce;
    
    private void Awake()
    {
        Rb = GetComponent<Rigidbody2D>();
    }

    public void ResetPosition()
    {
        Rb.velocity = Vector2.zero;
        Rb.position = new Vector2(Rb.position.x, 0f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!useDynamicBounce || !collision.gameObject.CompareTag("Ball")) return;
        
        Rigidbody2D ball = collision.rigidbody;
        Collider2D paddle = collision.otherCollider;
        
        Vector2 ballDirection = ball.velocity.normalized;
        Vector2 contactDistance = ball.transform.position - paddle.bounds.center;
        Vector2 surfaceNormal = collision.GetContact(0).normal;
        Vector3 rotationAxis = Vector3.Cross(Vector3.up, surfaceNormal);
        
        float maxBounceAngle = 75f;
        float bounceAngle = (contactDistance.y / paddle.bounds.size.y) * maxBounceAngle;
        ballDirection = Quaternion.AngleAxis(bounceAngle, rotationAxis) * ballDirection;
        
        ball.velocity = ballDirection * ball.velocity.magnitude;
    }
}