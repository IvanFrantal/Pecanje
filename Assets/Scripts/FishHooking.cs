using UnityEngine;

public class FishHook : MonoBehaviour
{
    private Transform hookPoint;
    private bool isHooked = false;
    private Vector3 offsetFromHook;

    private bool destroyOnBoat = false;

    void Update()
    {
        if (isHooked && hookPoint != null)
        {
            transform.position = hookPoint.position + offsetFromHook;
        }
    }

    public void HookTo(Transform hook, Vector2 udicaSize)
    {
        isHooked = true;
        hookPoint = hook;

        transform.rotation = Quaternion.Euler(0f, 0f, -90f);

        offsetFromHook = new Vector3(0, -udicaSize.y / 2f, 0);

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
            rb.simulated = false;

        destroyOnBoat = true;
    }

    public bool IsHooked()
    {
        return isHooked;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("nesto se dogada");
        //Debug.Log(destroyOnBoat + "Destroy");
        //Debug.Log(other.tag + "mytagother");
        if (destroyOnBoat && other.CompareTag("Boat"))
        {
            Destroy(gameObject);
        }
    }
}
