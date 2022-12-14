using TMPro;
using UnityEngine;
using UnityEngine.Assertions;

[RequireComponent(typeof(TextMeshProUGUI))]
public class HeartbeatDisplay : MonoBehaviour {

  private TextMeshProUGUI display;
  private IBeater beater;

  [field: SerializeField]
  public GameObject Beater { get; set; }

  [field: SerializeField]
  public HeartbeatCounter HeartbeatCounter;

  [field: SerializeField]
  public TextMeshProUGUI BPMText;

  public void Awake() {
    this.display = this.GetComponent<TextMeshProUGUI>();
    Assert.IsNotNull(this.Beater);
    Assert.IsNotNull(this.HeartbeatCounter);
    Assert.IsNotNull(this.BPMText);
    this.beater = this.Beater.GetComponent<IBeater>();
  }

  public void Update() {
    this.display.text = this.beater.BeatRemainder.ToString();
    this.BPMText.text = ((int) this.HeartbeatCounter.CurrentBPM).ToString() + " BPM";
  }
}
