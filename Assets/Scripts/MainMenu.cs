using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    GameObject mainMenuPanel;
    [SerializeField]
    GameObject helpPanel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnPlayClick()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void OnHelpClick()
    {
        mainMenuPanel.SetActive(false);
        helpPanel.SetActive(true);
    }

    public void OnQuitClick()
    {
        Application.Quit();
    }

    public void OnBackClick()
    {
        mainMenuPanel.SetActive(true);
        helpPanel.SetActive(false);
    }
}
