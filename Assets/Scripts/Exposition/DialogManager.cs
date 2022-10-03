﻿using System;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour {

  [field: SerializeField]
  public TextMeshProUGUI Display;

  [field: SerializeField]
  public float LetterPeriod = 0.05f;

  [field: SerializeField]
  public float TimeoutMultiplier = 0.25f;

  private float letterCountdown = 0;
  private int sentenceIndex = 0;
  private string targetSentence = "";
  private float timeout { get { return this.TimeoutMultiplier * this.targetSentence.Length; } }
  private float timeoutCountdown;

  public void Awake() {
    Assert.IsNotNull(this.Display);
  }

  public void Update() {
    if (this.sentenceIndex < this.targetSentence.Length) {
      this.letterCountdown -= Time.deltaTime;

      if (this.letterCountdown <= 0) {
        this.letterCountdown = this.LetterPeriod;
        this.sentenceIndex += 1;
        this.UpdateDisplay();

        if (this.sentenceIndex == this.targetSentence.Length)
          this.timeoutCountdown = this.timeout;
      }
    } else if (this.timeoutCountdown > 0) {
      this.timeoutCountdown -= Time.deltaTime;

      if (this.timeoutCountdown <= 0) {
        this.letterCountdown = 0;
        this.sentenceIndex = 0;
        this.targetSentence = "";
        this.timeoutCountdown = 0;
        this.UpdateDisplay();
      }
    }
  }

  public void Say(string text) {
    this.letterCountdown = this.LetterPeriod;
    this.targetSentence = text;
    this.sentenceIndex = 1;
    this.UpdateDisplay();
  }

  private void UpdateDisplay() {
    this.Display.text = this.targetSentence.Substring(0, this.sentenceIndex);
  }
}

