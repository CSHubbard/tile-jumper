using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
  [SerializeField]
  private AudioClip clip;
  

  private void OnTriggerEnter2D(Collider2D other)
  {
    if (other.tag == "Player")
    {
      AudioSource.PlayClipAtPoint(clip, gameObject.transform.position);
      Destroy(gameObject);
    }
  }
}
