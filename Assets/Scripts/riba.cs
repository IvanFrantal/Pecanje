using UnityEngine;

public class riba : MonoBehaviour
{
    public float speed = 10f;

    private Rigidbody2D rb;
    private Transform trans;

    private Vector2 lastSafePos;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        trans = transform;

        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        rb.freezeRotation = true;
    }

    void Start()
    {
        rb.linearVelocity = new Vector2(-speed, 0f);
        lastSafePos = rb.position;
    }

    void FixedUpdate()
    {
        lastSafePos = rb.position;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("CollisionWallFish"))
        {
            float dir = Mathf.Sign(rb.linearVelocity.x);
            rb.position = lastSafePos - new Vector2(dir * 0.01f, 0f);

            BounceX();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("CollisionWallFish"))
        {
            BounceX();
        }
    }

    private void BounceX()
    {
        speed = -speed;
        rb.linearVelocity = new Vector2(-speed, 0f);

        if (speed > 0f)
            trans.eulerAngles = new Vector3(0f, 0f, 0f);
        else
            trans.eulerAngles = new Vector3(0f, 180f, 0f);
    }
}
