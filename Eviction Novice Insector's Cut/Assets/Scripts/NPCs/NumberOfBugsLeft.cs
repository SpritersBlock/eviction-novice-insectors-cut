using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberOfBugsLeft : MonoBehaviour
{
    public int numberOfBugsTotal = 1;
    public int numberOfBugsLeft = 1;
    public static NumberOfBugsLeft instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        numberOfBugsLeft = numberOfBugsTotal;
    }

    public void RemoveOneBug()
    {
        numberOfBugsLeft--;
    }
}
