using System;
using UnityEngine;
using UnityEngine.Assertions;

public class TorchComponent : MonoBehaviour {

  public TorchState State { get; set; }

  [field: SerializeField]
  public Int32 MaxUsage { get; set; } = 3;

  [field: SerializeField]
  public float MaxLightSourceRadius { get; set; } = 3f;

  [field: SerializeField]
  public KeyCode InteractKey = KeyCode.F;

  [field: SerializeField]
  private SpriteMask LightSpriteMask { get; set; }

  private Renderer renderer;

  public void Start() {
    if (this.State == null) this.State = new TorchState();
    this.State.RemainingUsage = Math.Min(this.State.RemainingUsage, this.MaxUsage);

    Assert.IsNotNull(this.LightSpriteMask);
    this.renderer = this.GetComponent<Renderer>();
  }

  public void Update() {
    float ratio = 1f * this.State.RemainingUsage / this.MaxUsage;
    Color color = this.renderer.material.color;
    color.a = ratio;
    this.renderer.material.color = color;
    float lightSourceSize = ratio * this.MaxLightSourceRadius;
    this.LightSpriteMask.transform.localScale = new Vector3(lightSourceSize, lightSourceSize, 0);
  }

  public bool TakeLight() {
    if (this.State.RemainingUsage > 0) {
      this.State.RemainingUsage -= 1;
      return true;
    } else return false;
  }
}
