using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Debuffer : MonoBehaviour {

  [field: SerializeField]
  public float Duration { get; set; } = 1.0f;
  [field: SerializeField]
  public float Force { get; set; } = 10.0f;

  private Rigidbody2D rb;

  private GameObject target;
  private Rigidbody2D targetRb;
  private float duration;
  private HorizontalDirection collisionDirection;

  public void Awake() {
    this.rb = this.GetComponent<Rigidbody2D>();
  }

  public void OnTriggerEnter2D(Collider2D collision) {
    if (collision.gameObject.tag == "Player") {
      collision.gameObject.GetComponent<MovementControls>().enabled = false;
      this.target = collision.gameObject;
      this.target.SendMessageUpwards("TransitionTo", MovementState.Bound);
      this.targetRb = this.target.GetComponent<Rigidbody2D>();
      this.duration = 0;
      this.collisionDirection = this.rb.transform.position.x < this.targetRb.position.x
        ? HorizontalDirection.Left
        : HorizontalDirection.Right;
    }
  }

  public void OnTriggerExit2D(Collider2D collision) {
    if (collision.gameObject.tag == "Player") {
      collision.gameObject.GetComponent<MovementControls>().enabled = true;
    }
  }

  public void Update() {
    if (this.target != null) {
      this.duration += Time.deltaTime;
      if (this.duration >= this.Duration) {
        this.target.SendMessageUpwards("TransitionTo", MovementState.Idle); // TODO Change to Running?
        this.target = null;
        this.targetRb.velocity = 2 * this.Force * new Vector2(
          this.collisionDirection == HorizontalDirection.Left ? -1 : 1,
          0.5f
        );
      } else {
        Vector2 thisPosition = this.rb.transform.position;
        Vector2 targetPosition = this.targetRb.transform.position;
        Vector2 delta = thisPosition - targetPosition;

        if (delta.magnitude < 0.01f) return;

        this.targetRb.velocity = this.Force * delta;
      }
    }
  }
}
