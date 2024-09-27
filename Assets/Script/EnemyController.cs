using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask WhatIsGround, WhatIsPlayer;
    public GameObject bullet;

    private Quaternion yes;

    private string whoAmI = "Jame";
    // Patroling
    public Vector3 walkPoint;
    bool walkPointIsSet;
    public float walkPointSearchRange;

    //Attack
    public float ChamberingTime;
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
            GameObject instantiatedBullet = Instantiate(bullet, this.transform.position, yes, bullet.whoShotMe = this.gameObject);
            instantiatedBullet.whoShotMe = gameObject;
            Invoke(nameof(Chamber), ChamberingTime);
        }
    }

    private void Chamber()
    {
        isChambering = false;
    }

    private void OnTriggerEnter(Collider thing)
    {
        if (thing.CompareTag("Bullet"))
        {
            if (thing.gameObject.whoShotMe == this.gameObject.name) { }
            else Destroy(gameObject);
        }
    }

    void OnGUI()
    {
        GUIStyle style = new GUIStyle();
        style.fontSize = 18;
        GUI.Label(new Rect(400, 30, 0, 0), "Walkpoint set: " + walkPointIsSet, style);
        GUI.Label(new Rect(400, 50, 0, 0), "isChambering : " + isChambering, style);
    }
}
