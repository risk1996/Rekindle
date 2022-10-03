using System;
using UnityEngine;
using UnityEngine.Assertions;

[RequireComponent(typeof(SpriteRenderer))]
public class PlayerLight : MonoBehaviour {

  [field: SerializeField]
  public float FireSpriteExtinguishThreshold { get; set; } = 0.5f;

  [field: SerializeField]
  public GameObject LightCirle { get; set; }

  private Vector3 originalLightScale;
  private Vector2 originalLightTransform;
  private SpriteRenderer fireSpriteRenderer;

  public void Awake() {
    Assert.IsNotNull(this.LightCirle);
    this.fireSpriteRenderer = this.GetComponent<SpriteRenderer>();
  }

  public void Start() {
    this.originalLightScale = this.LightCirle.transform.localScale;
    this.originalLightTransform = this.LightCirle.transform.localPosition;
  }

  public void Update() {
    this.LightCirle.transform.localPosition =
      this.originalLightTransform * new Vector2(this.fireSpriteRenderer.flipX ? -1 : 1, 1);
  }

  public void SetLight(float to) {
    this.fireSpriteRenderer.enabled = to >= this.FireSpriteExtinguishThreshold;
    this.LightCirle.transform.localScale = new Vector3(originalLightScale.x * to / 10, originalLightScale.y * to / 10, 0);
  }
}
