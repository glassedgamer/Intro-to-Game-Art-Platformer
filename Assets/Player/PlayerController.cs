using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    [SerializeField] Rigidbody2D rb;
    [SerializeField] Animator animator;

    Vector2 movement;
    
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float jumpForce = 2f;

    private float inputX;

    bool isGrounded;
    bool facingRight = true;

    [SerializeField] Transform groundCheck;
    [SerializeField] float checkRadius;
    [SerializeField] LayerMask whatIsGround;

    void Start() {
        
    }

    void Update() {
        movement = new Vector2(inputX * moveSpeed, rb.velocity.y);
        
        rb.velocity = movement;
        
        if(movement != Vector2.zero && isGrounded) {
            animator.SetBool("isWalking", true);
        } else if(movement == Vector2.zero){
            animator.SetBool("isWalking", false);
        }

        if(facingRight == false && inputX > 0) {
            Flip();
        } else if(facingRight == true && inputX < 0) {
            Flip();
        }
    }

    void FixedUpdate() {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);
    }
    
    void Flip() {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }

    public void Move(InputAction.CallbackContext context) {
        inputX = context.ReadValue<Vector2>().x;
    }

    public void Jump(InputAction.CallbackContext context) {
        if(isGrounded) {
            animator.SetTrigger("jump");
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, checkRadius);
    }

}
