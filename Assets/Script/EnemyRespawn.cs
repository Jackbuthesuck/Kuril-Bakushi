using System.Threading;
using UnityEngine;

public class EnemyRespawn : MonoBehaviour
{
    public GameObject enemy;

    public Transform origin;
    public float timeUntilActive = 0f;   
    public float respawnInterval = 5f;
    public int howManyChildCanExist = 1;
    public float timer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (this.transform.childCount >= howManyChildCanExist)
        {
            timer = respawnInterval;
        }
    }
    void FixedUpdate()
    {
        if (timeUntilActive > 0)
        {
            timeUntilActive -= Time.deltaTime;
        }
        else
        {
            timer -= Time.deltaTime;
            if (timer < 0)
            {
                Instantiate(enemy, this.transform, worldPositionStays: false);
                timer = respawnInterval;
            }

        }
    }
    void OnGUI()
    {
        GUIStyle style = new GUIStyle();
        style.fontSize = 18;
    }
}
