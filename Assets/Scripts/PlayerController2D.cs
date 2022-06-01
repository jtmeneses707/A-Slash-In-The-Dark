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

  [SerializeField]
  private Vector2 movementInput = Vector2.zero;

  private float distToGround;

  [SerializeField] private AudioClip[] footsteps, slashes, hurts;

  [SerializeField]
  private float timeBetweenEachFootstep = 1f;

  [SerializeField]
  private float currentTimeBetweenFootstep = 0f;

  private AudioSource audioSource;

  [SerializeField]
  private bool isDead = false;

  [SerializeField]
  private bool isMoveLeft = false;



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
    if (isMoveLeft)
    {
      StartCoroutine(MoveLeftFromStage());
    }
    timeSinceLastAttack += Time.deltaTime;
    if (!isDead)
    {
      Move();
    }

  }


  /** PRIVATE HELPERS **/

  // Reads input from OnMove calllback. 
  private void Move()
  {
    var curPos = rb.transform.position;
    var move = new Vector3(movementInput.x, 0);
    var newPos = curPos + (Time.fixedDeltaTime * playerSpeed * move);
    rb.MovePosition(newPos);
    if (Mathf.Abs(move.x) > 0)
    {
      currentTimeBetweenFootstep += Time.fixedDeltaTime;
    }
    if (currentTimeBetweenFootstep >= timeBetweenEachFootstep)
    {
      currentTimeBetweenFootstep = 0f;
      // var firstStep = footsteps[0];
      var index = Random.Range(1, footsteps.Length);
      var clip = footsteps[index];
      audioSource.PlayOneShot(clip);
      footsteps[index] = footsteps[0];
      footsteps[0] = clip;
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
    if (!isDead)
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
    }

    animator.SetFloat("speed", Mathf.Abs(movementInput.x));
    // Debug.Log("MOVEMENT INPUT:" + movementInput);
  }

  public void OnAttack(InputAction.CallbackContext ctx)
  {
    Debug.Log("ATTACK");
    if (!isDead)
    {
      if (timeSinceLastAttack >= attackDelay)
      {
        var currentAudioScale = audioSource.volume;
        audioSource.PlayOneShot(slashes[Random.Range(0, slashes.Length)]);
        timeSinceLastAttack = 0f;
        animator.SetBool("isAttack", true);
        // animator.SetBool("isAttack", false);
      }
    }

  }

  public void SetStartPos(Vector3 pos)
  {
    startPos = pos;
  }

  public void StartDeath()
  {
    audioSource.PlayOneShot(hurts[Random.Range(0, hurts.Length)]);
    isDead = true;
    animator.SetBool("isHurt", isDead);
    bc.enabled = false;
    Destroy(rb);
  }


  // Used to manually move character left. 
  // Useful for animations or intro screen.
  public IEnumerator MoveLeftFromStage()
  {
    var origPlayerSpeed = playerSpeed;
    playerSpeed = 3f;
    movementInput = new Vector2(-1f, 0f);
    yield return new WaitForSecondsRealtime(3f);
    movementInput = Vector2.zero;
    playerSpeed = origPlayerSpeed;
  }

  // Helpers for InputAction callback function


  // Update is called once per frame
  // void Update()
  // {

  // }
}
