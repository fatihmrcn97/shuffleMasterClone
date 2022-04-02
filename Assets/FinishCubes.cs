using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishCubes : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("card"))
        {
            Destroy(other.gameObject);

        }
    }
}
