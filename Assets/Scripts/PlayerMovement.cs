using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
  [SerializeField]
  private float runSpeed = 7f;

  [SerializeField]
  private float jumpSpeed = 20f;

  [SerializeField]
  private float climbSpeed = 5f;

  [SerializeField]
  Vector2 deathFling = new Vector2(20f, 20f);

  [SerializeField]
  GameObject bullet;

  [SerializeField]
  Transform gun;

  Vector2 moveInput;
  Rigidbody2D playerRigidbody;
  private Animator playerAnimator;

  CapsuleCollider2D playerBodyCollider;
  BoxCollider2D playerFeetCollider;
  private SpriteRenderer playerSpriteRenderer;
  private Transform playerGun;
  private float defaultGravity;
  private bool isAlive = true;
  private float respawnDelay = 1.5f;

  private void Awake()
  {
    playerRigidbody = GetComponent<Rigidbody2D>();
    playerAnimator = GetComponent<Animator>();
    playerBodyCollider = GetComponent<CapsuleCollider2D>();
    playerFeetCollider = GetComponent<BoxCollider2D>();
    playerSpriteRenderer = GetComponent<SpriteRenderer>();
    playerGun = GetComponentInChildren<Transform>();
    defaultGravity = playerRigidbody.gravityScale;
  }

  // Start is called before the first frame update
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {
    if (!isAlive) return;
    Run();
    FlipSprite();
    ClimbLadder();
    Die();
  }

  private void ClimbLadder()
  {
    if (playerBodyCollider.IsTouchingLayers(LayerMask.GetMask("Climbable")))
    {
      playerRigidbody.gravityScale = 0f;
      Vector2 playerVelocity = new Vector2(playerRigidbody.velocity.x, moveInput.y * climbSpeed);
      playerRigidbody.velocity = playerVelocity;
      playerAnimator.SetBool("isClimbing", MathF.Abs(playerRigidbody.velocity.y) > Mathf.Epsilon);
    }
    else
    {
      playerAnimator.SetBool("isClimbing", false);
      playerRigidbody.gravityScale = defaultGravity;
    }
  }

  private void Run()
  {
    if (!isAlive) return;
    Vector2 playerVelocity = new Vector2(moveInput.x * runSpeed, playerRigidbody.velocity.y);
    playerRigidbody.velocity = playerVelocity;
    playerAnimator.SetBool("isRunning", MathF.Abs(playerRigidbody.velocity.x) > Mathf.Epsilon);
  }

  private void FlipSprite()
  {
    if (MathF.Abs(playerRigidbody.velocity.x) > Mathf.Epsilon)
    {
      transform.localScale = new Vector2(Mathf.Sign(playerRigidbody.velocity.x), 1f);
    }
  }

  void OnMove(InputValue value)
  {
    if (!isAlive) return;
    moveInput = value.Get<Vector2>();
  }

  void OnJump(InputValue value)
  {
    if (!isAlive) return;
    if (!playerFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))) return;
    if (value.isPressed)
    {
      playerRigidbody.velocity += new Vector2(0, jumpSpeed);
    }
  }

  void OnFire(InputValue value)
  {
    if (!isAlive) return;
    Instantiate(bullet, gun.position, transform.rotation);
  }

  private void Die()
  {
    if (playerBodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemy", "Hazard")))
    {
      isAlive = false;
      playerAnimator.SetTrigger("Dying");
      playerRigidbody.velocity = deathFling;
      Invoke("ReloadScene", respawnDelay);
    }
  }

  private void ReloadScene()
  {
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
  }
}
