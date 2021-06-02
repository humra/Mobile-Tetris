using UnityEngine;

public class GameManager : MonoBehaviour, IActivePieceControl, ISwipeDetectionControl
{
    [SerializeField]
    private GameObject gameUI;
    [SerializeField]
    private GameObject pauseUI;
    [SerializeField]
    private PlayAreaManager playAreaManager;
    [SerializeField]
    private InputManager inputManager;
    [SerializeField]
    private SwipeDetection swipeDetection;
    [SerializeField]
    private Spawner spawner;

    private ActivePieceManager activePieceManager;

    private void Awake()
    {
        swipeDetection.swipeDetectionControl = this;
    }

    private void Start()
    {
        TogglePauseUI();
        SpawnNextPiece();
    }

    private void TogglePauseUI()
    {
        pauseUI.SetActive(!pauseUI.activeSelf);
    }

    public void PauseGame()
    {
        //TO-DO
    }

    public void SpawnNextPiece()
    {
        GameObject newPiece = spawner.SpawnPiece();
        activePieceManager = newPiece.GetComponent<ActivePieceManager>();
        activePieceManager.activePieceControl = this;
        activePieceManager.isGamePaused = false;
    }

    public void MovePiece(MoveDirection moveDirection)
    {
        activePieceManager.MovePiece(moveDirection);
    }
}

public interface IActivePieceControl
{
    public void SpawnNextPiece();
}

public interface ISwipeDetectionControl
{
    public void MovePiece(MoveDirection moveDirection);
}
