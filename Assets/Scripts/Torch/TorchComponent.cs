using System;
using UnityEngine;
using UnityEngine.Assertions;

public class TorchComponent : MonoBehaviour {

  [field: SerializeField]
  public TorchState State { get; set; } = new TorchState();

  [field: SerializeField]
  public KeyCode InteractKey = KeyCode.F;

  [field: SerializeField]
  private SpriteMask LightSpriteMask { get; set; }


  private Renderer renderer;

  public void Start() {
    Assert.IsNotNull(this.State);
    Assert.IsNotNull(this.LightSpriteMask);
    this.renderer = this.GetComponent<Renderer>();
  }

  public void Update() {
    float ratio = this.State.UsageRatio;
    Color color = this.renderer.material.color;
    color.a = ratio;
    this.renderer.material.color = color;
    float lightSourceSize = ratio * this.State.MaxLightSourceRadius;
    this.LightSpriteMask.transform.localScale = new Vector3(lightSourceSize, lightSourceSize, 0);
  }

  public bool TakeLight() {
    if (this.State.RemainingUsage > 0) {
      this.State.RemainingUsage -= 1;
      return true;
    } else return false;
  }
}
