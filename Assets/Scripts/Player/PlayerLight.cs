using System;
using UnityEngine;
using UnityEngine.Assertions;

[RequireComponent(typeof(Renderer))]
public class PlayerLight : MonoBehaviour {

  [field: SerializeField]
  public float FireSpriteExtinguishThreshold { get; set; } = 0.5f;

  [field: SerializeField]
  public GameObject LightCirle { get; set; }

  private Renderer fireSpriteRenderer;

  public void Awake() {
    Assert.IsNotNull(this.LightCirle);
    this.fireSpriteRenderer = this.GetComponent<Renderer>();
  }

  public void SetLight(float to) {
    this.fireSpriteRenderer.enabled = to >= this.FireSpriteExtinguishThreshold;
    this.LightCirle.transform.localScale = new Vector3(to, to, 0);
  }
}
