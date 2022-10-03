using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class GameOverTrigger : MonoBehaviour {

  private GameManager gameManager;
  private Rigidbody2D rb;

  public void Awake() {
    this.gameManager = GameObject.FindObjectOfType<GameManager>();
    this.rb = this.GetComponent<Rigidbody2D>();
  }

  public void OnCollisionEnter2D(Collision2D collision) {
    if (collision.gameObject.tag == "Player") {
      this.gameManager.GameOver();
    }
  }

}
