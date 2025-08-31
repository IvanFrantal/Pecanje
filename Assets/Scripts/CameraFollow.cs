using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public string targetName = "udica_0(Clone)";
    public string fallbackTargetName = "boat";
    public float smoothTime = 0.3f;
    public Vector2 offset;

    private Vector2 velocity = Vector2.zero;
    private Transform currentTarget;

    void LateUpdate()
    {
        if (currentTarget == null || currentTarget.name != targetName)
        {
            GameObject targetObj = GameObject.Find(targetName);
            if (targetObj != null)
            {
                currentTarget = targetObj.transform;
            }
            else
            {
                GameObject fallback = GameObject.Find(fallbackTargetName);
                if (fallback != null)
                {
                    currentTarget = fallback.transform;
                }
            }
        }

        if (currentTarget == null) return;

        Vector2 targetPosition = (Vector2)currentTarget.position + offset;
        transform.position = Vector2.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        transform.position = new Vector3(transform.position.x, transform.position.y, -10f);
    }
}
