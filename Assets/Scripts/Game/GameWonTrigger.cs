using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]

public class GameWonTrigger : MonoBehaviour {
  private GameManager gameManager;
  private int triggerCount = 0;
  private Rigidbody2D rb;
  public GameObject WinningPanel;

  public void Awake() {
    this.gameManager = GameObject.FindObjectOfType<GameManager>();
    this.rb = this.GetComponent<Rigidbody2D>();
  }

  public void OnTriggerEnter2D(Collider2D collider) {
    if (collider.gameObject.tag == "Player") {
      if (triggerCount == 0) {
        collider.gameObject.GetComponent<MovementControls>().enabled = false;
        StartCoroutine(GameWon(collider));
        triggerCount++;
      }
    }
  }

  IEnumerator GameWon(Collider2D collider) {
    WinningPanel.SetActive(true);
    WinningPanel.GetComponent<Animator>().SetBool("isWinning", true);
    yield return new WaitForSeconds(4);
    this.gameManager.GameWon();
    yield return new WaitForSeconds(8);
    WinningPanel.GetComponent<Animator>().SetBool("isWinning", false);
    SceneManager.LoadScene("Menu");
    WinningPanel.SetActive(false);
    collider.gameObject.GetComponent<MovementControls>().enabled = false;
  }
}
