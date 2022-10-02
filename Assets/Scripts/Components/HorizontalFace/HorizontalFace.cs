using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class HorizontalFace : MonoBehaviour {
  [SerializeField] public HorizontalDirection DefaultDirection = HorizontalDirection.Right;
  [SerializeField] public float Threshold = 0.1f;

  private Rigidbody2D rb;

  public void Awake() {
    this.rb = this.GetComponent<Rigidbody2D>();
  }

  public void Update() {
    if (Mathf.Abs(this.rb.velocity.x) < this.Threshold) return;

    HorizontalDirection currentDirection = this.rb.velocity.x < 0
      ? HorizontalDirection.Left
      : HorizontalDirection.Right;

    bool isReversed = currentDirection != this.DefaultDirection;
    Quaternion rotation = this.transform.rotation;
    rotation.y = isReversed ? 180 : 0;
    this.transform.rotation = rotation;
  }
}

