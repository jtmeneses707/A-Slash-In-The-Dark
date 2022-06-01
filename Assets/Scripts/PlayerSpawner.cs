using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSpawner : MonoBehaviour
{
  [SerializeField] private GameObject _PlayerObject;
  [SerializeField] private PlayerInputManager _InputManager;
  // Start is called before the first frame update
  [SerializeField] private int _PlayerNum = 0;

  [SerializeField] private AudioClip[] _audioClips;

  [SerializeField]
  private Vector3[] _spawnPoints = {
    new Vector3(-22f, -16f, 0f), new Vector3(22f, -16f, 0f)
  };

  [SerializeField]
  private GameLogicController _LogicController;
  void Start()
  {
    // var t = PlayerInput.Instantiate(_PlayerObject, -1, "Keyboard 1");
    // t.gameObject.transform.position = new Vector3(-17f, -7f, 0f);
    // var x = PlayerInput.Instantiate(_PlayerObject, -1, "Controller");
    // x.gameObject.transform.position = new Vector3(17f, -7f, 0f);

    // Debug.Log("SPAWNED PLAYERS!");
  }

  public void OnPlayerJoined(PlayerInput pi)
  {
    _LogicController.AddPlayers(pi.gameObject);
    // _PlayerNum++;
    Debug.Log("PLAYER JOINED");
    if (_PlayerNum == 0)
    {
      _LogicController.LeftPlayerHasJoined();
    }

    if (_PlayerNum == 1)
    {
      pi.gameObject.transform.rotation = new Quaternion(0, 180, 0, 1);
      _LogicController.RightPlayerHasJoined();
    }
    pi.gameObject.transform.position = _spawnPoints[_PlayerNum];
    _PlayerNum++;
    if (_PlayerNum == 2)
    {
      _LogicController.ProgressState();
    }


    // pi.gameObject.transform.position = new Vector3(0, 0, 0);
    // pi.gameObject.SetActive(false);

    // pi.transform.position = new Vector3(2, 2, 2);
    // Debug.Log("LOCATION" + pi.gameObject.transform.localScale = 200);
  }



  // Update is called once per frame
  // void z
}
