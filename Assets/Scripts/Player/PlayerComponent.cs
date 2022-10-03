using System;
using UnityEngine;
using UnityEngine.Assertions;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerComponent : MonoBehaviour, IPlayer, IBeater {

  [field: SerializeField]
  public float NewLightDuration { get; set; } = 10f;

  public PlayerState State { get; set; }

  private ExpositionManager exposition;
  private Rigidbody2D rb;
  private TorchComponent torch;

  public void Awake() {
    this.exposition = GameManager.FindObjectOfType<ExpositionManager>();
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

    if (this.State.LightDuration <= 0) {
      this.exposition.Dispatch(ExpositionType.Dialog, "TorchFirstTimeExtinguished", true);
    }

    if (this.torch != null && Input.GetKey(this.torch.InteractKey)) {
      if (this.torch.TakeLight()) {
        this.State.RekindleCount += 1;
        this.State.LightDuration = this.NewLightDuration;
        this.torch = null;

        if (this.State.RekindleCount == 2)
          this.exposition.Dispatch(ExpositionType.Dialog, "TorchSecondTimeRekindle", true);
      }
    }
  }

  public void OnTriggerEnter2D(Collider2D collision) {
    if (collision.gameObject.tag == "Torch") {
      this.torch = collision.GetComponent<TorchComponent>();
      this.exposition.Dispatch(ExpositionType.Dialog, "TorchApproach", true);
    } else if (collision.gameObject.tag == "Enemy")
      this.exposition.Dispatch(ExpositionType.Dialog, "MonsterEncounter", true);
  }

  public void OnTriggerExit2D(Collider2D collision) {
    if (collision.gameObject.tag == "Torch") this.torch = null;
  }

  public void Beat() {
    this.State.HeartbeatCount -= 1;
  }

  public UInt32 BeatRemainder { get { return this.State.HeartbeatCount; } }
}
