using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
  [SerializeField] private int playerLives = 3;
  [SerializeField] private int playerScore = 0;

  [SerializeField] TextMeshProUGUI livesText;

  [SerializeField] TextMeshProUGUI scoreText;

  [SerializeField] private float respawnDelay = 1.5f;

  void Awake()
  {
    int gameSessionCount = FindObjectsOfType<GameSession>().Length;
    if (gameSessionCount > 1)
    {
      Destroy(gameObject);
    }
    else
    {
      DontDestroyOnLoad(gameObject);
    }
  }

  void Start()
  {
    livesText.text = playerLives.ToString();
    scoreText.text = playerScore.ToString();
  }

  public void processPlayerDeath()
  {
    if (playerLives > 1)
    {
      TakeLife();
      livesText.text = playerLives.ToString();
    }
    else
    {
      ResetGameSession();
    }
  }

  private void ResetGameSession()
  {
    FindObjectOfType<ScenePersist>().ResetScenePersist();
    SceneManager.LoadScene(0);
    Destroy(gameObject);
  }

  private void TakeLife()
  {
    playerLives--;
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
  }

  public void processScoreIncrease(int amount)
  {
    playerScore += amount;
    scoreText.text = playerScore.ToString();
  }
}
