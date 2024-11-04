using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public GameObject endlessPanel;
    public GameObject masterVariableContainer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        endlessPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void startButton()
    {
        SceneManager.LoadScene("WoodUSECBase");
    }

    public void endlessButton()
    {
        if (endlessPanel.activeInHierarchy) endlessPanel.SetActive(false);
        else endlessPanel.SetActive(true);
    }

}
