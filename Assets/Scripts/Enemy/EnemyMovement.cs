using UnityEngine;
using UnityEngine.Assertions;

[RequireComponent(typeof(LineRenderer))]
public class EnemyMovement : MonoBehaviour {

  public EnemyPathwayState State { get; set; }

  [field: SerializeField]
  public float Speed { get; set; } = 1;

  [field: SerializeField]
  public Rigidbody2D MovingBody;

  private LineRenderer line;

  public void Awake() {
    Assert.IsNotNull(this.MovingBody);
    this.line = this.GetComponent<LineRenderer>();
  }

  public void Start() {
    this.State = new EnemyPathwayState();
  }

  public void Update() {
    Vector3 nextPoint = this.line.transform.position
      + this.line.GetPosition(this.State.NextPointIndex);
    Vector2 position = this.MovingBody.transform.position;
    Vector2 delta = (Vector2) nextPoint - position;
    this.MovingBody.velocity = this.Speed * delta.normalized;

    if (delta.magnitude < 0.01f * this.Speed) {
      this.State.NextPointIndex += 1;
      this.State.NextPointIndex %= this.line.positionCount;

      return;
    }
  }
}
