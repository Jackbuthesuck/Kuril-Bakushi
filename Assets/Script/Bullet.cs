using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float vel;
    public float damage;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(vel, 0, 0);
    }
}
