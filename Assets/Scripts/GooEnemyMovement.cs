using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GooEnemyMovement : MonoBehaviour
{
  private Rigidbody2D enemyRigidBody;
  private BoxCollider2D enemyBoxCollider;
  [SerializeField]
  private float moveSpeed = 1f;

  private void Awake()
  {
    enemyRigidBody = GetComponent<Rigidbody2D>();
    enemyBoxCollider = GetComponent<BoxCollider2D>();
  }

  // Start is called before the first frame update
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {
    enemyRigidBody.velocity = new Vector2(moveSpeed, 0);
  }

  private void OnTriggerExit2D(Collider2D other)
  {
    moveSpeed = -moveSpeed;
    FlipEnemySprite();
  }

  private void FlipEnemySprite()
  {
    transform.localScale = new Vector2(-1 * Mathf.Sign(enemyRigidBody.velocity.x), 1f);
  }
}
