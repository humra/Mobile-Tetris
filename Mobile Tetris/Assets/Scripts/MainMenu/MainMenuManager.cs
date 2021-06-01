using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{

    [SerializeField]
    private GameObject mainMenuElements;
    [SerializeField]
    private GameObject settingsElements;

    void Start()
    {
        settingsElements.SetActive(false);
    }

    public void ChangeSettingsVisibility()
    {
        settingsElements.SetActive(!settingsElements.activeSelf);
    }

    public void StartNewGame()
    {
        SceneManager.LoadScene("Game");
    }
}
