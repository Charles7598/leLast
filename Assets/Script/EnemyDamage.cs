using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // if (other.tag == "Sword")
        // {
            Debug.Log("il est mort le con");
            Destroy(this);
        // }            
    }
}
