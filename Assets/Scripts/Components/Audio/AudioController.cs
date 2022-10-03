using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class AudioController : MonoBehaviour {
  private static AudioController instance;
  private GameObject audioGameObject;
  private static AudioSource[] audioSources;
  public static float customLoopStartTime;
  public static float customLoopEndTime;
  public float delay = 1f;

  // Start is called before the first frame update
  private void Awake() {
    if (instance == null) {
      instance = this;
    } else if (instance != null) {
      Destroy(this);
      return;
    }
  }

  void Start() {
    audioSources = GameObject.FindGameObjectWithTag("AudioSources").GetComponents<AudioSource>();
  }

  // Update is called once per frame
  void Update() {
    delay -= Time.deltaTime;
    if (delay < 0) {
      AudioPlay(AudioName.BGM);
      AudioPlayMultiple(new (AudioName, float)[] {
        (AudioName.HeartBeat_A, 0f), (AudioName.HeartBeat_B, 0.5f)
      });

    }
    
  }

  public static void AudioPlay(AudioName audioName) {
    AudioSource clip = AudioController.audioSources.First(source => source.clip.name == audioName.ToString());
    if (!clip.isPlaying) {
      clip.Play();
    }
  }

  public static void AudioCustomLoop(AudioName audioName, float customLoopStartTime = 0f, float customLoopEndTime = 0f) {
    AudioSource clip = AudioController.audioSources.First(source => source.clip.name == audioName.ToString());
    if (customLoopEndTime == 0) {
      customLoopEndTime = clip.clip.length;
    }
    if (clip != null &&
      clip.isPlaying &&
      clip.time > customLoopEndTime) {
      clip.time = customLoopStartTime;
    } else if (!clip.isPlaying) {
      clip.Play();
    }
  }

  public static void AudioStop(AudioName audioName) {
    AudioSource clip = AudioController.audioSources.First(source => source.clip.name == audioName.ToString());
    clip.Stop();
  }

  public static void AudioMute(AudioName audioName) {
    AudioSource clip = AudioController.audioSources.First(source => source.clip.name == audioName.ToString());
    clip.volume = 0f;
  }

  public static void AudioUnmute(AudioName audioName, float volume = 1f) {
    AudioSource clip = AudioController.audioSources.First(source => source.clip.name == audioName.ToString());
    clip.volume = volume;
  }

  public static void AudioPlayMultiple((AudioName audioName, float startTime)[] audio) {
    bool isPlaying = false;

    //Check if there's any audio playing
    for (int i = 0; i < audio.Length; i++) {
      AudioSource clip = AudioController.audioSources.First(source => source.clip.name == audio[i].audioName.ToString());
      if (clip.isPlaying) {
        isPlaying = true;
      } 
    }

    //Play only if it hasn't played yet!
    if (!isPlaying) {
      for (int i = 0; i < audio.Length; i ++) {
        AudioSource clip = AudioController.audioSources.First(source => source.clip.name == audio[i].audioName.ToString());
        if(clip != null) {
          clip.PlayDelayed(audio[i].startTime);
        }
      }
    }
  }
}

