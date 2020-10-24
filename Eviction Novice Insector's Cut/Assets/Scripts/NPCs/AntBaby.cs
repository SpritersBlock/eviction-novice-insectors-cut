using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntBaby : MonoBehaviour
{
    Rigidbody rb;
    bool travellingRight;
    [SerializeField] float antSpeed;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        transform.localEulerAngles = new Vector3(90, 0, Random.Range(-360, 360));
        antSpeed += Random.Range(-4, 2);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.velocity = transform.up * antSpeed;
        if (travellingRight)
        {
            transform.localEulerAngles += new Vector3(0, 0, Random.Range(-2, 20));
        }
        else if (!travellingRight)
        {
            transform.localEulerAngles += new Vector3(0, 0, Random.Range(-20, 2));
        }

        Invoke("ReverseAntDirection", Random.Range(1,5));
    }

    void ReverseAntDirection()
    {
        travellingRight = !travellingRight;
    }
}
