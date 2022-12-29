using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMovement : MonoBehaviour
{
  [SerializeField]
  private float runSpeed = 7f;

  [SerializeField]
  private float jumpSpeed = 20f;

  [SerializeField]
  private float climbSpeed = 5f;

  Vector2 moveInput;
  Rigidbody2D playerRigidbody;
  private Animator playerAnimator;

  CapsuleCollider2D playerBodyCollider;
  BoxCollider2D playerFeetCollider;
  private float defaultGravity;

  private void Awake()
  {
    playerRigidbody = GetComponent<Rigidbody2D>();
    playerAnimator = GetComponent<Animator>();
    playerBodyCollider = GetComponent<CapsuleCollider2D>();
    playerFeetCollider = GetComponent<BoxCollider2D>();
    defaultGravity = playerRigidbody.gravityScale;
  }

  // Start is called before the first frame update
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {
    bool playerHasHorizontalSpeed = MathF.Abs(playerRigidbody.velocity.x) > Mathf.Epsilon;
    bool playerHasVerticleSpeed = MathF.Abs(playerRigidbody.velocity.y) > Mathf.Epsilon;
    Run(playerHasHorizontalSpeed);
    FlipSprite(playerHasHorizontalSpeed);
    ClimbLadder(playerHasVerticleSpeed);
  }

  private void ClimbLadder(bool playerHasVerticleSpeed)
  {
    if (playerBodyCollider.IsTouchingLayers(LayerMask.GetMask("Climbable")))
    {
      playerRigidbody.gravityScale = 0f;
      Vector2 playerVelocity = new Vector2(playerRigidbody.velocity.x, moveInput.y * climbSpeed);
      playerRigidbody.velocity = playerVelocity;
      playerAnimator.SetBool("isClimbing", playerHasVerticleSpeed);
    }
    else
    {
      playerAnimator.SetBool("isClimbing", false);
      playerRigidbody.gravityScale = defaultGravity;
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
    if (!playerFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))) return;
    if (value.isPressed)
    {
      playerRigidbody.velocity += new Vector2(0, jumpSpeed);
    }
  }
}
