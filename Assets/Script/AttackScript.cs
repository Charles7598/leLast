using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackScript : MonoBehaviour
{
    Animator anim;

    // on recupere les stats de l'attributes manager
    public AttributesManager playerAtm;
    public AttributesManager enemyAtm;

    private BoxCollider Box;

    // Start is called before the first frame update
    private void Start()
    {
        anim = GetComponent<Animator>();
        Box = GetComponent<BoxCollider>();
        // Box.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
            // Box.SetActive(true);
            anim.SetBool("Attacking", true);
        else if (Input.GetButtonUp("Fire1"))
            // Box.SetActive(false);
            anim.SetBool("Attacking", false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            Debug.Log("il prend des coups le bougre");
            playerAtm.DealDamage(enemyAtm.gameObject);
        }            
    }
}
