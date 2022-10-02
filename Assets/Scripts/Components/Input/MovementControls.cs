using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
public class MovementControls : MonoBehaviour {

  private Animator animator;
  private Rigidbody2D rb;

  [field: SerializeField]
  public JumpSpecification JumpSpecification { get; set; } =
    new JumpSpecification(3f, 2, 18, 4);

  private MovementState state;
  private float jumpCountdown; // NOTE: < 0 not jumping
  private bool jumpForceApplied;

  [field: SerializeField]
  public Vector2 Speed { get; set; } = new Vector2(2.0f, 9.5f);

  public void Start() {
    this.animator = this.GetComponent<Animator>();
    this.rb = this.GetComponent<Rigidbody2D>();
    this.TransitionTo(MovementState.Idle);
  }

  public void Update() {
    if (this.state == MovementState.Bound) return;

    float horizontalMovement = Input.GetAxisRaw("Horizontal");
    Vector2 velocity = new Vector2(
      Mathf.Clamp(this.rb.velocity.x / this.Speed.x + horizontalMovement, -1, 1) * this.Speed.x,
      this.rb.velocity.y
    );

    bool isJumping = this.jumpCountdown >= 0;
    bool willJump = Input.GetKeyDown(KeyCode.Space);

    if (!isJumping && willJump) this.TransitionTo(MovementState.Jumping);

    else if (isJumping) {
      if (
        !this.jumpForceApplied &&
        this.jumpCountdown > this.JumpSpecification.LandingStartTime &&
        this.jumpCountdown <= this.JumpSpecification.AirborneStartTime
      ) {
        this.jumpForceApplied = true;
        velocity.y = this.Speed.y;
      } else if (
        this.jumpCountdown <= this.JumpSpecification.LandingStartTime ||
        this.jumpCountdown > this.JumpSpecification.AirborneStartTime
      ) velocity.x = 0;

      this.jumpCountdown -= Time.deltaTime;

      if (this.jumpCountdown < 0) this.TransitionTo(MovementState.Idle);
    } else if (horizontalMovement != 0) this.TransitionTo(MovementState.Running);
    else this.TransitionTo(MovementState.Idle);

    this.rb.velocity = velocity;
  }

  private static readonly HashSet<(MovementState, MovementState)> stateTransitions =
    new HashSet<(MovementState, MovementState)> {
      (MovementState.Idle, MovementState.Bound),
      (MovementState.Idle, MovementState.Running),
      (MovementState.Idle, MovementState.Jumping),
      (MovementState.Bound, MovementState.Idle), // TODO Change to Running?
      (MovementState.Running, MovementState.Idle),
      (MovementState.Running, MovementState.Bound),
      (MovementState.Running, MovementState.Jumping),
      (MovementState.Jumping, MovementState.Idle),
      (MovementState.Jumping, MovementState.Bound),
      (MovementState.Jumping, MovementState.Running),
    };

  public void TransitionTo(MovementState state) {
    if (!MovementControls.stateTransitions.Contains((this.state, state)))
      return;

    this.state = state;
    this.animator.ResetTrigger("Idle");
    this.animator.ResetTrigger("Bound");
    this.animator.ResetTrigger("Running");
    this.animator.ResetTrigger("Jumping");

    switch (state) {
      case MovementState.Idle: {
          this.animator.SetTrigger("Idle");
          this.jumpCountdown = -float.Epsilon;
          this.jumpForceApplied = false;
          break;
        }
      case MovementState.Bound: {
          this.animator.SetTrigger("Bound");
          this.jumpCountdown = -float.Epsilon;
          this.jumpForceApplied = false;
          break;
        }
      case MovementState.Running: {
          this.animator.SetTrigger("Running");
          this.jumpCountdown = -float.Epsilon;
          this.jumpForceApplied = false;
          break;
        }
      case MovementState.Jumping: {
          this.animator.SetTrigger("Jumping");
          this.jumpCountdown = this.JumpSpecification.Duration;
          this.jumpForceApplied = false;
          break;
        }
    }
  }
}

