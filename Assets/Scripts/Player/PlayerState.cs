using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerState {

  public Vector2 Position = new Vector2();
  public Vector2 Velocity = new Vector2();
  public UInt32 HeartbeatCount = UInt32.MaxValue;
  public float LightDuration = 10f;
  public UInt32 RekindleCount = 0;
}
