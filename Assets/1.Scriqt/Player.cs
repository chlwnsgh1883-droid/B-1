using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    public float speed = 5f;
    Rigidbody2D rb;
    Vector2 move;
    public SpriteRenderer sr;
    public Animator animator; // Move만 켬/끔

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        rb.freezeRotation = true;
        if (!sr) sr = GetComponent<SpriteRenderer>();
        if (!animator) animator = GetComponent<Animator>();
    }

    void Update()
    {
        move.x = Input.GetAxisRaw("Horizontal");
        move.y = Input.GetAxisRaw("Vertical");
        move = move.normalized;

        if (sr && move.x != 0) sr.flipX = move.x < 0;
        if (animator) animator.SetBool("Move", move != Vector2.zero); // ✅ AK는 안 씀
    }

    void FixedUpdate()
    {
        rb.linearVelocity = move * speed;
    }
}
