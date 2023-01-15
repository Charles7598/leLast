using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttributesManager : MonoBehaviour
{
    public int health;
    public int attack;

    public void TakeDamage(int amount)
    {
        health -= amount;
    }

    public void DealDamage(GameObject target)
    {
        var atm = target.GetComponent<AttributesManager>();
        if (atm != null)
        {
            atm.TakeDamage(attack);
            // DeathMethod(target);
            if (atm.health <= 0)
            {
                Destroy(atm.gameObject);
                Debug.Log("Il est censé canner");
            }
        }
    }

    // public void DeathMethod(GameObject target)
    // {

    // }
}
