using System.Threading;
using UnityEngine;

public class EnemyRespawn : MonoBehaviour
{
    public GameObject enemy;

    public Transform origin;

    public float timerStart = 5f;
    private float timer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void FixedUpdate()
    {
        timer -= 1;
        if (timer < 0)
        {
            Instantiate(enemy, origin.position, Quaternion.identity);
            timer = 50;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            timer = timerStart;
        }
    }
    void OnGUI()
    {
        GUIStyle style = new GUIStyle();
        style.fontSize = 18;
        GUI.Label(new Rect(200, 30, 0, 0), "Spawn: " + timer, style);

    }
}
