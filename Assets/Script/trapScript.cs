using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trapScript : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        Destroy(this);
        Debug.Log("stp marche");
    }
}
