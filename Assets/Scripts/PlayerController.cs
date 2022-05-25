using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{


  [SerializeField]
  private float playerSpeed = 2.0f;

  [SerializeField]
  private float jumpHeight = 1.0f;

  [SerializeField]
  private float gravityValue = -9.81f;

  private CharacterController controller;
  private Vector3 playerVelocity;
  private bool groundedPlayer;

  private Vector2 movementInput = Vector2.zero;

  [SerializeField]
  private bool jumped = false;


  private void Start()
  {
    controller = gameObject.GetComponent<CharacterController>();
    var t = GetComponent<PlayerInput>().playerIndex;
    Debug.Log("NEW INDEX" + t);
  }

  public void OnMove(InputAction.CallbackContext ctx)
  {
    movementInput = ctx.ReadValue<Vector2>();
    Debug.Log(movementInput);
  }

  public void OnJump(InputAction.CallbackContext ctx)
  {
    // jumped = ctx.ReadValue<bool>();
    jumped = ctx.action.triggered;
    Debug.Log("JUMPED!");
  }

  void Update()
  {
    groundedPlayer = controller.isGrounded;
    if (groundedPlayer && playerVelocity.y < 0)
    {
      playerVelocity.y = 0f;
    }

    // Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
    Vector3 move = new Vector3(movementInput.x, movementInput.y);
    controller.Move(move * Time.deltaTime * playerSpeed);

    // if (move != Vector3.zero)
    // {
    //   gameObject.transform.forward = move;
    // }

    // Changes the height position of the player..
    if (jumped && groundedPlayer)
    {
      playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
    }

    playerVelocity.y += gravityValue * Time.deltaTime;
    controller.Move(playerVelocity * Time.deltaTime);
  }
}