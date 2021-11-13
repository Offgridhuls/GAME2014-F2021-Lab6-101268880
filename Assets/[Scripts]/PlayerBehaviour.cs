using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    [Header("Movement")]
    public float horizontalForce;
    public float verticalForce;

    public bool isGrounded;

    public LayerMask groundLayerMask;

    public Transform groundOrigin;
    public float groundRadius;

    private Rigidbody2D rigidBody;
    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Move();
    }
    // Update is called once per frame
    void Update()
    {
        
        CheckIsGrounded();
    }

    private void Move()
    {
        if (isGrounded)
        {
            float deltaTime = Time.deltaTime;

            float x = Input.GetAxisRaw("Horizontal");
            float y = Input.GetAxisRaw("Vertical");

            float jump = Input.GetAxisRaw("Jump");

            if (x != 0)
            {
                x = FlipAnimation(x);
            }

            Vector2 worldTouch = new Vector2();

            foreach (var touch in Input.touches)
            {
                worldTouch = Camera.main.ScreenToWorldPoint(touch.position);
            }

            float horizontalMoveForce = x * horizontalForce;
            float jumpMoveForce = jump * verticalForce;

            float mass = rigidBody.mass * rigidBody.gravityScale;

            rigidBody.AddForce(new Vector2(horizontalMoveForce, jumpMoveForce) * mass);

            rigidBody.velocity *= .99f;
        }
    }
    
    private float FlipAnimation(float x)
    {
        x = (x > 0) ? 1 : -1;

        transform.localScale = new Vector3(x, 1.0f);

        return x;
    }
    private void CheckIsGrounded()
    {
        RaycastHit2D hit = Physics2D.CircleCast(groundOrigin.position, groundRadius, Vector2.down, groundRadius, groundLayerMask);

        isGrounded = (hit) ? true : false;
    }
}
