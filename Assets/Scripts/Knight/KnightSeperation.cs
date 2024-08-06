using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightSeperation : MonoBehaviour
{
     private void Start()
    {
        var rigidbody = GetComponent<Rigidbody>();
        rigidbody.AddForce(Random.insideUnitSphere * 5f, ForceMode.Impulse);
    }
}
