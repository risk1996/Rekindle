using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;


public class MenuManager : MonoBehaviour { 

    public void StartGame() {
      AudioController.AudioPlay(AudioName.Button_Click);
      SceneManager.LoadScene("Level_1");
    }

    public void ExitGame() {
    Debug.Log("Exit");
      AudioController.AudioPlay(AudioName.Button_Click);
      Application.Quit();
    }

    public void ButtonHover() {
      AudioController.AudioPlay(AudioName.Button_Hover);
    }
}
