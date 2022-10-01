using System.Collections;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

[RequireComponent(typeof(TMPro.TextMeshProUGUI))]
public class HeartbeatDisplay : MonoBehaviour {

  private TMPro.TextMeshProUGUI display;
  private IBeater beater;

  [field: SerializeField]
  public GameObject Beater { get; set; }

  public void Awake() {
    this.display = this.GetComponent<TMPro.TextMeshProUGUI>();
    Assert.IsNotNull(this.Beater);
    this.beater = this.Beater.GetComponent<IBeater>();
  }

  public void Update() {
    this.display.text = this.beater.BeatRemainder.ToString();
  }
}

