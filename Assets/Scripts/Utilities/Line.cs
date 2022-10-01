using UnityEngine;

public class Line {

  public float Gradient { get; set; }
  public float YIntercept { get; set; }

  public Line(float gradient, float yIntercept) {
    this.Gradient = gradient;
    this.YIntercept = yIntercept;
  }

  public static Line FromPoints(Vector2 a, Vector2 b) {
    float gradient = (b.y - a.y) / (b.x - a.x);
    float yIntercept = gradient * a.x - a.y;

    return new Line(gradient, yIntercept);
  }
}

