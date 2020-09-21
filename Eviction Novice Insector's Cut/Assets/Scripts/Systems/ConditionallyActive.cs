using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionallyActive : MonoBehaviour
{
    public void UpdateActiveness(bool shouldBeActive)
    {
        if (shouldBeActive != isActiveAndEnabled)
        {
            gameObject.SetActive(shouldBeActive);
        }
    }
}
