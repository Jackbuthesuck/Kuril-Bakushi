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
        if (this.transform.childCount > 0)
        {
            timer = 50;
        }
    }
    void FixedUpdate()
    {
        timer -= 1;
        if (timer < 0)
        {
            Instantiate(enemy, this.transform, worldPositionStays: false);
            timer = 50;
        }
    }

    void OnGUI()
    {
        GUIStyle style = new GUIStyle();
        style.fontSize = 18;
    }
}
