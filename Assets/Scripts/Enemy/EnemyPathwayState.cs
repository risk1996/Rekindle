using System;
using UnityEngine;

[Serializable]
public class EnemyPathwayState {

  public Vector2 Position = Vector2.zero;
  public Vector2 Velocity = Vector2.zero;
  public Int32 NextPointIndex = 0;
}

