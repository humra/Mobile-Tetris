using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour, IActivePieceControl, ISwipeDetectionControl
{
    
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
    private UIManager uiManager;

    private ActivePieceManager activePieceManager;
    private bool isGamePaused = false;
    

    private void Awake()
    {
        Time.timeScale = 1f;
        swipeDetection.swipeDetectionControl = this;
    }

    private void Start()
    {
        uiManager.TogglePauseUI();
        uiManager.ToggleGameOverUI();
        SpawnNextPiece();
        uiManager.UpdateGameUI();
    }

    public void PauseGame()
    {
        isGamePaused = true;
        activePieceManager.SetPaused(isGamePaused);
        uiManager.TogglePauseUI();
        uiManager.ToggleGameUI();
    }

    public void ResumeGame()
    {
        isGamePaused = false;
        activePieceManager.SetPaused(isGamePaused);
        uiManager.TogglePauseUI();
        uiManager.ToggleGameUI();
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
        uiManager.SetNextPieceImage(spawner.GetNextPieceIndex());
    }

    public void GameOver()
    {
        isGamePaused = true;
        activePieceManager.SetPaused(true);
        soundEffectManager.PlaySoundEffect(SoundEffects.GameOver);
        uiManager.ToggleGameUI();
        uiManager.GameOver();
        Time.timeScale = 0f;
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
                uiManager.AddScore(100);
                break;
            case 2:
                uiManager.AddScore(400);
                break;
            case 3:
                uiManager.AddScore(1000);
                break;
            default:
                uiManager.AddScore(3000);
                break;
        }

        uiManager.UpdateGameUI();
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
