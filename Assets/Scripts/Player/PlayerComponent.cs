using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerComponent : MonoBehaviour, IPlayer, IBeater {

  private Rigidbody2D rb;

  public PlayerState State { get; set; }

  public void Awake() {
    this.rb = this.GetComponent<Rigidbody2D>();
  }

  public void Start() {
    this.rb.position = this.State.Position;
    this.rb.velocity = this.State.Velocity;
    this.BroadcastMessage("Rekindle", this.State.LightDuration);
  }

  public void Update() {
    this.State.Position = this.rb.position;
    this.State.Velocity = this.rb.velocity;
  }

  public void DimLight(float by) {
    this.State.LightDuration -= by;
  }

  public void Beat() {
    this.State.HeartbeatCount -= 1;
  }

  public UInt32 BeatRemainder { get { return this.State.HeartbeatCount; } }
}
