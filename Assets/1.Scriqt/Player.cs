using NUnit.Framework.Interfaces;
using UnityEngine;
using UnityEngine.Animations;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class Player : MonoBehaviour
{
    public float speed ;
    public Vector2 moveVecter;
    public Animator animator ;
    public SpriteRenderer spriteRenderer ;
    private bool isAttack;
    public float attackSpeed;
    float timer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        

    }

    // Update is called once per frame
    void Update()
    {
        

        float x = Input.GetAxisRaw ("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        moveVecter = new Vector2(x, y).normalized;
        if (x != 0)
        {
            if (x > 0)
            {
                spriteRenderer.flipX = false;
            }
            else
            {
                spriteRenderer.flipX = true;
            }
        }

        transform.position += (Vector3)moveVecter * speed * Time.deltaTime;
        if(moveVecter == Vector2.zero)
        {
            animator.SetBool("Move", false);
        }
        else
        {
            animator.SetBool("Move", true);
        }

        if (isAttack)
        {
            if (timer >= attackSpeed)
            {
                isAttack = false;
                timer = 0;
            }
            else
            {
                timer += Time.deltaTime;
            }
        }
        if (Input.GetKeyDown(KeyCode.F) && !isAttack)
        {
            isAttack = true;
            animator.SetBool("AK", true);
        }
        else
        {
            animator.SetBool("AK", false);
        }
    }
}
