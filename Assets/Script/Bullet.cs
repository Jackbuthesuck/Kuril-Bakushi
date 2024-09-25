using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Rigidbody rb;

    public float velocity;
    public float damage;
    public float lifeTime;
    public float angle;


    void Start()
    {
         angle = transform.eulerAngles.y;
    }

    void Update()
    {
        rb.linearVelocity = velocity * new Vector3(Mathf.Sin(transform.eulerAngles.y), 0, Mathf.Cos(transform.eulerAngles.y));

        lifeTime -= Time.deltaTime;
        if(lifeTime < 0)    Destroy(gameObject);
    }
}
