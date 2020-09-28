using UnityEngine;

public class Player : MonoBehaviour
{
    public LayerMask platformsLayerMask;
    public SpriteMask crouchSpriteMask;
    public float health = 3;
    public float jumpVelocity = 50f;
    public float forceFallVelocity = 50f;
    public float sideSpeed = 1f;
    public float minX = -1f;
    public float maxX = 1f;

    private Rigidbody2D rigidbody2d;
    private BoxCollider2D boxCollider2d;

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

        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector2.right * sideSpeed * Time.deltaTime);
            if (transform.position.x > maxX)
            {
                Vector2 pos = transform.position;
                pos.x = maxX;
                transform.position = pos;
            }
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector2.left * sideSpeed * Time.deltaTime);
            if (transform.position.x < minX)
            {
                Vector2 pos = transform.position;
                pos.x = minX;
                transform.position = pos;
            }
        }
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
