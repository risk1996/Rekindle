using System;
using UnityEngine;
using UnityEngine.Assertions;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerComponent : MonoBehaviour, IPlayer, IBeater {

  [field: SerializeField]
  public float NewLightDuration { get; set; } = 10f;

  public PlayerState State { get; set; }

  private Rigidbody2D rb;
  private TorchComponent torch;

  public void Awake() {
    this.rb = this.GetComponent<Rigidbody2D>();
  }

  public void Start() {
    if (this.State == null) this.State = new PlayerState();

    this.rb.position = this.State.Position;
    this.rb.velocity = this.State.Velocity;
  }

  public void Update() {
    this.State.Position = this.rb.position;
    this.State.Velocity = this.rb.velocity;
    this.State.LightDuration = Mathf.Max(this.State.LightDuration - Time.deltaTime, 0);
    this.BroadcastMessage("SetLight", this.State.LightDuration);

    if (this.torch != null && Input.GetKey(this.torch.InteractKey)) {
      if (this.torch.TakeLight()) {
        this.State.LightDuration = this.NewLightDuration;
        this.torch = null;
      }
    }
  }

  public void OnTriggerEnter2D(Collider2D collision) {
    if (collision.gameObject.tag == "Torch")
      this.torch = collision.GetComponent<TorchComponent>();
    else if (collision.gameObject.tag == "Enemy")
      this.SendMessage("Say", "Ouch?");
  }

  public void OnTriggerExit2D(Collider2D collision) {
    if (collision.gameObject.tag == "Torch") this.torch = null;
  }

  public void Beat() {
    this.State.HeartbeatCount -= 1;
  }

  public UInt32 BeatRemainder { get { return this.State.HeartbeatCount; } }
}
