using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour, IActivePieceControl, ISwipeDetectionControl
{
    [SerializeField]
    private GameObject gameUI;
    [SerializeField]
    private GameObject pauseUI;
    [SerializeField]
    private GameObject gameOverUI;
    [SerializeField]
    private Text scoreTxt;
    [SerializeField]
    private PlayAreaManager playAreaManager;
    [SerializeField]
    private InputManager inputManager;
    [SerializeField]
    private SwipeDetection swipeDetection;
    [SerializeField]
    private Spawner spawner;
    [SerializeField]
    private SoundEffectManager soundEffectManager;
    [SerializeField]
    private Text gameOverScoreTxt;

    private ActivePieceManager activePieceManager;
    private bool isGamePaused = false;
    private int score = 0;

    private void Awake()
    {
        swipeDetection.swipeDetectionControl = this;
    }

    private void Start()
    {
        TogglePauseUI();
        ToggleGameOverUI();
        SpawnNextPiece();
        UpdateGameUI();
    }

    private void TogglePauseUI()
    {
        pauseUI.SetActive(!pauseUI.activeSelf);
    }

    private void ToggleGameOverUI()
    {
        gameOverUI.SetActive(!gameOverUI.activeSelf);
    }

    private void ToggleGameUI()
    {
        gameUI.SetActive(!gameUI.activeSelf);
    }

    private void UpdateGameUI()
    {
        scoreTxt.text = score.ToString();
    }

    public void PauseGame()
    {
        isGamePaused = true;
        activePieceManager.SetPaused(isGamePaused);
        TogglePauseUI();
        ToggleGameUI();
    }

    public void ResumeGame()
    {
        isGamePaused = false;
        activePieceManager.SetPaused(isGamePaused);
        TogglePauseUI();
        ToggleGameUI();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void SpawnNextPiece()
    {
        GameObject newPiece = spawner.SpawnPiece();
        activePieceManager = newPiece.GetComponent<ActivePieceManager>();
        activePieceManager.activePieceControl = this;
    }

    public void GameOver()
    {
        isGamePaused = true;
        activePieceManager.SetPaused(true);
        soundEffectManager.PlaySoundEffect(SoundEffects.GameOver);
        ToggleGameUI();
        gameOverScoreTxt.text = "You scored " + score;
        ToggleGameOverUI();
    }

    public void RowsRemoved(int numberRemoved)
    {
        if(numberRemoved > 0)
        {
            soundEffectManager.PlaySoundEffect(SoundEffects.RemoveRow);
        }

        switch (numberRemoved)
        {
            case 0:
                break;
            case 1:
                score += 100;
                break;
            case 2:
                score += 400;
                break;
            case 3:
                score += 1000;
                break;
            default:
                score += 3000;
                break;
        }

        UpdateGameUI();
    }

    public void MovePiece(MoveDirection moveDirection)
    {
        if(!isGamePaused)
        {
            activePieceManager.MovePiece(moveDirection);
            
            if(moveDirection == MoveDirection.Rotate)
            {
                soundEffectManager.PlaySoundEffect(SoundEffects.RotatePiece);
            }
            else
            {
                soundEffectManager.PlaySoundEffect(SoundEffects.MovePiece);
            }
        }
    }
}

public interface IActivePieceControl
{
    public void SpawnNextPiece();
    public void GameOver();
    public void RowsRemoved(int numberRemoved);
}

public interface ISwipeDetectionControl
{
    public void MovePiece(MoveDirection moveDirection);
}
