using UnityEngine;
using UnityEngine.AI;

public class EnnemyAI : MonoBehaviour
{
    public NavMeshAgent agent;

    public Transform player;

    public LayerMask WhatIsGround, WhatIsPlayer;

    public float health;

    //Patrolling
    public Vector3 WalkPoint;
    bool WalkPointSet;
    public float WalkPointRange;

    //Attacking
    public float TimeBetweenAttacks;
    bool AlreadyAttacks;

    //States
    public float SightRange, AttackRange;
    public bool PlayerInSightRange, PlayerInAttackRange;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        //Check for sight and attack range
        PlayerInSightRange = Physics.CheckSphere(transform.position, SightRange, WhatIsPlayer);
        PlayerInAttackRange = Physics.CheckSphere(transform.position, AttackRange, WhatIsPlayer);

        if (!PlayerInSightRange && !PlayerInAttackRange) Patroling();
        if (PlayerInSightRange && !PlayerInAttackRange) ChasePlayer();
        if (PlayerInAttackRange && PlayerInSightRange) AttackPlayer();
 

    }


    private void Patroling()
    {
        if (!WalkPointSet) SearchWalkPoint();

        if (WalkPointSet)
            agent.SetDestination(WalkPoint);

        Vector3 distanceToWalkPoint = transform.position - WalkPoint;

        //WalkPoint reached 
        if (distanceToWalkPoint.magnitude < 1f)
            WalkPointSet = false;
    }

    private void SearchWalkPoint()
    {
        //Calculate random point in range 
        float randomZ = Random.Range(-WalkPointRange, WalkPointRange);
        float randomX = Random.Range(-WalkPointRange, WalkPointRange);

        WalkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(WalkPoint, -transform.up, 2f, WhatIsGround)) WalkPointSet = true;
    }


    private void ChasePlayer() 
    {
        agent.SetDestination(player.position);
    }
    private void AttackPlayer() 
    {
        //Make sur enemy doesn't move
        agent.SetDestination(transform.position);

        transform.LookAt(player);

        if (!AlreadyAttacks)
        {
            //Attack code
            AlreadyAttacks = true;
            Invoke(nameof(ResetAttack), TimeBetweenAttacks);
        }
              
    }
    private void ResetAttack()
    {
        AlreadyAttacks = false;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0) Invoke(nameof(Destroyenemy), .5f);
    }
    private void Destroyenemy()
    {
        Destroy(gameObject);
    }
}
