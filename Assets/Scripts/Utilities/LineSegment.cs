using UnityEngine;

public class LineSegment {
  public Vector2 Start { get; set; }
  public Vector2 End { get; set; }

  public LineSegment(Vector2 start, Vector2 end) {
    this.Start = start;
    this.End = end;
  }

  public Line Line {
    get { return Line.FromPoints(this.Start, this.End); }
  }

  public float Length {
    get { return Vector2.Distance(this.Start, this.End); }
  }
}
