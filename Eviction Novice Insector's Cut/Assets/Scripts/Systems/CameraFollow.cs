using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 currentZoomDistance = new Vector3(0f, 2f, -10f);
    public Vector3 playerDefaultDistance;
    public Vector3 zoomDefaultDistance;
    public Vector3 itemZoomDistance;
    [SerializeField] float distanceDamp;
    [SerializeField] float rotationalDamp;
    public bool lookAt;

    Vector3 velocity = Vector3.one;

    public static CameraFollow instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        if (target)
        {
            transform.position = target.position + currentZoomDistance;
        }

        playerDefaultDistance = currentZoomDistance;
    }

    private void FixedUpdate()
    {
        SmoothFollow();
    }

    void SmoothFollow()
    {
        Vector3 toPos = target.position + (target.rotation * currentZoomDistance);
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
            currentZoomDistance = playerDefaultDistance;
        }
        else
        {
            currentZoomDistance = newDistance;
        }
    }
}
