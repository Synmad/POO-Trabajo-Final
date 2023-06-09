using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Animator animator;
    Rigidbody2D rb;

    Vector2 movement;

    [SerializeField] 
    float moveSpeed;
    [SerializeField] float knockbackStrength = 10;
    private void Awake(){ animator = GetComponent<Animator>(); rb = GetComponent<Rigidbody2D>(); }

    public void MovementControls()
    {
        movement.x = Input.GetAxisRaw("Horizontal"); movement.y = Input.GetAxisRaw("Vertical");
        movement.Normalize();

        animator.SetFloat("Horizontal", movement.x); animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.magnitude);

        if (Input.GetAxisRaw("Horizontal") == 1 || Input.GetAxisRaw("Horizontal") == -1 || Input.GetAxisRaw("Vertical") == 1 || Input.GetAxisRaw("Vertical") == -1)
        {
            animator.SetFloat("IdleHorizontal", Input.GetAxisRaw("Horizontal")); animator.SetFloat("IdleVertical", Input.GetAxisRaw("Vertical"));
        }
    }
    public void MovementPhysics()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
    public void Knockback()
    {
        rb.AddForce(transform.right * knockbackStrength);
        Debug.Log("aa");
    }
}
