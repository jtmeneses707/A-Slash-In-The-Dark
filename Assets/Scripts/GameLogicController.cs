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
  private GameObject[] players;

  [SerializeField]
  private AudioSource announcer;

  [SerializeField]
  private AudioClip[] clips;

  private bool playedPressToJoin = false;


  // Start is called before the first frame update
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
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
      // TODO: PLAY STARTING ANIMATION.
    }
  }




  public State GetCurState()
  {
    return curState;
  }

  public void ProgressState()
  {
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
}
