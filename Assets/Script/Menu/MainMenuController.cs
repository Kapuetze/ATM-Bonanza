using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartGame()
    {
        SceneManager.LoadScene("Level1 Neu", LoadSceneMode.Single);
    }

    public void NavigateInstructions()
    {
        SceneManager.LoadScene("Instructions", LoadSceneMode.Single);
    }

    public void NavigateMainMenu()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }

    public void NavigateSettings()
    {
        SceneManager.LoadScene("Settings", LoadSceneMode.Single);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
