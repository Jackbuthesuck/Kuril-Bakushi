using UnityEngine;

public class HealthRegenBar : MonoBehaviour
{
    public PlayerController playerController;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.localScale = new Vector3(1, 1, 1);
    }

    public void DoTheThing()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        this.transform.localScale = new Vector3(1, playerController.healthRegenTimer / 5f, 1);
    }
}
