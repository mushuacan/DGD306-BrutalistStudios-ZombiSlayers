// ChatGPT tarafýndan oluþturulmuþtur.
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public Transform groundCheck;  // Karakterin altýndaki küçük collider
    public LayerMask groundLayer;  // Zeminin hangi layer'de olduðunu belirler

    private Rigidbody2D rb;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Karakteri saða-sola hareket ettir
        float moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        // Zeminde olup olmadýðýný kontrol et
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer); 

        // Zýplama iþlemi
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }
}