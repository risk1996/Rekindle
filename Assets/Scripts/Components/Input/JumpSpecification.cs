using System;
using UnityEngine;

[Serializable]
public struct JumpSpecification {

  [field: SerializeField]
  public float Duration { get; set; }

  [field: SerializeField]
  public UInt32 LiftoffFrameCount { get; set; }

  [field: SerializeField]
  public UInt32 AirborneFrameCount { get; set; }

  [field: SerializeField]
  public UInt32 LandingFrameCount { get; set; }

  public UInt32 TotalFrameCount {
    get { return this.LiftoffFrameCount + this.AirborneFrameCount + this.LandingFrameCount; }
  }

  public float LiftoffStartTime {
    get { return this.Duration; }
  }

  public float AirborneStartTime {
    get { return this.Duration * (this.LandingFrameCount + this.AirborneFrameCount) / this.TotalFrameCount; }
  }

  public float LandingStartTime {
    get { return this.Duration * this.LandingFrameCount / this.TotalFrameCount; }
  }

  public JumpSpecification(
    float duration,
    UInt32 liftoffFrameCount,
    UInt32 airborneFrameCount,
    UInt32 landingFrameCount
  ) {
    this.Duration = duration;
    this.LiftoffFrameCount = liftoffFrameCount;
    this.AirborneFrameCount = airborneFrameCount;
    this.LandingFrameCount = landingFrameCount;
  }
}
