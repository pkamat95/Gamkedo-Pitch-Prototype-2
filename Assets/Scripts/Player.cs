using UnityEngine;

public class Player : MonoBehaviour
{
    public LayerMask platformsLayerMask;
    public SpriteMask crouchSpriteMask;
    public float health = 3;

    private Rigidbody2D rigidbody2d; 
    private BoxCollider2D boxCollider2d;
    private float jumpVelocity = 50f;
    private float forceFallVelocity = 50f;

    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        boxCollider2d = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        bool isGrounded = IsGrounded();
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rigidbody2d.velocity = Vector2.up * jumpVelocity;
        }
        if (!isGrounded && Input.GetKey(KeyCode.S))
        {
            rigidbody2d.AddForce(Vector2.down * forceFallVelocity);
        }
        Crouch(Input.GetKey(KeyCode.S));
    }

    private bool IsGrounded()
    {
        RaycastHit2D raycastHit2d = Physics2D.BoxCast(boxCollider2d.bounds.center, boxCollider2d.bounds.size, 0f, Vector2.down, .1f, platformsLayerMask);
        return raycastHit2d.collider != null;
    }

    public void Crouch(bool shouldCrouch)
    {
        if (shouldCrouch)
        {
            float crouchSize = 0.5f;
            float verticalOffset = -0.25f;

            boxCollider2d.size = new Vector2(boxCollider2d.size.x, crouchSize);
            boxCollider2d.offset = new Vector2(boxCollider2d.offset.x, verticalOffset);

            Vector3 spriteMaskScale = crouchSpriteMask.transform.localScale;
            spriteMaskScale.y = crouchSize;
            crouchSpriteMask.transform.localScale = spriteMaskScale;

            Vector3 spriteMaskPos = crouchSpriteMask.transform.localPosition;
            spriteMaskPos.y = verticalOffset;
            crouchSpriteMask.transform.localPosition = spriteMaskPos;
        }
        else {
            float standSize = 1f;
            float verticalOffset = 0f;

            boxCollider2d.size = new Vector2(boxCollider2d.size.x, standSize);
            boxCollider2d.offset = new Vector2(boxCollider2d.offset.x, verticalOffset);

            Vector3 spriteMaskScale = crouchSpriteMask.transform.localScale;
            spriteMaskScale.y = standSize;
            crouchSpriteMask.transform.localScale = spriteMaskScale;

            Vector3 spriteMaskPos = crouchSpriteMask.transform.localPosition;
            spriteMaskPos.y = -verticalOffset;
            crouchSpriteMask.transform.localPosition = spriteMaskPos;
        }
    }
}
