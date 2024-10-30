using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SocialPlatforms.Impl;

public class EnemyController : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask WhatIsGround, WhatIsPlayer, WhatIsWall;
    public GameObject bullet;
    public ScoreHud score;
    private Quaternion yes;

    public float health;
    public float waitTimeUntilPatrol;

    // Patroling
    public Vector3 walkPoint;
    public bool walkPointIsSet;
    public float walkPointSearchRange;
    public float timeUntilPatrol;
    public Vector3 lastKnownPosition;

    //Attack
    public float chamberingTime;
    bool isChambering;

    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange, haveLastKnownPosition;
    void Start()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, WhatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, WhatIsPlayer);
        if (playerInSightRange && playerInAttackRange   && Physics.Raycast(this.transform.position, player.transform.position - this.transform.position, Vector3.Distance(this.transform.position, player.transform.position), WhatIsPlayer) 
                                                        && !Physics.Raycast(this.transform.position, player.transform.position - this.transform.position, Vector3.Distance(this.transform.position, player.transform.position), WhatIsWall))  AttackPlayer();
        if (playerInSightRange && !playerInAttackRange  && Physics.Raycast(this.transform.position, player.transform.position - this.transform.position, Vector3.Distance(this.transform.position, player.transform.position), WhatIsPlayer) 
                                                        && !Physics.Raycast(this.transform.position, player.transform.position - this.transform.position, Vector3.Distance(this.transform.position, player.transform.position), WhatIsWall))   ChasePlayer();
            else if (haveLastKnownPosition) GoLastKnownPosition();
            else if (timeUntilPatrol <= 0)  Patrol();
            else timeUntilPatrol -= Time.deltaTime;
    }

    private void Patrol()
    {
        if (!walkPointIsSet || (Physics.Raycast(this.transform.position, walkPoint - this.transform.position, 0.25f, WhatIsWall)) 
                            || Physics.CheckSphere(walkPoint, 0.25f, WhatIsWall)) searchWalkpoint();
        if (walkPointIsSet) agent.SetDestination(walkPoint);

        if (Vector3.Distance(this.transform.position, walkPoint) < 1f)
        {
            timeUntilPatrol = waitTimeUntilPatrol;
            walkPointIsSet = false;
        }   
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
        lastKnownPosition = player.position;
        haveLastKnownPosition = true;
    }
    
    private void GoLastKnownPosition()
    {
        agent.SetDestination(lastKnownPosition);
        if (Vector3.Distance(this.transform.position, lastKnownPosition) < 0.5f)
        {
            haveLastKnownPosition = false;
            timeUntilPatrol = waitTimeUntilPatrol;
        }
    }

    private void AttackPlayer()
    {
        agent.SetDestination(transform.position);
        lastKnownPosition = player.position;
        haveLastKnownPosition = true;
        transform.LookAt(player, Vector3.up);

        if (!isChambering)
        {
            isChambering = true;
            yes.eulerAngles = this.transform.eulerAngles;
            GameObject instantiatedBullet = Instantiate(bullet, this.transform.position, yes);
            instantiatedBullet.GetComponent<Bullet>().whoShotMe = gameObject;
            Invoke(nameof(Chamber), chamberingTime);
        }

        haveLastKnownPosition = false;
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
            if (health <= 0)
            {
                score = GameObject.Find("Score Hud").GetComponent<ScoreHud>();
                score.kill++;
                score.DoTheThing();
                Destroy(gameObject);
            }
        }
    }

    void OnGUI()
    {
        GUIStyle style = new GUIStyle();
        style.fontSize = 18;
    }
}
