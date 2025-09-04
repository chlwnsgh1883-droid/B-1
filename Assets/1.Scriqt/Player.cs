using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 10;
    public Vector2 moveVecter;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxisRaw ("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        moveVecter = new Vector2(x, y).normalized;

        transform.position += (Vector3)moveVecter * speed * Time.deltaTime;
    }
}
