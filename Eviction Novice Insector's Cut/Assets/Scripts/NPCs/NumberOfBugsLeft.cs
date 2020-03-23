using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberOfBugsLeft : MonoBehaviour
{
    public int numberOfBugsTotal = 1;
    public int numberOfBugsLeft = 1;

    private void Start()
    {
        numberOfBugsLeft = numberOfBugsTotal;
    }
}
