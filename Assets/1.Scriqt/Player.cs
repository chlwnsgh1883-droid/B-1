using UnityEngine;

public class Player : MonoBehaviour
{
    public Vector2 moveVecter;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        moveVecter = new Vector2(x, y);
    }
}
