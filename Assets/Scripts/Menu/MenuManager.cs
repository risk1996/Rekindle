using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;


public class MenuManager : MonoBehaviour { 

    public void StartGame() {
      SceneManager.LoadScene("Level_1");
    }

    public void ExitGame() {
    Debug.Log("Exit");
      Application.Quit();
    }
}
