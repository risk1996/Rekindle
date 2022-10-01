using System;
using Unity.Mathematics;
using UnityEngine;

[RequireComponent(typeof(Transform))]
public class Threat : MonoBehaviour {

  [field: SerializeField]
  public UInt32 MaxBPMIncrease { get; set; } = 50;

  [field: SerializeField]
  public float OutsideIlluminationThreshold = 0.3f;
  public float insideIlluminationThreashold {
    get { return 1 - this.OutsideIlluminationThreshold; }
  }

  private Transform tf;

  public void Awake() {
    this.tf = this.GetComponent<Transform>();
  }

  public float CalculateBPMIncrease(IThreatTarget target) {
    Vector2 threatPosition = new Vector2(
      this.tf.position.x,
      this.tf.position.y
    );

    LineSegment threatToVision = new LineSegment(threatPosition, target.VisionCircle.Origin);

    if (threatToVision.Length >= target.VisionCircle.Radius) return 0f;

    LineSegment threatToLight = new LineSegment(threatPosition, target.LightCircle.Origin);
    float visionAngle = Mathf.Atan(
      (target.VisionToLightOriginLine.Line.Gradient - threatToVision.Line.Gradient)
        / (1 + target.VisionToLightOriginLine.Line.Gradient * threatToVision.Line.Gradient)
    );
    float illuminationAngle = (float) Math.Asin(target.VisionToLightOriginLine.Length / threatToLight.Length * Math.Sin(visionAngle));
    float lightAngle = (float) Math.PI - visionAngle - illuminationAngle;
    float visionToIlluminationDistance = (float) (target.VisionCircle.Radius * Math.Sin(lightAngle) / Math.Sin(visionAngle));

    if (threatToVision.Length <= visionToIlluminationDistance)
      return this.MaxBPMIncrease * (
        this.OutsideIlluminationThreshold +
        ((visionToIlluminationDistance - threatToVision.Length) / visionToIlluminationDistance) * this.insideIlluminationThreashold
      );
    else
      return this.MaxBPMIncrease * this.OutsideIlluminationThreshold * (target.VisionCircle.Radius - threatToVision.Length) / target.VisionCircle.Radius;
  }
}

