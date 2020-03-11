using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 defaultDistance = new Vector3(0f, 2f, -10f);
    public Vector3 playerDefaultDistance;
    public Vector3 zoomDefaultDistance;
    [SerializeField] float distanceDamp;
    [SerializeField] float rotationalDamp;
    public bool lookAt;

    Vector3 velocity = Vector3.one;

    private void Start()
    {
        if (target)
        {
            transform.position = target.position + defaultDistance;
        }

        playerDefaultDistance = defaultDistance;
    }

    private void FixedUpdate()
    {
        SmoothFollow();
    }

    void SmoothFollow()
    {
        Vector3 toPos = target.position + (target.rotation * defaultDistance);
        Vector3 curPos = Vector3.SmoothDamp(transform.position, toPos, ref velocity, distanceDamp);
        transform.position = curPos;

        if (lookAt)
        {
            transform.LookAt(target, target.up);
        }
    }

    public void ChangeDefaultDistance(bool backToPlayer, Vector3 newDistance)
    {
        if (backToPlayer)
        {
            defaultDistance = playerDefaultDistance;
        }
        else
        {
            defaultDistance = newDistance;
        }
    }
}
