using System;
using UnityEngine;
using UnityEngine.Assertions;

[RequireComponent(typeof(Rigidbody2D))]
public class BossMovement : MonoBehaviour {

  public BossMovementState State { get; set; }

  [field: SerializeField]
  public GameObject PlayerObject { get; set; }

  private Rigidbody2D rb;

  public void Awake() {
    if (this.State == null) this.State = new BossMovementState();
    Assert.IsNotNull(this.PlayerObject);
    this.rb = this.GetComponent<Rigidbody2D>();
  }

  public void Update() {
    this.State.Countdown -= Time.deltaTime;

    if (this.State.Countdown <= 0) {
      this.State.Countdown += 1;
      this.State.Speed += this.State.Acceleration;
    }

    this.ApproachPlayer();
  }

  private void ApproachPlayer() {
    Vector2 bossPosition = this.transform.position;
    Vector2 playerPosition = this.PlayerObject.transform.position;
    Vector2 delta = playerPosition - bossPosition;
    this.rb.velocity = this.State.Speed * delta.normalized;
  }
}

