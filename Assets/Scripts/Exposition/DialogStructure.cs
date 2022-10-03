using System;

[Serializable]
public class DialogStructure {

  public string[] MonsterAvoidLight;
  public string[] MonsterEncounter;
  public string[] TorchApproach;
  public string[] TorchFirstTimeExtinguished;
  public string[] TorchSecondTimeRekindle;

  public string[] Get(string key) {
    switch (key) {
      case "MonsterAvoidLight": return this.MonsterAvoidLight;
      case "MonsterEncounter": return this.MonsterEncounter;
      case "TorchApproach": return this.TorchApproach;
      case "TorchFirstTimeExtinguished": return this.TorchFirstTimeExtinguished;
      case "TorchSecondTimeRekindle": return this.TorchSecondTimeRekindle;
      default: return null;
    }
  }
}
