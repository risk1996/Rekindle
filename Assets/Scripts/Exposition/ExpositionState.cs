using System;
using System.Collections.Generic;

[Serializable]
public class ExpositionState {

  public Dictionary<string, bool> DialogFlags = new Dictionary<string, bool>();
  public Dictionary<string, bool> NarrationFlags = new Dictionary<string, bool>();
}

