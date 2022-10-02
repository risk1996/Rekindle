using System;
using UnityEngine;
using UnityEngine.Assertions;


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

  public void Update() {
    this.SendMessageUpwards("DimLight", Time.deltaTime);

    Vector3 scale = this.LightCirle.transform.localScale;
    float newScale = Mathf.Max(scale.x - Time.deltaTime, 0);
    this.LightCirle.transform.localScale = new Vector3(newScale, newScale, 0);

    this.fireSpriteRenderer.enabled = newScale >= this.FireSpriteExtinguishThreshold;
  }

  public void Rekindle(float to) {
    this.LightCirle.transform.localScale = new Vector3(to, to, 0);
  }
}
