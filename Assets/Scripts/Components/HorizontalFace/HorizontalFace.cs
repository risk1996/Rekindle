using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class HorizontalFace : MonoBehaviour {

  [field: SerializeField]
  public HorizontalDirection DefaultDirection = HorizontalDirection.Right;

  [field: SerializeField]
  public float Threshold = 0.1f;

  [field: SerializeField]
  public List<SpriteRenderer> Renderers = new List<SpriteRenderer>();

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

    foreach (SpriteRenderer r in this.Renderers) r.flipX = isReversed;
  }
}

