using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;

[RequireComponent(typeof(CircleCollider2D))]
public class PlayerVision : MonoBehaviour, IThreatTarget {

  [field: SerializeField]
  public GameObject Light { get; set; }

  private CircleCollider2D visionCollider;
  private CircleCollider2D lightCollider;
  private List<GameObject> monstersInSight = new List<GameObject>();

  public void Awake() {
    Assert.IsNotNull(this.Light);
    this.visionCollider = this.GetComponent<CircleCollider2D>();
    this.lightCollider = this.Light.GetComponent<CircleCollider2D>();
  }

  public void OnTriggerEnter2D(Collider2D collider) {
    if (collider.gameObject.tag == "Enemy")
      this.monstersInSight.Add(collider.gameObject);
  }

  public void OnTriggerExit2D(Collider2D collider) {
    if (collider.gameObject.tag == "Enemy")
      this.monstersInSight.Remove(collider.gameObject);
  }

  public List<Threat> Threats {
    get { return this.monstersInSight.ConvertAll((monster) => monster.GetComponent<Threat>()); }
  }

  public Circle VisionCircle {
    get {
      return new Circle(
        this.visionCollider.transform.position,
        this.visionCollider.transform.localScale.x * this.visionCollider.radius
      );
    }
  }

  public Circle LightCircle {
    get {
      return new Circle(
        this.lightCollider.transform.position,
        this.lightCollider.transform.localScale.x * this.lightCollider.radius
      );
    }
  }
}
