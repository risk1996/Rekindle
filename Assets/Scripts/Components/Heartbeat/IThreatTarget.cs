using System;
using System.Collections.Generic;
using UnityEngine;

public interface IThreatTarget {

  public Circle VisionCircle { get; }

  public Circle LightCircle { get; }

  public List<Threat> Threats { get; }

  public LineSegment VisionToLightOriginLine {
    get { return new LineSegment(this.VisionCircle.Origin, this.LightCircle.Origin); }
  }

  public float VisionToLightDistance {
    get { return Vector2.Distance(this.VisionCircle.Origin, this.LightCircle.Origin); }
  }
}
