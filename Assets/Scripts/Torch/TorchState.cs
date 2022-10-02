using System;

[Serializable]
public class TorchState {

  public UInt32 RemainingUsage = 3;
  public UInt32 MaxUsage = 3;
  public float MaxLightSourceRadius = 3f;

  public float UsageRatio {
    get { return 1f * this.RemainingUsage / this.MaxUsage; }
  }
}

