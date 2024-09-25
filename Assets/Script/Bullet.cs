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
        if (angle < 90)
        {
            rb.linearVelocity = new Vector3(velocity * Mathf.Cos(transform.eulerAngles.y), 0, velocity * Mathf.Sin(transform.eulerAngles.y));
            if(angle < 180)
            {
                rb.linearVelocity = new Vector3(velocity * Mathf.Cos(transform.eulerAngles.y), 0, -velocity * Mathf.Sin(transform.eulerAngles.y));
                if (angle < 270)
                {
                    rb.linearVelocity = new Vector3(-velocity * Mathf.Cos(transform.eulerAngles.y), 0, -velocity * Mathf.Sin(transform.eulerAngles.y));
                }
            }
        } 
        else rb.linearVelocity = new Vector3(-velocity * Mathf.Cos(transform.eulerAngles.y), 0, velocity * Mathf.Sin(transform.eulerAngles.y));

        lifeTime -= Time.deltaTime;
        if(lifeTime < 0)    Destroy(gameObject);
    }
}
