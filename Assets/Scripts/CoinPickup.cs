using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
  [SerializeField]
  private AudioClip clip;

  [SerializeField]
  private int coinPointValue = 100;

  bool wasCollected = false;

  private void OnTriggerEnter2D(Collider2D other)
  {
    if (other.tag == "Player" && !wasCollected)
    {
      wasCollected = true;
      FindObjectOfType<GameSession>().processScoreIncrease(coinPointValue);
      AudioSource.PlayClipAtPoint(clip, gameObject.transform.position);
      gameObject.SetActive(false);
      Destroy(gameObject);
    }
  }
}
