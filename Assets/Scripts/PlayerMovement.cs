using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMovement : MonoBehaviour
{
  [SerializeField]
  private float runSpeed = 5f;

  [SerializeField]
  private float jumpSpeed = 20f;

  [SerializeField]
  private float climbSpeed = 5f;

  Vector2 moveInput;
  Rigidbody2D playerRigidbody;
  private Animator playerAnimator;

  CapsuleCollider2D playerCollider;


  private void Awake()
  {
    playerRigidbody = GetComponent<Rigidbody2D>();
    playerAnimator = GetComponent<Animator>();
    playerCollider = GetComponent<CapsuleCollider2D>();
  }

  // Start is called before the first frame update
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {
    bool playerHasHorizontalSpeed = MathF.Abs(playerRigidbody.velocity.x) > Mathf.Epsilon;
    Run(playerHasHorizontalSpeed);
    FlipSprite(playerHasHorizontalSpeed);
    ClimbLadder();
  }

  private void ClimbLadder()
  {
    if (playerCollider.IsTouchingLayers(LayerMask.GetMask("Climbable")))
    {
      Vector2 playerVelocity = new Vector2(playerRigidbody.velocity.x, moveInput.y * climbSpeed);
      playerRigidbody.velocity = playerVelocity;
    }
  }

  private void Run(bool playerHasHorizontalSpeed)
  {
    Vector2 playerVelocity = new Vector2(moveInput.x * runSpeed, playerRigidbody.velocity.y);
    playerRigidbody.velocity = playerVelocity;
    playerAnimator.SetBool("isRunning", playerHasHorizontalSpeed);
  }

  private void FlipSprite(bool playerHasHorizontalSpeed)
  {
    if (playerHasHorizontalSpeed)
    {
      transform.localScale = new Vector2(Mathf.Sign(playerRigidbody.velocity.x), 1f);
    }
  }

  void OnMove(InputValue value)
  {
    moveInput = value.Get<Vector2>();
  }

  void OnJump(InputValue value)
  {
    if (!playerCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))) return;
    if (value.isPressed)
    {
      playerRigidbody.velocity += new Vector2(0, jumpSpeed);
    }
  }
}
