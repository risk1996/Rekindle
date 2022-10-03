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

  private MovementStep state;
  private float jumpCountdown; // NOTE: < 0 not jumping
  private bool jumpForceApplied;

  [field: SerializeField]
  public Vector2 Speed { get; set; } = new Vector2(2.0f, 9.5f);

  public void Start() {
    this.animator = this.GetComponent<Animator>();
    this.rb = this.GetComponent<Rigidbody2D>();
    this.TransitionTo(MovementStep.Idle);
  }

  public void Update() {
    if (this.state == MovementStep.Bound) return;

    float horizontalMovement = Input.GetAxisRaw("Horizontal");
    Vector2 velocity = new Vector2(
      Mathf.Clamp(this.rb.velocity.x / this.Speed.x + horizontalMovement, -1, 1) * this.Speed.x,
      this.rb.velocity.y
    );

    bool isJumping = this.jumpCountdown >= 0;
    bool willJump = Input.GetKeyDown(KeyCode.Space);

    if (!isJumping && willJump) this.TransitionTo(MovementStep.Jumping);

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

      if (this.jumpCountdown < 0) this.TransitionTo(MovementStep.Idle);
    } else if (horizontalMovement != 0) this.TransitionTo(MovementStep.Running);
    else this.TransitionTo(MovementStep.Idle);

    this.rb.velocity = velocity;

    if (Input.GetAxisRaw("Horizontal") == 0) {
      this.rb.velocity = new Vector3(0f, this.rb.velocity.y);
    }
  }

  private static readonly HashSet<(MovementStep, MovementStep)> stateTransitions =
    new HashSet<(MovementStep, MovementStep)> {
      (MovementStep.Idle, MovementStep.Bound),
      (MovementStep.Idle, MovementStep.Running),
      (MovementStep.Idle, MovementStep.Jumping),
      (MovementStep.Bound, MovementStep.Idle), // TODO Change to Running?
      (MovementStep.Running, MovementStep.Idle),
      (MovementStep.Running, MovementStep.Bound),
      (MovementStep.Running, MovementStep.Jumping),
      (MovementStep.Jumping, MovementStep.Idle),
      (MovementStep.Jumping, MovementStep.Bound),
      (MovementStep.Jumping, MovementStep.Running),
    };

  public void TransitionTo(MovementStep state) {
    if (!MovementControls.stateTransitions.Contains((this.state, state)))
      return;

    this.state = state;
    this.animator.ResetTrigger("Idle");
    this.animator.ResetTrigger("Bound");
    this.animator.ResetTrigger("Running");
    this.animator.ResetTrigger("Jumping");

    switch (state) {
      case MovementStep.Idle: {
          this.animator.SetTrigger("Idle");
          this.jumpCountdown = -float.Epsilon;
          this.jumpForceApplied = false;
          break;
        }
      case MovementStep.Bound: {
          this.animator.SetTrigger("Bound");
          this.jumpCountdown = -float.Epsilon;
          this.jumpForceApplied = false;
          break;
        }
      case MovementStep.Running: {
          this.animator.SetTrigger("Running");
          this.jumpCountdown = -float.Epsilon;
          this.jumpForceApplied = false;
          break;
        }
      case MovementStep.Jumping: {
          this.animator.SetTrigger("Jumping");
          this.jumpCountdown = this.JumpSpecification.Duration;
          this.jumpForceApplied = false;
          break;
        }
    }
  }
}

