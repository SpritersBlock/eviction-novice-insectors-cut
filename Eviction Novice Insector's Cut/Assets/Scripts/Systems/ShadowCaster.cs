using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowCaster : MonoBehaviour
{
    [SerializeField] float rayToGroundDistance = 3;
    [SerializeField] GameObject shadow;
    [SerializeField] float shadowSize = 0.25f;
    [SerializeField] float distanceFromGround = 0.01f;

    private void Start()
    {
        shadow.transform.localScale = new Vector3(shadowSize, shadowSize, shadowSize);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        int layerMask = 1 << 9;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, rayToGroundDistance, layerMask))
        {
            Vector3 targetLocation = hit.point;
            targetLocation += new Vector3(0, distanceFromGround, 0);
            if (!shadow.activeSelf)
            {
                shadow.SetActive(true);
            }
            shadow.transform.position = targetLocation;
        }
        else
        {
            if (shadow.activeSelf)
            {
                shadow.SetActive(false);
            }
        }
    }
}
