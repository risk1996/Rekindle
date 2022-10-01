using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;

public class HeartbeatCounter : MonoBehaviour {

  [field: SerializeField]
  public float BaseBPM = 70f;

  [field: SerializeField]
  public float MaxBPM = 210f;

  [field: SerializeField]
  public GameObject Beater { get; set; }

  [field: SerializeField]
  public GameObject Vision { get; set; }

  private IBeater beater;
  private IThreatTarget target;
  private float currentBPM;
  private float beatPeriod { get { return 60.0f / this.currentBPM; } }
  private float elapsedTime = 0f;

  public void Awake() {
    Assert.IsNotNull(this.Beater);
    this.beater = this.Beater.GetComponent<IBeater>();
    Assert.IsNotNull(this.Vision);
    this.target = this.Vision.GetComponent<IThreatTarget>();
  }

  public void Start() {
    this.currentBPM = this.BaseBPM;
  }

  public void Update() {
    this.elapsedTime += Time.deltaTime;

    List<Threat> threats = this.target.Threats;
    this.currentBPM = Mathf.Min(
      this.BaseBPM + threats.Aggregate(0f,
        (acc, threat) => acc + threat.CalculateBPMIncrease(this.target)
      ),
      this.MaxBPM
    );

    Debug.Log(this.currentBPM);
    Debug.Log(this.beatPeriod);

    if (this.elapsedTime >= this.beatPeriod) {
      this.elapsedTime -= this.beatPeriod;
      this.beater.Beat();
    }
  }
}

