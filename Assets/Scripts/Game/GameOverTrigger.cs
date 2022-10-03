using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;



[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class GameOverTrigger : MonoBehaviour {

  private GameManager gameManager;
  private int triggerCount = 0;
  private Rigidbody2D rb;
  public GameObject GameOverPanel;

  public TextMeshProUGUI Display;

  public void Awake() {
    this.gameManager = GameObject.FindObjectOfType<GameManager>();
    this.rb = this.GetComponent<Rigidbody2D>();
  }

  public void OnCollisionEnter2D(Collision2D collision) {
    if (collision.gameObject.tag == "Player") {
      if (triggerCount == 0) {
        collision.gameObject.GetComponent<MovementControls>().enabled = false;
        StartCoroutine(GameOver(collision));
        triggerCount++;
      }
    }
  }

  IEnumerator GameOver(Collision2D collision) {
    Display.text = "Day " + (gameManager.State.Day + 1);
    AudioController.AudioCustomPlay(AudioName.Fall, 4f, customEndTime: 5.5f);
    GameOverPanel.SetActive(true);
    GameOverPanel.GetComponent<Animator>().SetBool("isGameOver", true);
    yield return new WaitForSeconds(5f);
    this.gameManager.GameOver();
    yield return new WaitForSeconds(4.5f);
    GameOverPanel.GetComponent<Animator>().SetBool("isGameOver", false);
    GameOverPanel.SetActive(false);
    collision.gameObject.GetComponent<MovementControls>().enabled = true;
    triggerCount = 0;
  }
}
