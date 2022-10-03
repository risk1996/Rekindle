using UnityEngine;
using UnityEngine.Assertions;

public class ExpositionManager : MonoBehaviour {

  public ExpositionState State { get; set; }

  [field: SerializeField]
  public GameObject PlayerObject { get; set; }

  public void Awake() {
    if (this.State == null) this.State = new ExpositionState();
    Assert.IsNotNull(this.PlayerObject);
  }
}