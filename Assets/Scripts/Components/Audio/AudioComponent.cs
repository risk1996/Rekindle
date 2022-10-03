using System;
using UnityEngine;

[Serializable]
public struct AudioComponent {
  [field: SerializeField]
  public AudioName Name;
  public AudioSource AudioSource { get; set; }

  [field: SerializeField]

  public AudioClip AudioClip { get; set; }

  public AudioComponent(
    AudioName Name,
    AudioSource AudioSource,
    AudioClip AudioClip
  ) {
    this.Name = Name;
    this.AudioSource = new AudioSource();
    this.AudioClip = AudioClip;
    this.AudioSource.clip = AudioClip;
  }
}