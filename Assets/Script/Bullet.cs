using UnityEngine;
using UnityEngine.InputSystem.XR;

public class Bullet : MonoBehaviour
{
    public Rigidbody rb;

    public float speed;
    public float velocityMulAdjust = 1;
    public float damage;
    public int pellet;
    public float moa;
    public float lifeTime;
    public bool skipFrame = true;
    public int resumingFrame = 1;
    public GameObject whoShotMe;
    public LayerMask whatIsWall, whatIsPlayer, whatIsEnemy;

    private RaycastHit hit;
    private Quaternion yes;
    private Vector3 direction;
    void Awake()
    {
        rb.linearVelocity = speed / velocityMulAdjust * new Vector3(Mathf.Sin(Mathf.Deg2Rad * transform.eulerAngles.y), 0, Mathf.Cos(Mathf.Deg2Rad * transform.eulerAngles.y)); 
    }

    void Update()
    {
        if (skipFrame == false) resumingFrame -= 1;
        if (resumingFrame <= 0) resumingFrame = 0;
        else
        {
            if  (Physics.Raycast(this.transform.position, transform.TransformDirection(Vector3.forward), out hit, speed / velocityMulAdjust  * Time.deltaTime, whatIsEnemy)
                || Physics.Raycast(this.transform.position, transform.TransformDirection(Vector3.forward), out hit, speed / velocityMulAdjust  * Time.deltaTime, whatIsWall))
            {
                this.transform.position = hit.point;
                rb.linearVelocity = Vector3.zero;
            }
            lifeTime -= Time.deltaTime;
            if (lifeTime < 0) Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (resumingFrame > 0) { }
        else Destroy(gameObject);
    }
    private void OnTriggerExit(Collider other)
    {
        skipFrame = false;
    }
}
