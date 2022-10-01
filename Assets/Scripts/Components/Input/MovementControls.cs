using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MovementControls : MonoBehaviour {

  private Rigidbody2D rb;

  [field: SerializeField]
  public Vector2 SpeedMultiplier { get; set; } = new Vector2(2.0f, 2.0f);
  [field: SerializeField]
  public JumpSpecification[] JumpSpecifications { get; set; } = new JumpSpecification[] {
    new JumpSpecification(2.5f, 0.5f),
    new JumpSpecification(2.5f, 0.5f)
  };

  public void Start() {
    this.rb = this.GetComponent<Rigidbody2D>();
  }

  public void Update() {
    float verticalMovement = Input.GetAxis("Vertical");
    float horizontalMovement = Input.GetAxis("Horizontal");

    this.rb.AddForce(
      new Vector2(
        horizontalMovement * this.SpeedMultiplier.x,
        verticalMovement * this.SpeedMultiplier.y
      )
    );
  }
}

