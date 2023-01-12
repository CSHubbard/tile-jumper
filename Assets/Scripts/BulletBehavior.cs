using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
  private Rigidbody2D bulletRigidbody;
  private CapsuleCollider2D bulletBodyCollider;
  PlayerMovement playerMovement;
  private float xSpeed;
  [SerializeField]
  private float bulletSpeed = 10f;

  private void Awake()
  {
    bulletRigidbody = GetComponent<Rigidbody2D>();
    bulletBodyCollider = GetComponent<CapsuleCollider2D>();
    playerMovement = FindObjectOfType<PlayerMovement>();
  }

  // Start is called before the first frame update
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {
    xSpeed = playerMovement.transform.localScale.x;
    bulletRigidbody.velocity += new Vector2(bulletSpeed * xSpeed, 0f);
  }

  private void OnTriggerEnter2D(Collider2D other)
  {
    if (other.tag == "Enemy")
    {
      Destroy(other.gameObject);
    }
    Destroy(gameObject);
  }

  private void OnCollisionEnter2D(Collision2D other)
  {
    Destroy(gameObject);
  }
}
