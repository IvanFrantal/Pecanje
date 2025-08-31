using System.Collections;
using UnityEngine;

public class boat : MonoBehaviour
{
    Rigidbody2D rb;
    Transform trans;
    float dirX;
    float moveSpeed = 10f;
    bool canControl = true;

    [Header("Rope attach point")]
    public Transform ropeAttachPoint;

    [Header("Bounce settings")]
    public float bounceBackDistance = 1f;

    [Header("Crash settings for collisionWallNew")]
    public float crashRotationZ = 330f;
    public float crashDisableTime = 1f;
    public float bounceForce = 5f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        trans = GetComponent<Transform>();
    }

    void Update()
    {
        if (canControl)
            dirX = Input.acceleration.x * moveSpeed;
        else
            dirX = 0f;
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(dirX, rb.linearVelocity.y);

        if (dirX != 0 && canControl)
        {
            if (dirX > 0)
                trans.eulerAngles = new Vector3(0, 180, 0);
            else
                trans.eulerAngles = new Vector3(0, 0, 0);

            AudioManagerGame.PlayLoopSFX(AudioManagerGame.instance.camac);
        }
        else
        {
            AudioManagerGame.StopLoopSFX();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("CollisionWall"))
        {
            AudioManagerGame.PlaySFX(AudioManagerGame.instance.brodSeZabioUzid);
            Vector2 bounceDir = new Vector2(-Mathf.Sign(dirX), 0);
            rb.position += bounceDir * bounceBackDistance;
        }

        if (other.CompareTag("collisionWallNew"))
        {
            StartCoroutine(HandleCrash());
        }

        if (other.CompareTag("CollisionWallExtra"))
        {
            rb.position = new Vector2(-23f, 3f);
            trans.position = new Vector3(-23f, 3f, 1f);
            //Debug.Log("sudario sam se");
        }
    }

    IEnumerator HandleCrash()
    {
        canControl = false;

        AudioManagerGame.PlaySFX(AudioManagerGame.instance.brodSeZabioUzid);

        float originalZ = trans.eulerAngles.z;
        trans.eulerAngles = new Vector3(trans.eulerAngles.x, trans.eulerAngles.y, originalZ + crashRotationZ);

        yield return new WaitForSeconds(crashDisableTime);

        trans.eulerAngles = new Vector3(trans.eulerAngles.x, trans.eulerAngles.y, originalZ);

        Vector2 bounceDir = new Vector2(-Mathf.Sign(dirX), 0).normalized;
        rb.linearVelocity = bounceDir * bounceForce;

        canControl = true;
    }
}
