using TMPro;
using UnityEngine;

public class ScoreHud : MonoBehaviour
{
    public TextMeshProUGUI textMeshPro;
    public int kill;

    void Start()
    {
        textMeshPro = this.GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {

    }
    public void DoTheThing()
    {
        textMeshPro.text = string.Format("K:{0}", kill);
    }
}