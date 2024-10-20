using UnityEngine;

public class LevelExitHud: MonoBehaviour
{
    public LevelExit levelExit;

    void Start()
    {
        levelExit = GameObject.Find("Player").GetComponent<LevelExit>();
    }

    void Update()
    {
       this.transform.localScale = new Vector3 (levelExit.timerNow / levelExit.timerMax - 1,1 ,1);
    }
    public void DoTheThing()
    {
        levelExit = GameObject.Find("Player").GetComponent<LevelExit>();
        this.transform.localScale = new Vector3(0, 1, 1);
    }
}
