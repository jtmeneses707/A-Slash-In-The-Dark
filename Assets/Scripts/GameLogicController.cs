using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogicController : MonoBehaviour
{
  public enum State
  {
    // Prep phase. Announcer asks players to join.
    Join,
    // Start phase. Player animation starts. 
    Start,
    // Player interactable state.
    Play,
    // Nearing end state. Either player or both have died, or game ended with a tie.
    Results,
    // Complete end state. Screen with options to restart or exit appear.
    End
  }

  [SerializeField]
  private State curState = State.Join;

  [SerializeField]
  private List<GameObject> players;

  [SerializeField]
  private AudioSource announcer;

  [SerializeField]
  private AudioClip[] clips;

  private bool playedPressToJoin = false;

  [SerializeField]
  private bool hasPlayedCurStateEvent = false;

  private GameObject leftPlayer, rightPlayer;
  private PlayerController2D lc, rc;



  // Start is called before the first frame update
  void Start()
  {

  }

  // Update is called once per frame
  void FixedUpdate()
  {

    if (curState == State.Join)
    {
      Debug.Log("JOIN PHASE");
      if (!playedPressToJoin)
      {
        Debug.Log("PLAYING CLIP");
        announcer.PlayOneShot(clips[0]);
        playedPressToJoin = true;
      }

    }

    if (curState == State.Start)
    {
      if (!hasPlayedCurStateEvent)
      {

        StartCoroutine(d());
        hasPlayedCurStateEvent = true;
        // GameObject leftPlayer, rightPlayer;
        // PlayerController2D left, right;
        // // Dynamically decide which players are left and right. 
        // if (players[0].transform.position.x > players[1].transform.position.x)
        // {
        //   leftPlayer = players[1];
        //   rightPlayer = players[0];
        // }
        // else
        // {
        //   leftPlayer = players[0];
        //   rightPlayer = players[1];
        // }

        // lc = leftPlayer.GetComponent<PlayerController2D>();
        // rc = rightPlayer.GetComponent<PlayerController2D>();

        // StartCoroutine(StartingB());

        // Debug.Log("LEFT PLAYERS COORDS" + left.transform.position);
        // Debug.Log("RIGHT PLAYERS COORDS" + right.transform.position);

        // // Start walking players towards stage.
        // StartCoroutine(left.MoveRightFromStage(2f, 4f));
        // StartCoroutine(right.MoveLeftFromStage(2f, 4f));
        // // StartCoroutine(StartingAnimation());
        // hasPlayedCurStateEvent = true;



      }
      // TODO: PLAY STARTING ANIMATION.
    }
  }




  // Coroutines for animations and events. 

  public void StartingAnim()
  {
    GameObject leftPlayer, rightPlayer;
    PlayerController2D left, right;
    // Dynamically decide which players are left and right. 
    if (players[0].transform.position.x > players[1].transform.position.x)
    {
      leftPlayer = players[1];
      rightPlayer = players[0];
    }
    else
    {
      leftPlayer = players[0];
      rightPlayer = players[1];
    }

    left = leftPlayer.GetComponent<PlayerController2D>();
    right = rightPlayer.GetComponent<PlayerController2D>();

    Debug.Log("LEFT PLAYERS COORDS" + left.transform.position);
    Debug.Log("RIGHT PLAYERS COORDS" + right.transform.position);

    // Start walking players towards stage.
    StartCoroutine(left.MoveRightFromStage(2f, 4f));
    // StartCoroutine(right.MoveLeftFromStage(2f, 4f));
  }

  public IEnumerator StartingA()
  {
    yield return lc.MoveRightFromStage(2f, 4f);
  }

  public IEnumerator StartingB()
  {
    yield return rc.MoveRightFromStage(2f, 4f);
    yield return StartingA();
  }


  public IEnumerator StartingAnimation()
  {
    GameObject leftPlayer, rightPlayer;
    PlayerController2D left, right;
    // Dynamically decide which players are left and right. 
    if (players[0].transform.position.x > players[1].transform.position.x)
    {
      leftPlayer = players[1];
      rightPlayer = players[0];
    }
    else
    {
      leftPlayer = players[0];
      rightPlayer = players[1];
    }

    left = leftPlayer.GetComponent<PlayerController2D>();
    right = rightPlayer.GetComponent<PlayerController2D>();
    Debug.Log("LEFT PLAYERS COORDS" + left.transform.position);
    Debug.Log("RIGHT PLAYERS COORDS" + right.transform.position);
    // yield return StartCoroutine(left.MoveRightFromStage(2f, 4f));
    // yield return new WaitForSeconds(5f);
    // Debug.Log("STARTING RIGHT COROUTINE");
    // yield return StartCoroutine(right.MoveLeftFromStage(2f, 4f));

    yield return StartCoroutine(left.MoveRightFromStage(2f, 4f));
    // yield return lc;
    Debug.Log("FINISHED LC");
    // Coroutine rc = StartCoroutine(right.MoveLeftFromStage(2f, 4f));
    // yield return rc;

    // yield return lc;
    // yield return rc;

  }

  public State GetCurState()
  {
    return curState;
  }

  public void ProgressState()
  {
    // Reset hasPlayed anim to prepare for new anim. 
    if (hasPlayedCurStateEvent)
    {
      hasPlayedCurStateEvent = false;
    }
    switch (curState)
    {

      case State.Join:
        curState = State.Start;
        return;

      case State.Start:
        curState = State.Play;
        return;

      case State.Play:
        curState = State.Results;
        return;

      case State.Results:
        curState = State.End;
        return;
    }
  }

  public void LeftPlayerHasJoined()
  {
    announcer.Stop();
    announcer.PlayOneShot(clips[1]);
  }

  public void RightPlayerHasJoined()
  {
    announcer.Stop();
    announcer.PlayOneShot(clips[2]);
  }


  public void AddPlayers(GameObject player)
  {
    players.Add(player);
    Debug.Log("ADDED NEW PLAYER. SIZE IS " + players.Count);
  }

  IEnumerator First() { yield return new WaitForSeconds(1f); }
  IEnumerator Second() { yield return new WaitForSeconds(2f); }
  IEnumerator Third() { yield return new WaitForSeconds(3f); }

  IEnumerator d()
  {
    Coroutine a = StartCoroutine(First());
    Coroutine b = StartCoroutine(Second());
    Coroutine c = StartCoroutine(Third());

    //wait until all of them are over
    yield return a;
    yield return b;
    yield return c;

    print("all over");
  }
}
