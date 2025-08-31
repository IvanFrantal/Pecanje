using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UdicaHook : MonoBehaviour
{
    public int maxFishCount = 1;
    public float ascendSpeed = 1.0f;
    public Transform boatTarget;

    private List<FishHook> hookedFish = new List<FishHook>();
    private bool isAscending = false;
    private Vector2 udicaSize;

    private Animator animator;
    private bool deliveryInProgress = false;

    void Start()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        if (sr != null)
            udicaSize = sr.bounds.size;
        else
            udicaSize = new Vector2(0.5f, 0.5f);
    }

    void Update()
    {
        if (isAscending && boatTarget != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, boatTarget.position, ascendSpeed * Time.deltaTime);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        FishHook fish = collision.GetComponent<FishHook>();
        if (fish != null && hookedFish.Count < maxFishCount && !fish.IsHooked())
        {
            fish.HookTo(transform, udicaSize);
            hookedFish.Add(fish);

            StartAscending();

            AudioManagerGame.PlaySFX(AudioManagerGame.instance.upecalaSeRiba);
            return;
        }

        if (collision.CompareTag("Boat") && hookedFish.Count > 0 && !deliveryInProgress)
        {
            deliveryInProgress = true;
            StartCoroutine(HandleFishDelivery());
        }
    }

    private IEnumerator HandleFishDelivery()
    {
        if (animator != null)
        {
            animator.SetTrigger("FishDelivered");
        }

        float clipLength = 0.5f;
        yield return new WaitForSeconds(clipLength);

        PointsDisplay pointsDisplay = FindObjectOfType<PointsDisplay>();
        foreach (FishHook hooked in hookedFish)
        {
            if (hooked != null)
            {
                Destroy(hooked.gameObject);
                if (pointsDisplay != null)
                    pointsDisplay.AddPoints(1);
            }
        }

        hookedFish.Clear();
        isAscending = false;

        AudioManagerGame.PlaySFX(AudioManagerGame.instance.uhvacenaRiba);

        deliveryInProgress = false;
    }

    void StartAscending()
    {
        isAscending = true;
    }

    public int GetHookedCount()
    {
        return hookedFish.Count;
    }
}
