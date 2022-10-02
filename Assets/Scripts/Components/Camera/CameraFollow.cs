using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
  private Vector2 velocity;
  public Vector2 SmoothTime;
  public GameObject Player;

  /*
   * Camera Offset Position from Player
   */
  public Vector2 CameraOffset;


  public Vector2 MinimumPoint;
  public Vector2 MaximumPoint;
  void Start() {
    Player = GameObject.FindGameObjectWithTag("Player");

  }

  void Update() {

  }

  void FixedUpdate() {
    float posX = Mathf.SmoothDamp(transform.position.x, Player.transform.position.x + CameraOffset.x, ref velocity.x, SmoothTime.x);
    float posY = Mathf.SmoothDamp(transform.position.y, Player.transform.position.y + CameraOffset.y, ref velocity.y, SmoothTime.y);

    if (posX < MinimumPoint.x) {
      transform.position = new Vector3(MinimumPoint.x, transform.position.y, transform.position.z);
    } else if (posX > MaximumPoint.x) {
      transform.position = new Vector3(MaximumPoint.x, transform.position.y, transform.position.z);
    } else {
      transform.position = new Vector3(posX, transform.position.y, transform.position.z);
    }


    if (posY < MinimumPoint.y) {
      transform.position = new Vector3(transform.position.x, MinimumPoint.y, transform.position.z);
    } else if (posY > MaximumPoint.y) {
      transform.position = new Vector3(transform.position.x, MaximumPoint.y, transform.position.z);
    } else {
      transform.position = new Vector3(transform.position.x, posY, transform.position.z);
    }
  }
}
