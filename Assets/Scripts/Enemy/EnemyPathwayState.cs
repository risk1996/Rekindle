using System;
using UnityEngine;

[Serializable]
public class EnemyPathwayState {

  public Vector2 Position = Vector2.zero;
  public Vector2 Velocity = Vector2.zero;
  public Int32 NextPointIndex = 0;

  public float AvoidDuration = 0;
  public Vector2 AvoidVelocity = Vector2.zero;
}

