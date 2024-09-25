using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Rigidbody rb;

    public float velocity;
    public float damage;
    public float lifeTime;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        rb.linearVelocity = new Vector3(velocity * Mathf.Cos(transform.eulerAngles.y), 0, velocity * Mathf.Sin(transform.eulerAngles.y));
        lifeTime -= Time.deltaTime;
        if(lifeTime < 0)    Destroy(gameObject);
    }
}
