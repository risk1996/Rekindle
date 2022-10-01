using UnityEngine;
using UnityEngine.Assertions;

[RequireComponent(typeof(PersistenceService<GameState>))]
public class GameManager : MonoBehaviour {

  private GameState _state;
  public GameState State { get { return this._state; } }

  private PersistenceService<GameState> persistence;

  [field: SerializeField]
  public GameObject PlayerObject;

  public void Awake() {
    this.PlayerObject = GameObject.FindGameObjectWithTag("Player");
    Assert.IsNotNull(this.PlayerObject);
  }

  public void Start() {
    this.persistence = new PersistenceService<GameState>();
    this._state = this.persistence.Load();
    Assert.IsNotNull(this._state);
    this.PlayerObject.GetComponent<IPlayer>().State = this._state.Player;
  }

  public void Update() {
    Assert.IsNotNull(this._state);
    this.persistence.Save(this._state);
  }
}
