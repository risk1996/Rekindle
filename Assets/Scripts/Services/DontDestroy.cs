using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour {
  private static DontDestroy instance;

  public void Awake() {
    if (instance == null) {
      instance = this;
      DontDestroyOnLoad(this);
    }
    else if (instance != null) {
      Destroy(this);
    }
  }

  void Start() {
  }

  void Update() {
  }
}