using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Rigidbody rb;

    public float velocity;
    public float damage;
    public int pellet;
    public float moa;
    public float spread;
    public float lifeTime;

    public GameObject whoShotMe;

    void Start()
    {
        spread = moa * 0.0166667f;
    }

    void Update()
    {
        rb.linearVelocity = velocity * new Vector3(Mathf.Sin(Mathf.Deg2Rad * transform.eulerAngles.y), 0, Mathf.Cos(Mathf.Deg2Rad * transform.eulerAngles.y));

        lifeTime -= Time.deltaTime;
        if(lifeTime < 0)    Destroy(gameObject);
    }
    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}
