using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameState {

  public Int32 Day = 1;
  public PlayerState Player = new PlayerState();
  public List<TorchState> Torches = new List<TorchState>();
}
