using System;
using System.Collections;
using UnityEngine;

public class PersistenceService<TData> where TData : new() {

  [field: SerializeField]
  public string SaveKey { get; set; } = "GAME_SAVE";

  public TData Load() {
    return JsonUtility.FromJson<TData>(
      PlayerPrefs.GetString(this.SaveKey)
    ) ?? new TData();
  }

  public void Save(TData data) {
    PlayerPrefs.SetString(this.SaveKey, JsonUtility.ToJson(data));
  }
}
