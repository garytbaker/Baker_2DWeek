using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{

    Rigidbody2D rB2D;
    public Animator animator;
    public float runSpeed;
    public float jumpSpeed;
    public SpriteRenderer spriteRenderer;
    public Text scoretext;
    public int score = 0;
    // Start is called before the first frame update
    void Start()
    {
        rB2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            int levelMask = LayerMask.GetMask("Level");

            if (Physics2D.BoxCast(transform.position, new Vector2(1f, .1f), 0f, Vector2.down, .01f, levelMask))
            {
                Jump();

            }

        }
    }

    private void FixedUpdate()
    {
        float horizontaInput = Input.GetAxis("Horizontal");
        rB2D.velocity = new Vector2(horizontaInput * runSpeed * Time.deltaTime, rB2D.velocity.y);

        if (rB2D.velocity.x <0f)
        {
            spriteRenderer.flipX = true;
        }
        else if (rB2D.velocity.x > 0f)
        {
            spriteRenderer.flipX = false;
        }

        if (Mathf.Abs(horizontaInput)>0f)
        {
            animator.SetBool("IsRunning", true);
        }
        else
        {
            animator.SetBool("IsRunning", false);
        }
        if (Input.GetAxis("Vertical") < 0f)
        {
            animator.SetBool("Falling", true);
        }
        else
        {
            animator.SetBool("Falling", false);
        }
        if (animator.GetBool("Jumping")==true)
        {
            animator.SetBool("Jumping", false);
        }
    }

    void Jump()
    {
        rB2D.velocity = new Vector2(rB2D.velocity.x, jumpSpeed);
        animator.SetBool("Jumping", true);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("PickUp"))
        {
            score += 1;
            scoretext.text = "Score: " + score.ToString();
            collision.gameObject.SetActive(false); 
        }
    }
}
