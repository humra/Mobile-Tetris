using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
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
    private Text gameOverScoreTxt;
    [SerializeField]
    private Image nextPieceImg;
    [SerializeField]
    private Sprite[] gamePieceSprites;

    private int score = 0;
    
    public void TogglePauseUI()
    {
        pauseUI.SetActive(!pauseUI.activeSelf);
    }

    public void ToggleGameOverUI()
    {
        gameOverUI.SetActive(!gameOverUI.activeSelf);
    }

    public void ToggleGameUI()
    {
        gameUI.SetActive(!gameUI.activeSelf);
    }

    public void UpdateGameUI()
    {
        scoreTxt.text = score.ToString();
    }

    public void SetScore(int score)
    {
        this.score = score;
    }

    public void GameOver()
    {
        gameOverScoreTxt.text = "You scored " + score;
        ToggleGameOverUI();
    }

    public void SetNextPieceImage(int index)
    {
        nextPieceImg.sprite = gamePieceSprites[index];
    }
}
