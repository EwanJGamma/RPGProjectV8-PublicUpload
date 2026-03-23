using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D PBody;
    private float horizontal;
    private float vertical;
    public float Speed = 500f;
    private Vector2 movement;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        movement = new Vector2(horizontal, vertical).normalized;
        PBody.linearVelocity = new Vector2(movement.x * Speed, movement.y * Speed) * Time.deltaTime;
    }
}
