using UnityEngine;

public class ArmorBar : MonoBehaviour
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

    }

    public void DoTheThing()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        this.transform.localScale = new Vector3(1, playerController.armor / 1000f, 1);
    }
}