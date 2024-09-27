using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask WhatIsGround, WhatIsPlayer;
    public GameObject bullet;
    private Quaternion yes;

    public float health;
    // Patroling
    public Vector3 walkPoint;
    bool walkPointIsSet;
    public float walkPointSearchRange;

    //Attack
    public float chamberingTime;
    bool isChambering;

    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;
    void Start()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, WhatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, WhatIsPlayer);
        if (playerInSightRange && playerInAttackRange) AttackPlayer();
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (!playerInSightRange && !playerInAttackRange) Patrol();
    }

    private void Patrol()
    {
        if (!walkPointIsSet) searchWalkpoint();
        if (walkPointIsSet) agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        if (distanceToWalkPoint.magnitude < 0.5f)
            walkPointIsSet = false;
    }

    private void searchWalkpoint()
    {
        float ranX = UnityEngine.Random.Range(-walkPointSearchRange, walkPointSearchRange);
        float ranZ = UnityEngine.Random.Range(-walkPointSearchRange, walkPointSearchRange);

        walkPoint = new Vector3(transform.position.x + ranX, transform.position.y, transform.position.z + ranZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, WhatIsGround))
            walkPointIsSet = true;
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }

    private void AttackPlayer()
    {
        agent.SetDestination(transform.position);

        transform.LookAt(player, Vector3.up);

        if (!isChambering)
        {
            isChambering = true;
            yes.eulerAngles = this.transform.eulerAngles;
            GameObject instantiatedBullet = Instantiate(bullet, this.transform.position, yes);
            instantiatedBullet.GetComponent<Bullet>().whoShotMe = gameObject;
            Invoke(nameof(Chamber), chamberingTime);
        }
    }

    private void Chamber()
    {
        isChambering = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            if (other.gameObject.GetComponent<Bullet>().whoShotMe.name == this.gameObject.name) { }
            else health -= other.gameObject.GetComponent<Bullet>().damage;
            if (health <= 0) Destroy(gameObject); 
        }
    }

    void OnGUI()
    {
        GUIStyle style = new GUIStyle();
        style.fontSize = 18;
    }
}
