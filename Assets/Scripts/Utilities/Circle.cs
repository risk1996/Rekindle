using UnityEngine;

public class Circle {
  public Vector2 Origin { get; set; }
  public float Radius { get; set; }

  public Circle(Vector2 origin, float radius) {
    this.Origin = origin;
    this.Radius = radius;
  }
}

