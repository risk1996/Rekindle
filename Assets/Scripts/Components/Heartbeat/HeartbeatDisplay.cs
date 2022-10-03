using TMPro;
using UnityEngine;
using UnityEngine.Assertions;

[RequireComponent(typeof(TextMeshProUGUI))]
public class HeartbeatDisplay : MonoBehaviour {

  private TextMeshProUGUI display;
  private IBeater beater;

  [field: SerializeField]
  public GameObject Beater { get; set; }

  public void Awake() {
    this.display = this.GetComponent<TextMeshProUGUI>();
    Assert.IsNotNull(this.Beater);
    this.beater = this.Beater.GetComponent<IBeater>();
  }

  public void Update() {
    this.display.text = this.beater.BeatRemainder.ToString();
  }
}

