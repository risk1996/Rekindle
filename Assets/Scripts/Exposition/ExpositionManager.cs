using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class ExpositionManager : MonoBehaviour {

  public ExpositionState State { get; set; }

  [field: SerializeField]
  public TextAsset Dialogs { get; set; }

  [field: SerializeField]
  public DialogManager DialogManager { get; set; }

  [field: SerializeField]
  public TextAsset Narrations { get; set; }

  private DialogStructure dialogs;
  private Dictionary<string, string[]> narrations;

  public void Awake() {
    if (this.State == null) this.State = new ExpositionState();

    Assert.IsNotNull(this.Dialogs);
    Assert.IsNotNull(this.DialogManager);
    this.dialogs =
      JsonUtility.FromJson<DialogStructure>(this.Dialogs.text);

    Assert.IsNotNull(this.Narrations);
    this.narrations =
      JsonUtility.FromJson<Dictionary<string, string[]>>(this.Narrations.text);
  }

  public void Dispatch(ExpositionType type, string key, bool once) {
    switch (type) {
      case ExpositionType.Dialog: {
          string[] texts = this.dialogs.Get(key);

          if (
            texts == null
            || (once && this.State.DialogFlags.GetValueOrDefault(key, false))
          ) return;

          this.State.DialogFlags.Add(key, true);
          this.DialogManager.SayMultiple(texts);
          break;
        }

      case ExpositionType.Narration: {
          string[] texts = this.narrations.GetValueOrDefault(key, null);

          if (
            texts == null ||
            once && this.State.NarrationFlags.GetValueOrDefault(key, false)
          ) return;

          this.State.NarrationFlags.Add(key, true);
          break;
        }
    }
  }
}