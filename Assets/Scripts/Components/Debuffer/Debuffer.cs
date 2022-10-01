using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Debuffer : MonoBehaviour {

  [field: SerializeField]
  public float Duration { get; set; } = 2.0f;

  private GameObject target;
  private float duration;

  public void OnTriggerEnter2D(Collider2D collision) {
    if (collision.gameObject.tag == "Player") {
      this.target = collision.gameObject;
      this.duration = 0;
    }
  }

  public void OnTriggerExit2D(Collider2D collision) {
    this.target = null;
    this.duration = 0;
  }

  public void Update() {
    if (this.target != null) {
      this.duration += Time.deltaTime;
      if (this.duration >= this.Duration) this.target = null;
      else
        this.target.transform.position = this.transform.position;
    }
  }
}
