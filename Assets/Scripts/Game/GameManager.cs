using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(PersistenceService<GameState>))]
public class GameManager : MonoBehaviour {

  private GameState _state;
  public GameState State { get { return this._state; } }

  private PersistenceService<GameState> persistence;

  [field: SerializeField]
  public PlayerComponent Player;

  [field: SerializeField]
  public PersistenceSettings PersistenceSettings { get; set; } = new PersistenceSettings();

  private List<TorchComponent> torches;

  public void Awake() {
    Assert.IsNotNull(this.Player);
    this.persistence = new PersistenceService<GameState>();

    this._state =
      this.PersistenceSettings.Load ? this.persistence.Load() : new GameState();
    Assert.IsNotNull(this._state);
    this.Player.State = this._state.Player;

    this.torches = new List<TorchComponent>(GameObject.FindObjectsOfType<TorchComponent>());
    foreach ((TorchComponent torch, int index) in this.torches.Select((torch, index) => (torch, index))) {
      if (this._state.Torches.Count <= index) this._state.Torches.Add(torch.State);
      torch.State = this._state.Torches[index];
    }
  }

  public void OnApplicationQuit() {
    if (this.PersistenceSettings.Save) {
      Assert.IsNotNull(this._state);
      if (PersistenceSettings.Save) {
        this.persistence.Save(this._state);
      }
    }
  }

  public void GameOver() {
    AudioController.AudioCustomPlay(AudioName.Fall, 4f, customEndTime: 5.5f);
    Player.transform.position = new Vector3(Player.Origin.x, Player.Origin.y, Player.Origin.z);

    this._state.Day += 1;
    Player.ResetState();
    foreach (TorchComponent torch in this.torches) torch.ResetState();
  }
}
