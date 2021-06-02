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
        ToggleSettingsVisibility();
    }

    public void ToggleSettingsVisibility()
    {
        settingsElements.SetActive(!settingsElements.activeSelf);
    }

    public void StartNewGame()
    {
        SceneManager.LoadScene("Game");
    }
}
