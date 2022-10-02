using System;
using UnityEngine;
using UnityEngine.Assertions;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerComponent : MonoBehaviour, IPlayer, IBeater {

  [field: SerializeField]
  public float NewLightDuration { get; set; } = 10f;

  private Rigidbody2D rb;

  public PlayerState State { get; set; }

  private bool isTouchingLight;

  public void Awake() {
    this.rb = this.GetComponent<Rigidbody2D>();
  }

  public void Start() {
    Assert.IsNotNull(this.State);
    this.rb.position = this.State.Position;
    this.rb.velocity = this.State.Velocity;
  }

  public void Update() {
    this.State.Position = this.rb.position;
    this.State.Velocity = this.rb.velocity;
    this.State.LightDuration = Mathf.Max(this.State.LightDuration - Time.deltaTime, 0);
    this.BroadcastMessage("SetLight", this.State.LightDuration);
  }

  public void OnTriggerStay2D(Collider2D collision) {
    if (collision.gameObject.tag == "Torch")
      this.State.LightDuration = this.NewLightDuration;
  }

  public void Beat() {
    this.State.HeartbeatCount -= 1;
  }

  public UInt32 BeatRemainder { get { return this.State.HeartbeatCount; } }
}
