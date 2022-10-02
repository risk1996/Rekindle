using UnityEngine;
using UnityEngine.Assertions;

[RequireComponent(typeof(PersistenceService<GameState>))]
public class GameManager : MonoBehaviour {

  private GameState _state;
  public GameState State { get { return this._state; } }

  private PersistenceService<GameState> persistence;

  [field: SerializeField]
  public PlayerComponent Player;

  public void Awake() {
    Assert.IsNotNull(this.Player);
  }

  public void Start() {
    this.persistence = new PersistenceService<GameState>();
    this._state = this.persistence.Load();
    Assert.IsNotNull(this._state);
    this.Player.State = this._state.Player;
  }

  public void Update() {
    Assert.IsNotNull(this._state);
    this.persistence.Save(this._state);
  }
}
