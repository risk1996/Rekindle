using UnityEngine;
using UnityEngine.Assertions;

public class EnemyMovement : MonoBehaviour {

  public EnemyPathwayState State { get; set; }

  [field: SerializeField]
  public float Speed { get; set; } = 1;

  [field: SerializeField]
  public float AvoidLightSpeed { get; set; } = 0;

  [field: SerializeField]
  public float AvoidLightDuration { get; set; } = 2;

  [field: SerializeField]
  public LineRenderer Line;

  private Rigidbody2D rb;

  public void Awake() {
    Assert.IsNotNull(this.Line);
    this.rb = this.GetComponent<Rigidbody2D>();
  }

  public void Start() {
    this.State = new EnemyPathwayState();
  }

  public void Update() {
    if (this.State.AvoidDuration > 0) {
      this.State.AvoidDuration -= Time.deltaTime;
      this.rb.velocity = this.State.AvoidVelocity;

      return;
    }

    Vector3 nextPoint = this.Line.transform.position
      + this.Line.GetPosition(this.State.NextPointIndex);
    Vector2 position = this.rb.transform.position;
    Vector2 delta = (Vector2) nextPoint - position;
    this.rb.velocity = this.Speed * delta.normalized;

    if (delta.magnitude < 0.01f * this.Speed) {
      this.State.NextPointIndex += 1;
      this.State.NextPointIndex %= this.Line.positionCount;
    }
  }

  public void OnTriggerEnter2D(Collider2D collision) {
    if (this.ShouldAvoidLight() && collision.gameObject.tag == "Light") {
      HorizontalDirection direction =
        collision.gameObject.transform.position.x < this.transform.position.x
          ? HorizontalDirection.Left
          : HorizontalDirection.Right;

      this.State.AvoidDuration = this.AvoidLightDuration;
      this.State.AvoidVelocity = this.AvoidLightSpeed * (
        direction == HorizontalDirection.Left ? Vector2.right : Vector2.left
      );
    }
  }

  private bool ShouldAvoidLight() {
    return this.AvoidLightSpeed > 0;
  }
}
