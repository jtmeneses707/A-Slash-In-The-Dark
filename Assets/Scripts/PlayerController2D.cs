using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// [RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(AudioSource))]
public class PlayerController2D : MonoBehaviour
{

  [SerializeField]
  private float playerSpeed = 2.0f;

  [SerializeField]
  private float jumpHeight = 1.0f;

  [SerializeField]
  private float gravityValue = -9.81f;

  [SerializeField]
  private bool jumped = false;

  [SerializeField]
  private float attackDelay = 1f;

  [SerializeField]
  private float timeSinceLastAttack = 0f;

  [SerializeField]
  private Vector3 startPos;

  private Vector3 playerVelocity;
  private bool groundedPlayer;

  private Animator animator;

  private Rigidbody2D rb;

  private CapsuleCollider2D bc;

  private Vector2 movementInput = Vector2.zero;

  private float distToGround;

  [SerializeField] private AudioClip[] footsteps;

  [SerializeField]
  private float timeBetweenEachFootstep = 1f;

  [SerializeField]
  private float currentTimeBetweenFootstep = 0f;

  private AudioSource audioSource;


  void Awake()
  {
    animator = this.gameObject.GetComponent<Animator>();
    rb = GetComponent<Rigidbody2D>();
    bc = GetComponent<CapsuleCollider2D>();
    audioSource = GetComponent<AudioSource>();
  }

  // Start is called before the first frame update
  void Start()
  {
    Debug.Log("START POS IS " + startPos);


    distToGround = bc.bounds.extents.y;
    // animator.runtimeAnimatorController.
    // this.gameObject.transform.position = startPos;
    // controller = gameObject.GetComponent<CharacterController>();
    // // controller.enabled = false;
    // // transform.position = new Vector3(15f, -7f, 0f);
    // // controller.enabled = true;
    // // this.gameObject.transform.position = new Vector3(15f, 15, 15f);
    // Debug.Log(this.controller.transform.position);
  }


  void FixedUpdate()
  {
    timeSinceLastAttack += Time.deltaTime;
    Move();
  }


  /** PRIVATE HELPERS **/

  // Reads input from OnMove calllback. 
  private void Move()
  {
    var curPos = rb.transform.position;
    var move = new Vector3(movementInput.x, 0);
    var newPos = curPos + (Time.fixedDeltaTime * playerSpeed * move);
    rb.MovePosition(newPos);
    Debug.Log("MAG" + rb.velocity.magnitude);
    if (Mathf.Abs(move.x) > 0)
    {
      currentTimeBetweenFootstep += Time.fixedDeltaTime;
    }
    if (currentTimeBetweenFootstep >= timeBetweenEachFootstep)
    {
      currentTimeBetweenFootstep = 0f;
      audioSource.PlayOneShot(footsteps[(Random.Range(0, footsteps.Length))]);
    }
  }

  private bool IsGrounded()
  {
    return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f);
  }

  // Animator Helpers

  // Used at end of death event to make sure player is stuck on death animation.
  private void SetAnimatorInactive()
  {
    animator.enabled = false;
  }

  private void SetAttackingFalse()
  {
    animator.SetBool("isAttack", false);
  }



  /** PUBLIC HELPERS **/

  public void OnMove(InputAction.CallbackContext ctx)
  {
    movementInput = ctx.ReadValue<Vector2>();
    if (movementInput.x > 0)
    {
      transform.rotation = new Quaternion(0, 0, 0, 1);
    }
    else if (movementInput.x < 0)
    {
      transform.rotation = new Quaternion(0, 180, 0, 1);
    }
    animator.SetFloat("speed", Mathf.Abs(movementInput.x));
    // Debug.Log("MOVEMENT INPUT:" + movementInput);
  }

  public void OnAttack(InputAction.CallbackContext ctx)
  {
    Debug.Log("ATTACK");
    if (timeSinceLastAttack >= attackDelay)
    {
      timeSinceLastAttack = 0f;
      animator.SetBool("isAttack", true);
      // animator.SetBool("isAttack", false);
    }
  }

  public void SetStartPos(Vector3 pos)
  {
    startPos = pos;
  }

  // Helpers for InputAction callback function


  // Update is called once per frame
  // void Update()
  // {

  // }
}
