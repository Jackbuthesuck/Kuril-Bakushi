using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Rigidbody rb;

    public float velocity;
    public float damage;
    public float lifeTime;

    public Transform origin;
    private Quaternion yes;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        origin = GetComponent<Transform>();
        transform.eulerAngles = origin.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        rb.linearVelocity = new Vector3(velocity, 0, 0);
        lifeTime -= Time.deltaTime;
        if(lifeTime < 0)    Destroy(gameObject);
    }
}
