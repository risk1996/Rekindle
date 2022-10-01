using System;
using UnityEngine;

[Serializable]
public struct JumpSpecification {

  [field: SerializeField]
  public float Height { get; set; }
  [field: SerializeField]
  public float Duration { get; set; }

  public JumpSpecification(float height, float duration) {
    this.Height = height;
    this.Duration = duration;
  }
}
