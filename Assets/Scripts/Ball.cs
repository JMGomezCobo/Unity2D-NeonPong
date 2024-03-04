using MoreMountains.Feedbacks;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Ball : MonoBehaviour
{
    private Rigidbody2D rb;

    public float baseSpeed = 5f;
    public float maxSpeed = Mathf.Infinity;
    
    public float currentSpeed { get; set; }
    
    [SerializeField] private MMF_Player _mmfPlayer;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void ResetPosition()
    {
        rb.velocity = Vector2.zero;
        rb.position = Vector2.zero;
    }

    public void AddStartingForce()
    {
        float x = Random.value < 0.5f ? -1f : 1f;
        float y = Random.value < 0.5f ? Random.Range(-1f, -0.5f) : Random.Range(0.5f, 1f);
        
        Vector2 direction = new Vector2(x, y).normalized;
        rb.AddForce(direction * baseSpeed, ForceMode2D.Impulse);
        currentSpeed = baseSpeed;
    }

    private void FixedUpdate()
    {
        Vector2 direction = rb.velocity.normalized;
        
        currentSpeed = Mathf.Min(currentSpeed, maxSpeed);
        rb.velocity = direction * currentSpeed;
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        _mmfPlayer.PlayFeedbacks();
    }
}