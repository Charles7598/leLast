using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightController : MonoBehaviour
{
    public AttributesManager playerAtm;
    public AttributesManager enemyAtm;

    private void Update()
    {
        // deal player dmg to enemy
        if (Input.GetButtonDown("Fire1"))
        {
            playerAtm.DealDamage(enemyAtm.gameObject);
        }
    }
}
