using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    public Transform hitRoot;
    int faceDir = 1;



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

        if (Mathf.Abs(move.x) > 0.01f)
        {
            faceDir = move.x > 0 ? 1 : -1;
            // 스프라이트만 뒤집고
            if (sr) sr.flipX = (faceDir == -1);
            // 히트박스 묶음은 좌우 스케일로 미러링
            if (hitRoot)
            {
                var vector = hitRoot.localScale;
                vector.x = faceDir;       // 1 또는 -1
                hitRoot.localScale = vector;
            }
        }
    }

    void FixedUpdate()
    {
        rb.linearVelocity = move.normalized * speed;
    }
}
