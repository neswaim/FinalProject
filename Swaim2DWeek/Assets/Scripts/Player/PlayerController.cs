using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{

    Rigidbody2D rigidBody2D;

    public float runSpeed = 5;
    public float jumpSpeed = 200f;
    public float maxVel;
    public float minVel;
    public float velx;
    public float vely;

    public TextMeshProUGUI countText;
    public SpriteRenderer spriteRenderer;
    public Animator animator;

    private int count;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            int levelMask = LayerMask.GetMask("Level");

            if (Physics2D.BoxCast(transform.position, new Vector2(1, 0.1f), 0f, Vector2.down, 0.01f, levelMask))
            {
                Jump();
            }
        }
    }

    void FixedUpdate()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        Vector2 direction = new Vector2(horizontalInput * runSpeed * Time.deltaTime, 0);

        rigidBody2D.AddForce(direction);

        velx = rigidBody2D.velocity.x;
        vely = rigidBody2D.velocity.y;

        if (velx > maxVel)
        {
            rigidBody2D.velocity = rigidBody2D.velocity.normalized * maxVel;
        }
        if (velx < minVel)
        {
            rigidBody2D.velocity = rigidBody2D.velocity.normalized * maxVel;
        }

            if (rigidBody2D.velocity.x > 0)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }

        if (Mathf.Abs(horizontalInput) > 0f)
        {
            animator.SetBool("isRunning", true);
        }
        else
        {
            animator.SetBool("isRunning", false);
        }
    }

    void Jump()
    {
        rigidBody2D.velocity = new Vector2(rigidBody2D.velocity.x, jumpSpeed);
    }

    void OnTriggerEnter2D(Collider2D other)
    {

    }
}

