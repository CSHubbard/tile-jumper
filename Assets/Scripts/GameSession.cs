using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
  [SerializeField]
  private int playerLives = 3;

  [SerializeField]
  TextMeshProUGUI livesText;

  [SerializeField]
  TextMeshProUGUI scoreText;

  [SerializeField]
  private float respawnDelay = 1.5f;

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

  public void processPlayerDeath()
  {
    if (playerLives > 1)
    {
      TakeLife();
    }
    else
    {
      ResetGameSession();
    }
  }

  private void ResetGameSession()
  {
    SceneManager.LoadScene(0);
    Destroy(gameObject);
  }

  private void TakeLife()
  {
    playerLives--;
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
  }
}
