using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Ball ball;
    
    [Header("Paddles")]
    [SerializeField] private Paddle playerPaddle;
    [SerializeField] private Paddle computerPaddle;
    
    [SerializeField] private TextMeshProUGUI playerScoreText;
    [SerializeField] private TextMeshProUGUI computerScoreText;
    [SerializeField] private Text winMessageText; // Reference to your win message text

    [SerializeField] private GameObject winPanel;
    private int _playerScore;
    private int _computerScore;

    private void Start()
    {
        NewGame();
    }
    
    public void NewGame()
    {
        SetPlayerScore(0);
        SetComputerScore(0);
        
        winMessageText.gameObject.SetActive(false);
        winPanel.gameObject.SetActive(false);
        
        ResumeGame();
        NewRound();
    }

    private void NewRound()
    {
        playerPaddle.ResetPosition();
        computerPaddle.ResetPosition();
        ball.ResetPosition();

        CancelInvoke();
        Invoke(nameof(StartRound), 2f);
    }

    private void StartRound()
    {
        ball.AddStartingForce();
    }

    public void OnPlayerScored()
    {
        SetPlayerScore(_playerScore + 1);
        CheckForWin();
    }

    public void OnComputerScored()
    {
        SetComputerScore(_computerScore + 1);
        CheckForWin();
    }

    private void SetPlayerScore(int score)
    {
        _playerScore = score;
        playerScoreText.text = score.ToString();
        CheckForWin();
    }

    private void SetComputerScore(int score)
    {
        _computerScore = score;
        computerScoreText.text = score.ToString();
    }

    private void CheckForWin()
    {
        if (_playerScore >= 10)
        {
            winMessageText.text = "You win!";
            winMessageText.gameObject.SetActive(true);
            EndGame();
        }
        else if (_computerScore >= 10)
        {
            winMessageText.text = "Game over!";
            winMessageText.gameObject.SetActive(true);
            EndGame();
        }
    }

    private void PauseGame()
    {
        Time.timeScale = 0.05f;
    }

    private void ResumeGame()
    {
        Time.timeScale = 1;
    }
    
    private void EndGame()
    {
        winPanel.gameObject.SetActive(true);
        PauseGame();
    }
}