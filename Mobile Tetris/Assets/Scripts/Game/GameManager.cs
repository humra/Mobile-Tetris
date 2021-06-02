using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject gameUI;
    [SerializeField]
    private GameObject pauseUI;

    private void Start()
    {
        TogglePauseUI();
    }

    private void TogglePauseUI()
    {
        pauseUI.SetActive(!pauseUI.activeSelf);
    }

    public void PauseGame()
    {
        //TO-DO
    }
}
