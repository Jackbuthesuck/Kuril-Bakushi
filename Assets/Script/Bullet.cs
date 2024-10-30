using UnityEngine;
using UnityEngine.InputSystem.XR;

public class Bullet : MonoBehaviour
{
    public Rigidbody rb;

    public float velocity;
    public float damage;
    public int pellet;
    public float moa;
    public float lifeTime;
    public bool willDie;

    public GameObject whoShotMe;
    public LayerMask whatIsWall, whatIsPlayer, whatIsEnemy;

    private RaycastHit hit;
    private Quaternion yes;
    private Vector3 direction;
    void Awake()
    {

    }

    void Update()
    {
        if (willDie) Destroy(gameObject);
        rb.linearVelocity = velocity * 5 * Time.deltaTime * new Vector3(Mathf.Sin(Mathf.Deg2Rad * transform.eulerAngles.y), 0, Mathf.Cos(Mathf.Deg2Rad * transform.eulerAngles.y));
        if (Physics.Raycast(this.transform.position, transform.TransformDirection(Vector3.forward), out hit, velocity / 5 * Time.deltaTime, whatIsPlayer) 
            || Physics.Raycast(this.transform.position, transform.TransformDirection(Vector3.forward), out hit, velocity / 5 * Time.deltaTime, whatIsWall))
        {
            this.transform.position = hit.point;
            willDie = true;
            velocity = 0;
        }

        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * velocity, Color.yellow);
        lifeTime -= Time.deltaTime;
        if(lifeTime < 0)    Destroy(gameObject);
    }
    private void OnCollisionEnter(Collision other)
    {
        Destroy(gameObject);
    }
}
