using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;
    public float jumpForce = 12f;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundCheckRadius = 0.1f;
    public LayerMask groundLayer;

    [Header("Jump Settings")]
    public float coyoteTime = 0.1f;
    public float jumpBufferTime = 0.1f;

    [Header("Axolotl Settings")]
    public Animator animator; // Animator bileşeni Inspector üzerinden atanacak
    private bool isCarryingAxolotl = false;

    private Rigidbody2D rb;
    private float moveInput;
    private bool facingRight = true;

    private bool isGrounded;
    private float coyoteTimeCounter;
    private float jumpBufferCounter;

    public AudioSource footstepAudio;

    public void PlayFootstepSound()
    {
        if (footstepAudio != null && isGrounded) 
        {
            footstepAudio.Play();
        }
        
       // if(footstepAudio !=null && !isGrounded)
            
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Hareket girişi
        moveInput = Input.GetAxisRaw("Horizontal");

        // Sprite yönü
        if (moveInput > 0 && !facingRight) Flip();
        else if (moveInput < 0 && facingRight) Flip();

        // Ground Check
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // Coyote Timer
        if (isGrounded)
            coyoteTimeCounter = coyoteTime;
        else
            coyoteTimeCounter -= Time.deltaTime;

        // Jump Buffer
        if (Input.GetButtonDown("Jump"))
            jumpBufferCounter = jumpBufferTime;
        else
            jumpBufferCounter -= Time.deltaTime;

        // Zıplama
        if (jumpBufferCounter > 0 && coyoteTimeCounter > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpBufferCounter = 0;
        }

        // Değişken zıplama yüksekliği
        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }

        // 🟡 Animator parametrelerini güncelle
        animator.SetFloat("Speed", Mathf.Abs(moveInput));
        animator.SetBool("isCarrying", isCarryingAxolotl);
        
        
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Axolotl") && !isCarryingAxolotl)
        {
            isCarryingAxolotl = true;
            Destroy(other.gameObject); // Akselotu sahneden kaldır
        }

        if (other.CompareTag("Lake") && isCarryingAxolotl)
        {
            isCarryingAxolotl = false;
        }
    }
}