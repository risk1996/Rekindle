using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MovementControls : MonoBehaviour {

  private Rigidbody2D rb;

  public Animator PlayerAnimator;
  public Animator BodyAnimator;
  public Animator FireAnimator;

  private float jumpAnimationDurationCounter;
  private float jumpDurationCounter;

  [field: SerializeField]
  public Vector2 SpeedMultiplier { get; set; } = new Vector2(2.0f, 2.0f);

  [field: SerializeField]
  public float JumpAnimationDuration { get; set; } = 0.8f;

  [field: SerializeField]
  public float MaxVelocity { get; set; } = 10f;

  public void Start() {
    this.rb = this.GetComponent<Rigidbody2D>();
    jumpAnimationDurationCounter = JumpAnimationDuration;
    jumpDurationCounter = JumpAnimationDuration / 3;
  }

  public void Update() {
    float horizontalMovement = Input.GetAxisRaw("Horizontal");
    this.rb.velocity = new Vector2(horizontalMovement * this.SpeedMultiplier.x, 0f);


    if (!PlayerAnimator.GetBool("isJumping") && Input.GetKeyDown(KeyCode.Space)) {
      PlayerAnimator.SetTrigger("isJumping");
      BodyAnimator.SetTrigger("isJumping");
      jumpAnimationDurationCounter = JumpAnimationDuration;
      jumpDurationCounter = JumpAnimationDuration / 3;
    }

    if (PlayerAnimator.GetBool("isJumping")) {
      if (jumpAnimationDurationCounter > 0) {
        if (jumpDurationCounter > 0) {
          this.rb.velocity = new Vector2(this.rb.velocity.x, this.SpeedMultiplier.y * (JumpAnimationDuration * 2 / 3));
        } else if (jumpDurationCounter < 0) {
          this.rb.velocity = new Vector2(this.rb.velocity.x, -this.SpeedMultiplier.y * (JumpAnimationDuration * 1 / 3));
        } else {
          this.rb.velocity = new Vector2(this.rb.velocity.x, 0);
        }

        jumpDurationCounter -= Time.deltaTime;
        jumpAnimationDurationCounter -= Time.deltaTime;
      }
      else {
        PlayerAnimator.SetBool("isJumping", false);
        BodyAnimator.SetBool("isJumping", false);
      }
    }
  }
}

