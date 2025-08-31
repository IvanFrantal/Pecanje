using UnityEngine;

public class Rope : MonoBehaviour
{
    public Rigidbody2D hook;
    public GameObject[] prefabRopeSegs;
    public int numLinks = 5;

    [Header("Boat reference")]
    public boat boatScript;

    void Start()
    {
        if (hook != null)
            hook.isKinematic = true;

        GenerateRope();
    }

    void LateUpdate()
    {
        if (boatScript != null && boatScript.ropeAttachPoint != null && hook != null)
        {
            hook.position = boatScript.ropeAttachPoint.position;
            hook.rotation = boatScript.ropeAttachPoint.eulerAngles.z;
        }
    }


    void GenerateRope()
    {
        Rigidbody2D prevBod = hook;
        GameObject newSeg;
        HingeJoint2D hj;
        int index;

        for (int i = 0; i < numLinks; i++)
        {
            index = Random.Range(0, prefabRopeSegs.Length - 2);

            newSeg = Instantiate(prefabRopeSegs[index]);
            newSeg.transform.parent = transform;
            newSeg.transform.position = transform.position;

            hj = newSeg.GetComponent<HingeJoint2D>();
            hj.connectedBody = prevBod;

            prevBod = newSeg.GetComponent<Rigidbody2D>();
        }

        index = prefabRopeSegs.Length - 1;
        newSeg = Instantiate(prefabRopeSegs[index]);
        newSeg.transform.parent = transform;
        newSeg.transform.position = transform.position;

        hj = newSeg.GetComponent<HingeJoint2D>();
        hj.connectedBody = prevBod;
    }
}
