using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [SerializeField]
    private GameObject MainMenuCanvas;
    [SerializeField]
    private GameObject SettingsCanvas;
    [SerializeField]
    private GameObject InstructionsCanvas;


    void Awake()
    {
        SettingsCanvas.SetActive(false);
        InstructionsCanvas.SetActive(false);
    }

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

    public void NavigteInstructions()
    {
        MainMenuCanvas.SetActive(false);
        SettingsCanvas.SetActive(false);
        InstructionsCanvas.SetActive(true);
    }

    public void NavigateSettings()
    {
        MainMenuCanvas.SetActive(false);
        SettingsCanvas.SetActive(true);
        InstructionsCanvas.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
