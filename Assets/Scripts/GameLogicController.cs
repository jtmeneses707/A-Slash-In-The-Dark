using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Experimental.Rendering.Universal;
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

  [SerializeField]
  private GameObject lantern;

  [SerializeField]
  private GameObject globalLight;

  [SerializeField]
  private float startingGlobalIntensity;

  [SerializeField]
  private Sprite brokenLanternSprite;

  [SerializeField]
  private float baseRoundTime;

  [SerializeField]
  private float totalRoundTime = 0f;

  [SerializeField]
  private float randomRoundTime;

  [SerializeField]
  private float initialThunderWarning;

  private bool hasPlayedThunderWarning = false;

  [SerializeField]
  private Canvas canvas;



  // Start is called before the first frame update
  void Start()
  {
    canvas.enabled = false;
    startingGlobalIntensity = globalLight.GetComponent<Light2D>().intensity;
    randomRoundTime = baseRoundTime + Random.Range(0f, 5f);
    initialThunderWarning = randomRoundTime - Random.Range(3f, 5f);
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
      foreach (var player in players)
      {
        player.GetComponent<PlayerController2D>().enabled = false;
      }

    }

    if (curState == State.Start)
    {
      if (!hasPlayedCurStateEvent)
      {
        foreach (var player in players)
        {
          player.GetComponent<PlayerController2D>().enabled = false;
        }
        hasPlayedCurStateEvent = true;
        StartCoroutine(StartingAnim());
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
    }

    if (curState == State.Play)
    {
      if (!hasPlayedCurStateEvent)
      {
        foreach (var player in players)
        {
          player.GetComponent<PlayerController2D>().enabled = true;
          player.GetComponent<PlayerController2D>().SetCanMove(true);
        }
      }

      print("IN PLAY MODE");
      totalRoundTime += Time.fixedDeltaTime;

      foreach (var player in players)
      {
        if (player.GetComponent<PlayerController2D>().IsDead())
        {
          ProgressState();
        }
      }

      if (totalRoundTime >= randomRoundTime)
      {
        // Progress state if round has finished.
        // State will progress iin other cases such as players hitting each other.
        ProgressState();
      }


      if (totalRoundTime >= initialThunderWarning)
      {
        if (!hasPlayedThunderWarning)
        {
          announcer.volume = 0.5f;
          announcer.PlayOneShot(clips[5]);
          announcer.volume = 1f;
          hasPlayedThunderWarning = true;
          foreach (var player in players)
          {
            player.GetComponent<PlayerController2D>().CanAttack();
          }
        }
      }
    }


    if (curState == State.Results)
    {
      if (!hasPlayedCurStateEvent)
      {
        hasPlayedCurStateEvent = true;
        foreach (var player in players)
        {
          // player.GetComponent<PlayerController2D>().enabled = false;
          player.GetComponent<PlayerController2D>().SetCanMove(false);
          player.GetComponent<Animator>().enabled = false;
        }
        StartCoroutine(ResultsAnim());
      }

    }

    if (curState == State.End)
    {
      canvas.enabled = true;
      foreach (var player in players)
      {
        var controller = player.GetComponent<PlayerController2D>();
        if (controller.reset)
        {
          print("RESET");
        }
      }
    }
  }




  // Coroutines for animations and events. 

  // public void StartingAnim()
  // {
  //   GameObject leftPlayer, rightPlayer;
  //   PlayerController2D left, right;
  //   // Dynamically decide which players are left and right. 
  //   if (players[0].transform.position.x > players[1].transform.position.x)
  //   {
  //     leftPlayer = players[1];
  //     rightPlayer = players[0];
  //   }
  //   else
  //   {
  //     leftPlayer = players[0];
  //     rightPlayer = players[1];
  //   }

  //   left = leftPlayer.GetComponent<PlayerController2D>();
  //   right = rightPlayer.GetComponent<PlayerController2D>();

  //   Debug.Log("LEFT PLAYERS COORDS" + left.transform.position);
  //   Debug.Log("RIGHT PLAYERS COORDS" + right.transform.position);

  //   // Start walking players towards stage.
  //   StartCoroutine(left.MoveRightFromStage(2f, 4f));
  //   // StartCoroutine(right.MoveLeftFromStage(2f, 4f));
  // }

  // public IEnumerator StartingA()
  // {
  //   yield return lc.MoveRightFromStage(2f, 4f);
  // }

  // public IEnumerator StartingB()
  // {
  //   yield return rc.MoveRightFromStage(2f, 4f);
  //   yield return StartingA();
  // }


  // public IEnumerator StartingAnimation()
  // {
  //   GameObject leftPlayer, rightPlayer;
  //   PlayerController2D left, right;
  //   // Dynamically decide which players are left and right. 
  //   if (players[0].transform.position.x > players[1].transform.position.x)
  //   {
  //     leftPlayer = players[1];
  //     rightPlayer = players[0];
  //   }
  //   else
  //   {
  //     leftPlayer = players[0];
  //     rightPlayer = players[1];
  //   }

  //   left = leftPlayer.GetComponent<PlayerController2D>();
  //   right = rightPlayer.GetComponent<PlayerController2D>();
  //   Debug.Log("LEFT PLAYERS COORDS" + left.transform.position);
  //   Debug.Log("RIGHT PLAYERS COORDS" + right.transform.position);
  //   // yield return StartCoroutine(left.MoveRightFromStage(2f, 4f));
  //   // yield return new WaitForSeconds(5f);
  //   // Debug.Log("STARTING RIGHT COROUTINE");
  //   // yield return StartCoroutine(right.MoveLeftFromStage(2f, 4f));

  //   yield return StartCoroutine(left.MoveRightFromStage(2f, 4f));
  //   // yield return lc;
  //   Debug.Log("FINISHED LC");
  //   // Coroutine rc = StartCoroutine(right.MoveLeftFromStage(2f, 4f));
  //   // yield return rc;

  //   // yield return lc;
  //   // yield return rc;

  // }

  public IEnumerator StartingAnim()
  {
    yield return new WaitForSecondsRealtime(2f);
    announcer.PlayOneShot(clips[3]);
    // lantern.GetComponent<Light2D>().intensity = 0f;


    yield return new WaitForSecondsRealtime(0.1f);
    lantern.GetComponent<SpriteRenderer>().sprite = brokenLanternSprite;
    var lanternLight = lantern.GetComponentInChildren<Light2D>();
    var intensity = lanternLight.intensity;
    for (var i = intensity; i > 0; i -= 0.1f)
    {
      lanternLight.intensity = i;
      yield return new WaitForSecondsRealtime(.1f);
    }
    // yield return new WaitForSecondsRealtime(2f);
    globalLight.GetComponent<Light2D>().intensity = 0f;
    var clip = clips[4];
    var length = clip.length;
    announcer.PlayOneShot(clip);
    yield return new WaitForSecondsRealtime(length);
    print("DONE WITH STARTING ANIM");
    ProgressState();
  }

  public IEnumerator ResultsAnim()
  {
    var light = globalLight.GetComponent<Light2D>();
    var currentGlobalIntensity = light.intensity;
    for (var i = currentGlobalIntensity; i < 0.6f; i += 0.05f)
    {
      light.intensity = i;
      yield return new WaitForSecondsRealtime(0.05f);
    }
    yield return new WaitForSecondsRealtime(1f);
    foreach (var player in players)
    {
      var pc = player.GetComponent<PlayerController2D>();
      pc.ZeroMovement();
      player.GetComponent<Animator>().enabled = true;
    }

    GameObject leftPlayer, rightPlayer;
    PlayerController2D lc, rc;
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

    lc = leftPlayer.GetComponent<PlayerController2D>();
    rc = rightPlayer.GetComponent<PlayerController2D>();

    if ((lc.IsDead() == rc.IsDead()))
    {
      // print("GAME IS A TIE");
      announcer.PlayOneShot(clips[7]);
    }

    else if (lc.IsDead())
    {
      // print("RIGHT WINS");
      announcer.PlayOneShot(clips[8]);
    }
    else
    {
      announcer.PlayOneShot(clips[9]);
    }

    yield return new WaitForSecondsRealtime(2f);
    ProgressState();

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
}
